using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Feesh
{
    public class Texture
    {
        public int Handle;

        public Texture(string filePath)
        {
            //Check if file exist
            if (!File.Exists("Assets/Textures/" + filePath))
            {
               Utils.WriteLine("file not found at Assets/Textures/" + filePath, ConsoleColor.DarkRed);
               throw new FileNotFoundException("file not found at Assets/Textures/" + filePath);
            }

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Image image = Image.FromFile("Assets/Textures/" + filePath);

            image.RotateFlip(RotateFlipType.Rotate180FlipX);
            Bitmap bmp = new Bitmap(image);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.ClampToBorder);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);
            Handle = id;
        }
    }
}

