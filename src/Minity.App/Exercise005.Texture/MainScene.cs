using System;
using Minity.MinityEngine;
using OpenTK.Graphics.OpenGL4;

using Minity.MinityEngine.Rendering.LowLevel;

namespace Minity.App.Exercise.Uniform
{
    public class MainScene : IScene, IDisposable
    {
        private GLBufferObject<float> VertexBuffer { get; set; }
        private GLBufferObject<float> ColorBuffer { get; set; }
        private GLBufferObject<uint> IndexBuffer { get; set; }
        private GLVertexArrayObject VertexArray { get; set; }
        private GLProgram Program { get; set; }
        private GLUniform ElapsedTimeUniform { get; set; }
        private double ElapesedTime { get; set; }

        private readonly float[] Vertices = new float[]
        {
            -0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
             0.5f,  0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f,
        };

        private readonly float[] Colors = new float[]
        {
            0.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 1.0f,
        };

        private readonly uint[] Indices = new uint[]
        {
            0, 1, 2,
            0, 2, 3,
        };

        public void Setup()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            VertexBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Vertices, BufferUsageHint.StaticDraw);
            ColorBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Colors, BufferUsageHint.StaticDraw);
            IndexBuffer = new GLBufferObject<uint>(BufferTarget.ElementArrayBuffer, Indices, BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(1, 3, VertexAttribPointerType.Float, ColorBuffer, false, 3 * ColorBuffer.DataSize, 0);

            var vertexShaderResource = new EmbeddedResource("src/Minity.App/Exercise004.Uniform/vert.glsl");
            var fragmentShaderResource = new EmbeddedResource("src/Minity.App/Exercise004.Uniform/frag.glsl");

            var vertexShader = new GLShader(vertexShaderResource.Stream, ShaderType.VertexShader);
            var fragmentShader = new GLShader(fragmentShaderResource.Stream, ShaderType.FragmentShader);

            Program = new GLProgram(vertexShader, fragmentShader);

            ElapsedTimeUniform = Program.GetUniform("elapsedTime");
        }

        public void Dispose()
        {
            IndexBuffer.Dispose();
            VertexArray.Dispose();
            VertexBuffer.Dispose();
            Program.Dispose();
        }

        public void Update(double deltaTime)
        {
        }

        public void Render(double deltaTime)
        {
            Program.Use();
            VertexArray.Bind();
            IndexBuffer.Bind();
            ElapsedTimeUniform.Uniform1((float) ElapesedTime);
            GL.DrawElements(PrimitiveType.Triangles, IndexBuffer.DataCount, DrawElementsType.UnsignedInt, 0);

            ElapesedTime += deltaTime;
        }
    }
}
