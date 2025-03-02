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

	public class TargaImage: RawImage
	{
		[StructLayout(LayoutKind.Sequential,Pack=1,Size=18)]
		public struct Header
		{
			public byte		IdLength;
			public byte		ColorMapType;
			public byte		DataTypeCode;
			public short	ColorMapOrigin;
			public short	ColorMapLength;
			public byte		ColorMapDepth;
			public short	OriginX;
			public short	OriginY;
			public short	Width;
			public short	Height;
			public byte		BitsPerPixel;
			public byte		ImageDescriptor;
		}

		public TargaImage( string filename )
		{
			int headersize = Marshal.SizeOf(typeof(Header));

			filesize = new FileInfo( filename ).Length;
			file = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess );

			Header header = Memory.Read<Header>( file );
			Width    = header.Width;
			Height   = header.Height;
			Channels = header.BitsPerPixel / 8;

			Bounds = new Rectangle( 0, 0, Width, Height );

			if( header.DataTypeCode != 2 && header.DataTypeCode != 3 )
				throw new Exception( string.Format( "Data type of targa is not 2 [{0}:{1}]", header.DataTypeCode, filename ) );

			datapos = headersize;
			flip = true;
		}
	}
}