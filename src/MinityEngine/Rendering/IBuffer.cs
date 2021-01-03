using System;

namespace MinityEngine.Rendering
{
    public interface IBuffer
    {
        uint SizeInBytes { get; }
        BufferUsage Usage { get; }
        BufferType Type { get; }

        void Bind();
        void UpdateBuffer(IntPtr pointer, uint sizeInBytes);
    }
}