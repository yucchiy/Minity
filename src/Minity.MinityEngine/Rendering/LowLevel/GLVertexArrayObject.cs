using System;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public class GLVertexArrayObject : IDisposable
    {
        public int Handle { get; }

        public GLVertexArrayObject()
        {
            Handle = GL.GenVertexArray();
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void BindAttribute<T>(int index, int size, VertexAttribPointerType pointerType, GLBufferObject<T> bufferObject, bool normalized, int stride, int offset) where T :struct
        {
            Bind();
            bufferObject.Bind();
            GL.VertexAttribPointer(index, size, pointerType, normalized, stride, offset);
            GL.EnableVertexAttribArray(index);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(Handle);
        }
    }
}