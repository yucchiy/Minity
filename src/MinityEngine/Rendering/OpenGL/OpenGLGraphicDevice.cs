using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace MinityEngine.Rendering.OpenGL
{
    public class OpenGLGraphicDevice : IGraphicsDevice, IDisposable
    {
        private OpenGLPipelineState CurrentPipelineState { get; set; }

        public OpenGLGraphicDevice()
        {
        }

        public IBuffer CreateBuffer(in BufferDescription description) => new OpenGLBuffer(description);
        public IShader CreateShader(in ShaderDescription description) => new OpenGLShader(description);
        public IPipelineState CreatePipelineState(in PipelineStateDescription description) => new OpenGLPipelineState(description);

        public void BindBuffer(IBuffer buffer)
        {
            buffer.Bind();
        }
        public void BindPipelineState(IPipelineState pipelineState)
        {
            pipelineState.Bind();

            CurrentPipelineState = pipelineState as OpenGLPipelineState;
        }

        public unsafe void UpdateBuffer<T>(IBuffer buffer, T[] data) where T : unmanaged
        {
            UpdateBuffer(buffer, (ReadOnlySpan<T>)data);
        }

        private unsafe void UpdateBuffer<T>(IBuffer buffer, ReadOnlySpan<T> data) where T : unmanaged
        {
            fixed (void *pin = &MemoryMarshal.GetReference(data))
            {
                buffer.UpdateBuffer((IntPtr)pin, (uint)(data.Length * Marshal.SizeOf<T>()));
            }
        }

        public void Draw(uint count, uint start)
        {
            GL.DrawArrays(CurrentPipelineState.PrimitiveType, (int)start, (int)count);
            OpenGLUtility.CheckError();
        }
        public void DrawIndexed(uint count, uint start)
        {
            GL.DrawElements(CurrentPipelineState.PrimitiveType, (int)count, DrawElementsType.UnsignedInt, (int)start);
            OpenGLUtility.CheckError();
        }

        public void Dispose()
        {
        }
    }
}