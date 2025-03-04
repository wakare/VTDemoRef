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
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualTextureDemo
{
	public partial class Options: Form
	{
		const int DefaultFeedbackBufferSize = 64;
		const int DefaultTileSize			= 128;
		const int DefaultBorderSize			= 1;
		const int DefaultAtlasSize			= 4096;
		const int DefaultUploadsPerFrame	= 5;

		bool cacheexists;

		public DemoInfo DemoInfo 
		{
			get 
			{
				DemoInfo info = new DemoInfo()
				{
					TextureFile = tb_texture.Text,
					GeometryFile = tb_geometry.Text,

					AtlasSize = int.Parse( cb_atlassize.Text ),
					UploadsPerFrame = int.Parse( tb_uploads.Text ),
					
					Multisamples = int.Parse( cb_ms.Text ),					

					FeedbackBufferSize = int.Parse( cb_feedbacksize.Text ),

					VirtualTextureInfo = new VirtualTextureInfo()
					{
						VirtualTextureSize = int.Parse( cb_vt_size.Text ),
						TileSize = int.Parse( cb_vt_tilesize.Text ),
						BorderSize = int.Parse( cb_vt_bordersize.Text )
					}
				};

				string[] tokens = cb_res.Text.Split( ' ', 'x' );
				info.WindowSize = new Size( int.Parse( tokens[0] ), int.Parse( tokens[1] ) );

				return info;
			}
		}

		public Options()
		{
			InitializeComponent();

			//ll_url.Click += ( sender, e ) => System.Diagnostics.Process.Start( "http://www.linedef.com/personal/demos/?p=virtual-texturing" );
			ll_url.Click += ( sender, e ) => System.Diagnostics.Process.Start( "http://www.linedef.com" );

			b_browse_texture.Click  += ( sender, e ) => OpenFile( "All Types (*.png)|*.png|All Files (*.*)|*", tb_texture );
			b_browse_geometry.Click += ( sender, e ) => OpenFile( "3dsmax IGame Export (*.xml)|*.xml|All Files (*.*)|*.*", tb_geometry );

			tb_texture.TextChanged += new EventHandler( tb_texture_TextChanged );
			tb_geometry.TextChanged += new EventHandler( tb_geometry_TextChanged );

			SetToolTip
			( 
				"This is the size of the virtual texture.\r\n" +
				"It should be the same size as the input texture.\r\n" + 
				"Currently only power of two sizes are supported.\r\n" +
				"Textures must have 4 8-bit channels (RGBA).",
				cb_vt_size, l_vt_size
			);

			SetToolTip
			(
				"Tile size represents the usable portion of a page.\r\n" +
				"The larger the page size the slower the uploads will be.\r\n" +
				"The smaller the size, the more memory will be \"wasted\"\r\n" + 
				"with borders. Only power of two sizes are supported.\r\n" +
				"PageSize = TileSize + 2 * BorderSize",
				cb_vt_tilesize, l_vt_tilesize
			);

			SetToolTip
			(
				"BorderSize is is used to pad the tile for filtering.\r\n" +
				"This value should be larger to match texture compression block\r\n" + 
				"size or to support higher levels of filtering. This demo\r\n" +
				"currently only supports bilinear & brute force trilinear\r\n" + 
				"and therefore only needs a border of 1. There are no size\r\n" + 
				"restrictions.\r\n",
				cb_vt_bordersize, l_vt_bordersize
			);

			SetToolTip
			(
				"Feedback buffer size represents the size of render target\r\n" +
				"during the pre pass.  The smaller the size the faster the \r\n" +
				"downloads.  If you make it too small there is a chance \r\n" + 
				"pages could be skipped",
				cb_feedbacksize, l_feedbacksize
			);

			SetToolTip
			(
				"Atlas size controls the size of the texture that holds the \r\n" + 
				"pages on the gpu. You can shrink the size of the atlas\r\n" + 
				"to fit a specific memory budget.  Direct3D 10 supports\r\n" + 
				"a maximum of 8192.  If the page size is not a power of two \r\n" + 
				"the size will be shrunk down to be evenly divisible by the \r\n" + 
				"page size.",
				cb_atlassize, l_atlassize
			);

			SetToolTip
			(
				"This controls the number of pages sent to the GPU each frame.\r\n" +
				"The lower the value the better the performance. The higher \r\n" + 
				"the value the less delay in page loading.",
				tb_uploads, l_uploads
			);

			cb_vt_tilesize.Text = DefaultTileSize.ToString();
			cb_vt_bordersize.Text = DefaultBorderSize.ToString();

			cb_feedbacksize.Text = DefaultFeedbackBufferSize.ToString();
			cb_atlassize.Text = DefaultAtlasSize.ToString();
			tb_uploads.Text = DefaultUploadsPerFrame.ToString();

			foreach( DemoInfo info in DemoPresets.Infos )
				if( File.Exists( info.TextureFile ) && File.Exists( info.GeometryFile ) )
					cb_preset.Items.Add( info );

			if( cb_preset.Items.Count > 0 )
				cb_preset.SelectedIndex = 0;
			else
			{
				cb_preset.Enabled = false;
				l_preset.Enabled = false;
			}

			cb_res.SelectedIndex = cb_res.Items.Count-2;
			cb_ms.SelectedIndex  = cb_ms.Items.Count-2;

			b_go.Select();
		}

		void tb_geometry_TextChanged( object sender, EventArgs e )
		{
			b_go.Enabled = !string.IsNullOrEmpty( tb_texture.Text );
		}

		void tb_texture_TextChanged( object sender, EventArgs e )
		{
			string cachename = tb_texture.Text + ".cache";
			cacheexists = File.Exists( cachename );
			b_delete.Enabled = cacheexists;

			cb_vt_size.Enabled = !cacheexists;
			cb_vt_tilesize.Enabled = !cacheexists;
			cb_vt_bordersize.Enabled = !cacheexists;
			
			if( cacheexists )
			{
				VirtualTextureInfo info = default(VirtualTextureInfo);
				using( TileDataFile tdfile = new TileDataFile( cachename, info, FileAccess.Read ) )
					info = tdfile.ReadInfo();

				cb_vt_size.Text = info.VirtualTextureSize.ToString();
				cb_vt_tilesize.Text = info.TileSize.ToString();
				cb_vt_bordersize.Text = info.BorderSize.ToString();

				b_go.Text = "Start";
				b_go.Enabled = !string.IsNullOrEmpty( tb_geometry.Text );
			}

			// Automatically fill in size
			else
			{
				if( File.Exists( tb_texture.Text ) )
				{
					if( tb_texture.Text.Contains( ".tga" ) )
					{
						TargaImage.Header header = default(TargaImage.Header);
						using( FileStream file = File.OpenRead( tb_texture.Text ) )
							header = Memory.Read<TargaImage.Header>( file );

						cb_vt_size.Text = header.Width.ToString();
					}

					else if( tb_texture.Text.Contains( ".raw" ) )
					{
						FileInfo info = new FileInfo( tb_texture.Text );
						long size = info.Length / VirtualTexture.ChannelCount;

						// This only works for power of two images
						cb_vt_size.Text = ((long)System.Math.Sqrt( size )).ToString();
					}

					b_go.Text = "Generate Pages";
					b_go.Enabled = !string.IsNullOrEmpty( tb_geometry.Text );
				}
			}

			b_go.Select();
		}

		private void cb_preset_SelectedIndexChanged( object sender, EventArgs e )
		{
			DemoInfo info = cb_preset.SelectedItem as DemoInfo;

			tb_texture.Text = info.TextureFile;
			tb_geometry.Text = info.GeometryFile;

			//if( !cacheexists )
				//b_go.Text = "Generate Pages";
		}

		private void b_go_Click( object sender, EventArgs e )
		{
			if( !cacheexists )
			{
				Hide();
				using( TileGenProgress gen = new TileGenProgress( DemoInfo ) )
					if( gen.ShowDialog() == DialogResult.OK )
					{
						cacheexists = true;
						
						cb_vt_size.Enabled = false;
						cb_vt_tilesize.Enabled = false;
						cb_vt_bordersize.Enabled = false;

						b_delete.Enabled = true;
						b_go.Text = "Start";
						b_go.Select();
					}

				Show();
			}

			else
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void b_delete_Click( object sender, EventArgs e )
		{
			string cachename = tb_texture.Text + ".cache";
			File.Delete( cachename );

			cacheexists = false;
			b_delete.Enabled = false;

			cb_vt_size.Enabled = true;
			cb_vt_tilesize.Enabled = true;
			cb_vt_bordersize.Enabled = true;

			b_go.Text = "Generate Pages";
			b_go.Select();
		}

		private void OpenFile( string filter, Control destination )
		{
			using( OpenFileDialog ofd = new OpenFileDialog() )
			{
				ofd.Filter = filter;
				ofd.InitialDirectory = Environment.CurrentDirectory + "\\Data";

				if( ofd.ShowDialog() == DialogResult.OK )
					destination.Text = ofd.FileName;
			}
		}

		void SetToolTip( string text, params Control[] controls )
		{
			foreach( Control control in controls )
				toolTip1.SetToolTip( control, text );
		}
	}
}
