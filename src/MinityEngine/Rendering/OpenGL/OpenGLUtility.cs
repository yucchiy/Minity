using OpenTK.Graphics.OpenGL4;
using MinityEngine;

namespace MinityEngine.Rendering.OpenGL
{
    public static class OpenGLUtility
    {
        public static BufferUsageHint GetBufferUsageHint(BufferUsage bufferUsage)
        {
            switch (bufferUsage)
            {
                case BufferUsage.Static:
                    return BufferUsageHint.StaticDraw;
                case BufferUsage.Dynamic:
                case BufferUsage.Stream:
                    return BufferUsageHint.DynamicDraw;
                default:
                    return BufferUsageHint.DynamicDraw;
            }
        }

        public static BufferTarget GetBufferTarget(BufferType bufferType)
        {
            switch (bufferType)
            {
                case BufferType.IndexBuffer:
                    return BufferTarget.ElementArrayBuffer;
                case BufferType.VertexBuffer:
                    return BufferTarget.ArrayBuffer;
            }

            return BufferTarget.ArrayBuffer;
        }

        public static ShaderType GetShaderType(ShaderStage shaderStage)
        {
            switch (shaderStage)
            {
                case ShaderStage.VertexStage:
                    return ShaderType.VertexShader;
                case ShaderStage.FragmentStage:
                    return ShaderType.FragmentShader;
            }

            return ShaderType.VertexShader;
        }

        public static VertexAttribPointerType GetVertexAttribPointerType(VertexElementFormat format)
        {
            switch (format)
            {
                case VertexElementFormat.Float1:
                case VertexElementFormat.Float2:
                case VertexElementFormat.Float3:
                case VertexElementFormat.Float4:
                    return VertexAttribPointerType.Float;
                case VertexElementFormat.Half1:
                case VertexElementFormat.Half2:
                case VertexElementFormat.Half4:
                    return VertexAttribPointerType.HalfFloat;
                case VertexElementFormat.Byte2_Norm:
                case VertexElementFormat.Byte4_Norm:
                    return VertexAttribPointerType.UnsignedByte;
                case VertexElementFormat.Byte2:
                case VertexElementFormat.Byte4:
                    return VertexAttribPointerType.UnsignedByte;
                case VertexElementFormat.SByte2_Norm:
                case VertexElementFormat.SByte4_Norm:
                    return VertexAttribPointerType.Byte;
                case VertexElementFormat.SByte2:
                case VertexElementFormat.SByte4:
                    return VertexAttribPointerType.Byte;
                case VertexElementFormat.UShort2_Norm:
                case VertexElementFormat.UShort4_Norm:
                    return VertexAttribPointerType.UnsignedShort;
                case VertexElementFormat.UShort2:
                case VertexElementFormat.UShort4:
                    return VertexAttribPointerType.UnsignedShort;
                case VertexElementFormat.Short2_Norm:
                case VertexElementFormat.Short4_Norm:
                    return VertexAttribPointerType.Short;
                case VertexElementFormat.Short2:
                case VertexElementFormat.Short4:
                    return VertexAttribPointerType.Short;
                case VertexElementFormat.UInt1:
                case VertexElementFormat.UInt2:
                case VertexElementFormat.UInt3:
                case VertexElementFormat.UInt4:
                    return VertexAttribPointerType.UnsignedInt;
                case VertexElementFormat.Int1:
                case VertexElementFormat.Int2:
                case VertexElementFormat.Int3:
                case VertexElementFormat.Int4:
                    return VertexAttribPointerType.Int;
                default:
                    return VertexAttribPointerType.Int;
            }
        }

