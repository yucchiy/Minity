using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public class GLTexture2D : GLTexture
    {
        public int Width { get; }
        public int Height { get; }
        public override TextureTarget TextureTarget => TextureTarget.Texture2D;

        public GLTexture2D(int width, int height, int level, byte[] data) : base(level)
        {
            Bind();
            Width = width;
            Height = height;
            GL.TexImage2D(TextureTarget, Level, PixelInternalFormat.Rgb, Width, Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, data);
        }

        public override void Dispose()
        {
            GL.DeleteTexture(Handle);
        }
    }
}