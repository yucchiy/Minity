using System;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public class GLProgram : IDisposable
    {
        public GLShader VertexShader { get; }
        public GLShader FragmentShader { get; }
        public int Handle { get; }

        public GLProgram(GLShader vertexShader, GLShader fragmentShader)
        {
            VertexShader = vertexShader;
            FragmentShader = fragmentShader;

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, VertexShader.Handle);
            GL.AttachShader(Handle, FragmentShader.Handle);

            GL.LinkProgram(Handle);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public void Dispose()
        {
            GL.DeleteProgram(Handle);
        }
    }
}