        public static int GetElementCount(VertexElementFormat format)
        {
            switch (format)
            {
                case VertexElementFormat.Float1:
                case VertexElementFormat.UInt1:
                case VertexElementFormat.Int1:
                case VertexElementFormat.Half1:
                    return 1;
                case VertexElementFormat.Float2:
                case VertexElementFormat.Byte2_Norm:
                case VertexElementFormat.Byte2:
                case VertexElementFormat.SByte2_Norm:
                case VertexElementFormat.SByte2:
                case VertexElementFormat.UShort2_Norm:
                case VertexElementFormat.UShort2:
                case VertexElementFormat.Short2_Norm:
                case VertexElementFormat.Short2:
                case VertexElementFormat.UInt2:
                case VertexElementFormat.Int2:
                case VertexElementFormat.Half2:
                    return 2;
                case VertexElementFormat.Float3:
                case VertexElementFormat.UInt3:
                case VertexElementFormat.Int3:
                    return 3;
                case VertexElementFormat.Float4:
                case VertexElementFormat.Byte4_Norm:
                case VertexElementFormat.Byte4:
                case VertexElementFormat.SByte4_Norm:
                case VertexElementFormat.SByte4:
                case VertexElementFormat.UShort4_Norm:
                case VertexElementFormat.UShort4:
                case VertexElementFormat.Short4_Norm:
                case VertexElementFormat.Short4:
                case VertexElementFormat.UInt4:
                case VertexElementFormat.Int4:
                case VertexElementFormat.Half4:
                    return 4;
                default:
                    return 0;
            }
        }

        public static uint GetSizeInBytes(VertexElementFormat format)
        {
            switch (format)
            {
                case VertexElementFormat.Byte2_Norm:
                case VertexElementFormat.Byte2:
                case VertexElementFormat.SByte2_Norm:
                case VertexElementFormat.SByte2:
                case VertexElementFormat.Half1:
                    return 2;
                case VertexElementFormat.Float1:
                case VertexElementFormat.UInt1:
                case VertexElementFormat.Int1:
                case VertexElementFormat.Byte4_Norm:
                case VertexElementFormat.Byte4:
                case VertexElementFormat.SByte4_Norm:
                case VertexElementFormat.SByte4:
                case VertexElementFormat.UShort2_Norm:
                case VertexElementFormat.UShort2:
                case VertexElementFormat.Short2_Norm:
                case VertexElementFormat.Short2:
                case VertexElementFormat.Half2:
                    return 4;
                case VertexElementFormat.Float2:
                case VertexElementFormat.UInt2:
                case VertexElementFormat.Int2:
                case VertexElementFormat.UShort4_Norm:
                case VertexElementFormat.UShort4:
                case VertexElementFormat.Short4_Norm:
                case VertexElementFormat.Short4:
                case VertexElementFormat.Half4:
                    return 8;
                case VertexElementFormat.Float3:
                case VertexElementFormat.UInt3:
                case VertexElementFormat.Int3:
                    return 12;
                case VertexElementFormat.Float4:
                case VertexElementFormat.UInt4:
                case VertexElementFormat.Int4:
                    return 16;
                default:
                    return 0;
            }
        }

        public static CullFaceMode GetCullFaceMode(FaceCullMode faceCullMode)
        {
            switch (faceCullMode)
            {
                case FaceCullMode.Back:
                    return CullFaceMode.Back;
                case FaceCullMode.Front:
                    return CullFaceMode.Front;
                default:
                    throw new IllegalValueException<FaceCullMode>();
            }
        }

        public static FrontFaceDirection GetFrontFaceDirection(FrontFace frontFace)
        {
            switch (frontFace)
            {
                case FrontFace.Clockwise:
                    return FrontFaceDirection.Cw;
                case FrontFace.CounterClockwise:
                    return FrontFaceDirection.Ccw;
                default:
                    throw new IllegalValueException<FrontFace>();
            }
        }

        public static PrimitiveType GetPrimitiveType(PrimitiveTopology primitiveTopology)
        {
            switch (primitiveTopology)
            {
                case PrimitiveTopology.TriangleList:
                    return PrimitiveType.Triangles;
                case PrimitiveTopology.TriangleStrip:
                    return PrimitiveType.TriangleStrip;
                case PrimitiveTopology.LineList:
                    return PrimitiveType.Lines;
                case PrimitiveTopology.LineStrip:
                    return PrimitiveType.LineStrip;
                case PrimitiveTopology.PointList:
                    return PrimitiveType.Points;
                default:
                    throw new IllegalValueException<PrimitiveTopology>();
            }
        }

        public static void CheckError()
        {
            var errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                throw new OpenGLException($"glGetError indicated an error: {errorCode}");
            }
        }
    }
}