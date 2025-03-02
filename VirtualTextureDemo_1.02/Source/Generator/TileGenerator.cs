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
	using System.IO;
	using System.Drawing;
	using System.Diagnostics;
	using System.Collections.Generic;

	// This class generates the pages and saves them to disk.
	// It's slow and messy, but it works.
	public class TileGenerator: System.IDisposable
	{
		readonly VirtualTextureInfo info;
		readonly PageIndexer		indexer;
		readonly TileDataFile		tiles;

		readonly int	tilesize;
		readonly int	pagesize;
		readonly bool	targafile;

		IImage	sourceimage;

		Stopwatch sw;

		SimpleImage img_page1;
		SimpleImage img_page2;
		SimpleImage img_2xtile;
		SimpleImage img_4xtile;
		SimpleImage img_tile;

		public int Count
		{
			get { return indexer.Count; }
		}

		public TileGenerator( string filename, VirtualTextureInfo info )
		{
			this.info = info;
			this.tilesize = info.TileSize;
			this.pagesize = info.PageSize;

			string cachename = filename + ".cache";

			indexer = new PageIndexer( info );
			tiles = new TileDataFile( cachename, info, FileAccess.ReadWrite );
			
			targafile = filename.Contains( ".tga" );
			if( targafile )
				sourceimage = new TargaImage( filename );	
			else
				sourceimage = new RawImage( info.VirtualTextureSize, info.VirtualTextureSize, VirtualTexture.ChannelCount, filename );

			img_page1  = new SimpleImage( pagesize, pagesize, VirtualTexture.ChannelCount );
			img_page2  = new SimpleImage( pagesize, pagesize, VirtualTexture.ChannelCount );
			img_tile   = new SimpleImage( tilesize, tilesize, VirtualTexture.ChannelCount );
			img_2xtile = new SimpleImage( tilesize*2, tilesize*2, VirtualTexture.ChannelCount );
			img_4xtile = new SimpleImage( tilesize*4, tilesize*4, VirtualTexture.ChannelCount );

			sw = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			tiles.WriteInfo();
			tiles.Dispose();
		}

		public IEnumerable<int> Generate()
		{
			int mipcount = MathHelper.Log2( info.PageTableSize );

			long time = sw.ElapsedMilliseconds;
			for( int i = 0; i < mipcount+1; ++i )
			{
				int count = ( info.VirtualTextureSize / tilesize ) >> i;
				System.Console.Write("MipLevel: {1} TileCount: {0}x{0}\t[100%]", count, i );

				for( int y = 0; y < count; ++y )
				for( int x = 0; x < count; ++x )
				{
					if( x == 0 )
						System.Console.Write("\b\b\b\b\b{0,3:###}%]", 100.0f*(y*count+x+1)/(float)(count*count));

					Page page = new Page( x, y, i );

					int index = indexer[page];
					CopyTile( img_page1, page );
					tiles.WritePage( index, img_page1.Data );

					yield return index;
				}
				System.Console.WriteLine("\b\b\b\b\b100%]");
			}
			System.Console.WriteLine("Build Time: {0}ms", sw.ElapsedMilliseconds - time);
		}

		void CopyTile( SimpleImage image, Page request )
		{	
			if( request.Mip == 0 )
			{
				int x = request.X * tilesize - info.BorderSize;
				int y = request.Y * tilesize - info.BorderSize;

				System.Drawing.Rectangle rect = new System.Drawing.Rectangle( x, y, pagesize, pagesize );
				sourceimage.CopyTo( image, Point.Empty, rect );

				if( targafile )
					SwapChannels( image );
			}

			else
			{
				int largesize = tilesize*4;

				int xpos = request.X << 1;
				int ypos = request.Y << 1;
				int mip  = request.Mip-1;

				int size = info.PageTableSize >> mip;

				for( int i = 0; i < img_4xtile.Width * img_4xtile.Height * img_4xtile.Channels; ++i )
					img_4xtile.Data[i] = 0;

				for( int y = 0; y < 4; ++y )
				for( int x = 0; x < 4; ++x )
				{
					Page page = new Page( xpos+x-1, ypos+y-1, mip );

					// Wrap so we get the border sections of other pages
					page.X = MathHelper.Modulus( page.X, size );
					page.Y = MathHelper.Modulus( page.Y, size );

					//if( page.X >= 0 && page.Y >= 0 && page.X < size && page.Y < size )
					{
						tiles.ReadPage( indexer[page], img_page2.Data );
						
						System.Drawing.Rectangle src_rect = new System.Drawing.Rectangle( info.BorderSize, info.BorderSize, tilesize, tilesize );
						Point dst_offset = new Point( x * tilesize, y * tilesize );

						SimpleImage.Copy( img_4xtile, dst_offset, img_page2, src_rect );
					}
				}

				SimpleImage.Mipmap( img_4xtile.Data, img_4xtile.Width, VirtualTexture.ChannelCount, img_2xtile.Data );

				System.Drawing.Rectangle srect = new System.Drawing.Rectangle( tilesize/2-info.BorderSize, tilesize/2-info.BorderSize, pagesize, pagesize );
				SimpleImage.Copy( image, Point.Empty, img_2xtile, srect );
			}

			/*
			// Write Targa Frames
			Directory.CreateDirectory( "Tiles" );
			FileStream file = new FileStream( string.Format( "Tiles\\tile_{0}_{1}x{2}.tga", request.Mip, request.X, request.Y ), FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.SequentialScan );
			TargaStream.Header header = new TargaStream.Header()
			{
				Width = (short)pagesize,
				Height = (short)pagesize,
				BitsPerPixel = 32,
				DataTypeCode = 2
			};

			byte[] h = Memory.RawSerialize( header );
			file.Write( h, 0, h.Length );
			file.Write( image.Data, 0, image.Data.Length );
			file.Close();
				//*/
		}

		void SwapChannels( SimpleImage image )
		{
			for( int i = 0; i < image.Width * image.Height * image.Channels; i += image.Channels )
			{
				byte temp = image.Data[i+0];
				image.Data[i+0] = image.Data[i+2];
				image.Data[i+2] = temp;
			}
		}
	}
}