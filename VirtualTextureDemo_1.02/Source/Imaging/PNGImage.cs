namespace VirtualTextureDemo
{
    using System;
    using System.IO;
    using System.Drawing;
    
    public class PNGImage : IImage
    {
        public int			Width		{ get; protected set; }
        public int			Height		{ get; protected set; }
        public int			Channels	{ get; protected set; }
        public Rectangle	Bounds		{ get; protected set; }

        private byte[] Data;
        
        public PNGImage(string filename)
        {
            using (Bitmap bitmap = new Bitmap(filename))
            {
                Width = bitmap.Width;
                Height = bitmap.Height;
                Channels = 4;
                //Console.WriteLine($"图像尺寸: {Width}x{Height}");
                Data = new byte[Width * Height * Channels];
                // 遍历像素
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        int offset = 4 * (y * Width + x);
                        Data[offset + 0] = pixel.R;
                        Data[offset + 1] = pixel.G;
                        Data[offset + 2] = pixel.B;
                        Data[offset + 3] = pixel.A;
                    }
                }
            }
            //TileGenerator.CreateBitmap(Data, Width, Height, "LoadedSource.png");
        }
        
        public void Dispose()
        {
            
        }

        public void CopyTo(SimpleImage image, Point dst_offset, Rectangle src_rect)
        {
            for (int y = 0; y < src_rect.Height; y++)
            {
                for (int x = 0; x < src_rect.Width; x++)
                {
                    int dstX = x + dst_offset.X;
                    int dstY = y + dst_offset.Y;
                    
                    int srcX = src_rect.X + x;
                    int srcY = src_rect.Y + y;

                    for (int channel = 0; channel < Channels; channel++)
                    {
                        image.Data[(dstY * image.Width + dstX) * Channels + channel] = Data[(srcY * Width + srcX) * Channels + channel];    
                    }
                }
            }
        }
    }
}