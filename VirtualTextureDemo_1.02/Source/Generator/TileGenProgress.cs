/* 
MIT License

Copyright (c) 2009 Brad Blanchard (www.linedef.com)

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace VirtualTextureDemo
{
	public partial class TileGenProgress: Form, IDisposable
	{
		readonly DemoInfo		info;
		readonly string			cachename;
		readonly TileGenerator	generator;

		Thread thread;
		volatile bool active;

		int currentpage;

		System.Windows.Forms.Timer timer;

		Stopwatch	sw;

		public TileGenProgress( DemoInfo info )
		{
			InitializeComponent();

			this.info = info;
			this.cachename = info.TextureFile + ".cache";

			generator = new TileGenerator( info.TextureFile, info.VirtualTextureInfo );

			progress.Maximum = generator.Count;

			timer = new System.Windows.Forms.Timer();
			timer.Interval = 500;
			timer.Tick += ( sender, e ) => SetProgress();
			timer.Start();

			thread = new Thread( new ThreadStart( Generate ) );
			thread.Start();
		}

		public new void Dispose()
		{
			timer.Stop();

			active = false;
			thread.Join();

			int count = generator.Count;
			generator.Dispose();

			// Check to make sure we finished
			if( progress.Value < count && File.Exists( cachename ) )
				File.Delete( cachename );

			base.Dispose();
		}

		void Generate()
		{
			active = true;

			Console.WriteLine("Building tiles...");
			sw = Stopwatch.StartNew();
			foreach( int index in generator.Generate() )
			{
				if( !active ) break;
				this.currentpage = index;
			}

			if( active )
				Invoke( new Action( SetComplete ), null );
		}

		void SetComplete()
		{
			timer.Stop();
			b_close.Enabled = true;
			b_cancel.Enabled = false;
			progress.Value = generator.Count;
			progress.Enabled = false;
			l_pagevalue.Text = string.Format( "{0} of {0} (100%)", generator.Count ); 
			System.Media.SystemSounds.Asterisk.Play();
		}

		void SetProgress()
		{
			progress.Value = currentpage;
			l_pagevalue.Text = string.Format( "{0} of {1} ({2}%)", currentpage, generator.Count, 100*progress.Value/generator.Count ); 
			l_elapsed.Text = DateTime.MinValue.Add( sw.Elapsed ).ToString( "H' Hours 'm' Minutes 's' Seconds'" );
		}
	}
}
