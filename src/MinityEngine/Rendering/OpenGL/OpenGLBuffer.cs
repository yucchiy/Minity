using System;
using OpenTK.Graphics.OpenGL4;

namespace MinityEngine.Rendering.OpenGL
{
    public class OpenGLBuffer : IBuffer, IDisposable
    {
        public int Handle { get; }
        public uint SizeInBytes { get; }
        public BufferUsage Usage { get; }
        public BufferType Type { get; }

        public OpenGLBuffer(BufferDescription descriptor)
        {
            SizeInBytes = descriptor.SizeInBytes;
            Usage = descriptor.Usage;
            Type = descriptor.Type;

            Handle = GL.GenBuffer();
            OpenGLUtility.CheckError();
        }

        public void Bind()
        {
            GL.BindBuffer(OpenGLUtility.GetBufferTarget(Type), Handle);
            OpenGLUtility.CheckError();
        }

        public void UpdateBuffer(IntPtr pointer, uint sizeInBytes)
        {
            Bind();

            GL.BufferData(OpenGLUtility.GetBufferTarget(Type), (int)sizeInBytes, pointer, OpenGLUtility.GetBufferUsageHint(Usage));;;;
            OpenGLUtility.CheckError();
        }

        public void Dispose()
        {
            GL.DeleteBuffer(Handle);
        }
    }
}