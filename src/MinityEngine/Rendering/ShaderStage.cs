using System;

namespace MinityEngine.Rendering
{
    [Flags]
    public enum ShaderStage : byte
    {
        None = 0,
        VertexStage = 1 << 0,
        FragmentStage = 1 << 1,
    }
}