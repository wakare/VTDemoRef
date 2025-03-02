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

namespace VirtualTextureDemo
{
	using System;
	using System.IO;
	using System.Drawing;
	using System.Runtime.InteropServices;

	public interface IImage: IDisposable
	{
		int			Width			{ get; }
		int			Height			{ get; }
		int			Channels		{ get; }
		Rectangle	Bounds			{ get; }

		void CopyTo( SimpleImage image, Point offset, Rectangle rect );
	}

	public class RawImage: IImage
	{
		public int			Width		{ get; protected set; }
		public int			Height		{ get; protected set; }
		public int			Channels	{ get; protected set; }
		public Rectangle	Bounds		{ get; protected set; }

		protected FileStream		file;
		protected long				datapos;
		protected bool				flip;
		protected long				filesize;

		protected RawImage()
		{}

		public RawImage( int width, int height, int channels, string filename )
		{
			Width    = width;
			Height   = height;
			Channels = channels;

			Bounds = new Rectangle( 0, 0, Width, Height );

			filesize = new FileInfo( filename ).Length;

			file = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess );
		}

		public void Dispose()
		{
			file.Close();
		}

		public void CopyTo( SimpleImage image, Point dst_offset, Rectangle src_rect )
		{
			if( !Bounds.IntersectsWith( src_rect ) )
				throw new ArgumentException("No Intersection");

			if( src_rect.X < 0 ) dst_offset.X += -src_rect.X;
			if( src_rect.Y < 0 ) dst_offset.Y += -src_rect.Y;

			src_rect.Intersect( Bounds );
			
			if( src_rect.Width == 0 || src_rect.Height == 0 )
				throw new ArgumentException("No Size");

			if( src_rect.X < 0 || src_rect.Y < 0 )
				throw new ArgumentException("Rect negative");

#if DONT_FIX
			if( rect.Width > image.Width-offset.X || rect.Height > image.Height-offset.Y )
				throw new ArgumentException("Rect overlaps");
#else
			int width  = System.Math.Min( src_rect.Width,  image.Width  - dst_offset.X );
			int height = System.Math.Min( src_rect.Height, image.Height - dst_offset.Y );
#endif
			for( int j = 0; j < height; ++j )
			{
				int y = flip ? Height - (j+src_rect.Y) - 1 : j+src_rect.Y;
				long srcindex = Index( src_rect.X, y, Width, Channels );
				long destindex  = Index( dst_offset.X, j+dst_offset.Y, image.Width, Channels );
				long destlength = width * Channels;

				file.Position = datapos + srcindex;
				file.Read( image.Data, (int)destindex, (int)destlength );
			}
		}

		static long Index( long x, long y, long stride, long channels )
		{
			return ( y * stride + x ) * channels;
		}
	}
}