using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public class GLBufferObject<T> : IDisposable
        where T : struct
    {
        public int Handle { get; }
        public int DataCount { get; }
        public int DataSize { get; }
        public T[] Data { get; }
        public BufferTarget BufferTarget { get; }

        public GLBufferObject(BufferTarget target, T[] data, BufferUsageHint hint)
        {
            Data = data;
            DataCount = data.Length;
            DataSize = Marshal.SizeOf<T>();
            BufferTarget = target;

            Handle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget, Handle);
            GL.BufferData(BufferTarget, DataCount * DataSize, Data, hint);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget, Handle);
        }

        public void Dispose()
        {

            GL.DeleteBuffer(Handle);
        }
    }
}