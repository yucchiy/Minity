using System;
using Minity.MinityEngine;
using OpenTK.Graphics.OpenGL4;

using Minity.MinityEngine.Rendering.LowLevel;

namespace Minity.App.Exercise.FirstTriangle
{
    public class FirstTriangleScene : IScene, ISetupable, IRenderable, IDisposable
    {
        private GLBufferObject<float> VertexBuffer { get; set; }
        private GLVertexArrayObject VertexArray { get; set; } 
        private GLProgram Program { get; set; }


        private readonly float[] Vertices = new float[]
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        };

        public void Setup()
        {
            GL.ClearColor(0.8f, 0.8f, 0.8f, 1.0f);

            VertexBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Vertices, BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);

            var vertexShaderResource = new EmbeddedResource("src/Minity.App/Exercise001.FirstTriangle/vert.glsl");
            var fragmentShaderResource = new EmbeddedResource("src/Minity.App/Exercise001.FirstTriangle/frag.glsl");

            var vertexShader = new GLShader(vertexShaderResource.Stream, ShaderType.VertexShader);
            var fragmentShader = new GLShader(fragmentShaderResource.Stream, ShaderType.FragmentShader);

            Program = new GLProgram(vertexShader, fragmentShader);
        }

        public void Dispose()
        {
            VertexArray.Dispose();
            VertexBuffer.Dispose();
            Program.Dispose();
        }

        public void Render(double deltaTime)
        {
            Program.Use();
            VertexArray.Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}