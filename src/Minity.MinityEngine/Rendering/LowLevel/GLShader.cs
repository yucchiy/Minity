using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public class GLShader : IDisposable
    {
        public ShaderType Type { get; }
        public int Handle { get; }

        public GLShader(Stream stream, ShaderType shaderType)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                Handle = GL.CreateShader(shaderType);
                GL.ShaderSource(Handle, reader.ReadToEnd());
                GL.CompileShader(Handle);

                var log = GL.GetShaderInfoLog(Handle);
                if (!string.IsNullOrEmpty(log)) throw new System.ArgumentException($"Shader Error: {log}");
            }
        }

        public void Dispose()
        {
            GL.DeleteShader(Handle);
        }
    }
}