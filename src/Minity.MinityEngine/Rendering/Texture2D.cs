using System;
using System.Collections.Generic;
using SkiaSharp;
using Minity.MinityEngine.Rendering.LowLevel;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine.Rendering
{
    public class Texture2D : IDisposable
    {
        public GLTexture2D Texture { get; }
        public Texture2D(EmbeddedResource resource)
        {
            var image = SKBitmap.FromImage(SKImage.FromEncodedData(resource.Stream));

            var data = new List<byte>(image.Width * image.Height * 3);
            for (var y = 0; y < image.Height; ++y)
            {
                for (var x = 0; x < image.Width; ++x)
                {
                    var pixel = image.GetPixel(x, y);
                    data.Add(pixel.Red);
                    data.Add(pixel.Green);
                    data.Add(pixel.Blue);
                }
            }

            Texture = new GLTexture2D(image.Width, image.Height, 0, data.ToArray());

            Texture.SetParameter(TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            Texture.SetParameter(TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            Texture.SetParameter(TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            Texture.SetParameter(TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        public void Bind()
        {
            Texture.Bind();
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}