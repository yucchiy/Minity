using System;
using Minity.MinityEngine;
using OpenTK.Graphics.OpenGL4;

using Minity.MinityEngine.Rendering;
using Minity.MinityEngine.Rendering.LowLevel;

namespace Minity.App.Exercise.Texture
{
    public class MainScene : IScene, ISetupable, IRenderable, IDisposable
    {
        private GLBufferObject<float> VertexBuffer { get; set; }
        private GLBufferObject<float> UvBuffer { get; set; }
        private GLBufferObject<uint> IndexBuffer { get; set; }
        private GLVertexArrayObject VertexArray { get; set; }
        private GLProgram Program { get; set; }
        private Texture2D Texture { get; set; }

        private readonly float[] Vertices = new float[]
        {
            -0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
             0.5f,  0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f,
        };

        private readonly float[] Uvs = new float[]
        {
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,
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
            UvBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Uvs, BufferUsageHint.StaticDraw);
            IndexBuffer = new GLBufferObject<uint>(BufferTarget.ElementArrayBuffer, Indices, BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(1, 2, VertexAttribPointerType.Float, UvBuffer, false, 2 * UvBuffer.DataSize, 0);

            var vertexShaderResource = new EmbeddedResource("src/Minity.App/Exercise005.Texture/vert.glsl");
            var fragmentShaderResource = new EmbeddedResource("src/Minity.App/Exercise005.Texture/frag.glsl");

            var vertexShader = new GLShader(vertexShaderResource.Stream, ShaderType.VertexShader);
            var fragmentShader = new GLShader(fragmentShaderResource.Stream, ShaderType.FragmentShader);

            Program = new GLProgram(vertexShader, fragmentShader);

            Texture = new Texture2D(new EmbeddedResource("src/Minity.App/Resources/crate.png"));
        }

        public void Dispose()
        {
            IndexBuffer.Dispose();
            VertexArray.Dispose();
            VertexBuffer.Dispose();
            Program.Dispose();
        }

        public void Render(double deltaTime)
        {
            Program.Use();
            VertexArray.Bind();
            IndexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, IndexBuffer.DataCount, DrawElementsType.UnsignedInt, 0);
        }
    }
}
