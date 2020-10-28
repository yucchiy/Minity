using System;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public abstract class GLTexture : IDisposable
    {
        public int Handle { get; }
        public int Level { get; }
        public abstract TextureTarget TextureTarget { get; }

        internal GLTexture(int level)
        {
            Level = level;
            Handle = GL.GenTexture();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget, Handle);
        }

        public void Bind(TextureUnit textureUnit)
        {
            GL.ActiveTexture(textureUnit);
            Bind();
        }

        public void SetParameter(TextureParameterName parameterName, int value)
        {
            GL.TexParameter(TextureTarget, parameterName, value);
        }

        public abstract void Dispose();
    }
}