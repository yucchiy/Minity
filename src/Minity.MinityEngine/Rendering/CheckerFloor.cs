using System;
using System.Linq;
using Minity.MinityEngine.Rendering.LowLevel;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Minity.MinityEngine.Rendering
{
    public class CheckerFloor : IDisposable
    {
        private GLBufferObject<float> VertexBuffer { get; }
        private GLBufferObject<float> NormalBuffer { get; }
        private GLBufferObject<float> UVBuffer { get; }
        private GLBufferObject<uint> IndexBuffer { get; }
        private GLVertexArrayObject VertexArray { get; }
        private GLProgram Program { get; }
        private Texture2D Texture { get; }
        private ICamera Camera { get; }

        private GLUniform UniformModel { get; }
        private GLUniform UniformView { get; }
        private GLUniform UniformProjection { get; }

        private Matrix4 ModelMatrix;
        private Matrix4 ViewMatrix;
        private Matrix4 ProjectionMatrix;

        private readonly float[] Vertices = new float[]
        {
            -0.5f, 0f, -0.5f,
             0.5f, 0f, -0.5f,
             0.5f, 0f,  0.5f,
            -0.5f, 0f,  0.5f,
        };

        private readonly float[] Normals = new float[]
        {
            0f, 1f, 0f,
            0f, 1f, 0f,
            0f, 1f, 0f,
            0f, 1f, 0f,
        };

        private readonly float[] UVs = new float[]
        {
            0.0f, 1f - 0.0f,
            1.0f, 1f - 0.0f,
            1.0f, 1f - 1.0f,
            0.0f, 1f - 1.0f,
        };

        private readonly uint[] Indices = new uint[]
        {
            0, 1, 2,
            0, 2, 3,
        };

        public CheckerFloor(float size, ICamera camera)
        {
            var vertices = Vertices.Select(v => v * size).ToArray();
            var uvs = UVs.Select(v => v * size).ToArray();

            VertexBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, vertices, BufferUsageHint.StaticDraw);
            NormalBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Normals, BufferUsageHint.StaticDraw);
            UVBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, uvs, BufferUsageHint.StaticDraw);
            IndexBuffer = new GLBufferObject<uint>(BufferTarget.ElementArrayBuffer, Indices, BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(1, 2, VertexAttribPointerType.Float, UVBuffer, false, 2 * UVBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(2, 3, VertexAttribPointerType.Float, NormalBuffer, false, 3 * NormalBuffer.DataSize, 0);

            var vertexShaderResource = new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/checkerfloor_vert.glsl");
            var fragmentShaderResource = new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/checkerfloor_frag.glsl");

            var vertexShader = new GLShader(vertexShaderResource.Stream, ShaderType.VertexShader);
            var fragmentShader = new GLShader(fragmentShaderResource.Stream, ShaderType.FragmentShader);

            Program = new GLProgram(vertexShader, fragmentShader);
            Texture = new Texture2D(new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/checkerfloor.jpg"));

            Program.Use();

            UniformModel = Program.GetUniform("ModelMatrix");
            UniformView = Program.GetUniform("ViewMatrix");
            UniformProjection = Program.GetUniform("ProjectionMatrix");

            ModelMatrix = Matrix4.Identity;
            ViewMatrix = Matrix4.Identity;
            ProjectionMatrix = Matrix4.Identity;

            Camera = camera;
        }

        public CheckerFloor(float size, ICamera camera, GLProgram program)
        {
            var vertices = Vertices.Select(v => v * size).ToArray();
            var uvs = UVs.Select(v => v * size).ToArray();

            VertexBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, vertices, BufferUsageHint.StaticDraw);
            NormalBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Normals, BufferUsageHint.StaticDraw);
            UVBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, uvs, BufferUsageHint.StaticDraw);
            IndexBuffer = new GLBufferObject<uint>(BufferTarget.ElementArrayBuffer, Indices, BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(1, 2, VertexAttribPointerType.Float, UVBuffer, false, 2 * UVBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(2, 3, VertexAttribPointerType.Float, NormalBuffer, false, 3 * NormalBuffer.DataSize, 0);

            Texture = new Texture2D(new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/checkerfloor.jpg"));
            Program = program;

            Program.Use();
            UniformModel = Program.GetUniform("ModelMatrix");
            UniformView = Program.GetUniform("ViewMatrix");
            UniformProjection = Program.GetUniform("ProjectionMatrix");

            ModelMatrix = Matrix4.Identity;
            ViewMatrix = Matrix4.Identity;
            ProjectionMatrix = Matrix4.Identity;

            Camera = camera;
        }

        public void Dispose()
        {
            IndexBuffer.Dispose();
            VertexArray.Dispose();
            UVBuffer.Dispose();
            NormalBuffer.Dispose();
            VertexBuffer.Dispose();
            Program.Dispose();
        }

        public void Render(double deltaTime)
        {
            Program.Use();
            Texture.Bind();

            Camera.GetViewMatrix(out ViewMatrix);
            Camera.GetProjectionMatrix(out ProjectionMatrix);

            UniformModel.Matrix4(false, ref ModelMatrix);
            UniformView.Matrix4(false, ref ViewMatrix);
            UniformProjection.Matrix4(false, ref ProjectionMatrix);

            VertexArray.Bind();
            IndexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, IndexBuffer.DataCount, DrawElementsType.UnsignedInt, 0);
        }
    }
}