using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Minity.MinityEngine.Rendering;
using Minity.MinityEngine.Rendering.LowLevel;

namespace Minity.MinityEngine
{
    public class Cube : System.IDisposable
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

        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero;
        public Vector3 Scale { get; set; } = Vector3.One;

        private readonly float[] Vertices = new float[]
        {
            // front
            -0.5f, -0.5f,  0.5f,
            0.5f, -0.5f,  0.5f,
            0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            // top
            -0.5f,  0.5f,  0.5f,
            0.5f,  0.5f,  0.5f,
            0.5f,  0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,
            // back
            0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,
            0.5f,  0.5f, -0.5f,
            // bottom
            -0.5f, -0.5f, -0.5f,
            0.5f, -0.5f, -0.5f,
            0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            // left
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f,
            // right
            0.5f, -0.5f,  0.5f,
            0.5f, -0.5f, -0.5f,
            0.5f,  0.5f, -0.5f,
            0.5f,  0.5f,  0.5f,
        };

        private readonly float[] Normals = new float[]
        {
            // front
            0f, 0f, 1f,
            0f, 0f, 1f,
            0f, 0f, 1f,
            0f, 0f, 1f,
            // top
            0f, 1f, 0f,
            0f, 1f, 0f,
            0f, 1f, 0f,
            0f, 1f, 0f,
            // back
            0f, 0f, -1f,
            0f, 0f, -1f,
            0f, 0f, -1f,
            0f, 0f, -1f,
            // bottom
            0f, -1f, 0f,
            0f, -1f, 0f,
            0f, -1f, 0f,
            0f, -1f, 0f,
            // left
            -1f, 0f, 0f,
            -1f, 0f, 0f,
            -1f, 0f, 0f,
            -1f, 0f, 0f,
            // right
            1f, 0f, 0f,
            1f, 0f, 0f,
            1f, 0f, 0f,
            1f, 0f, 0f,
        };

        private readonly float[] UVs = new float[]
        {
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,

            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,

            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,

            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,

            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,

            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,
        };

        private readonly uint[] Indices = new uint[]
        {
            // front
            0,  1,  2,
            2,  3,  0,
            // top
            4,  5,  6,
            6,  7,  4,
            // back
            8,  9, 10,
            10, 11,  8,
            // bottom
            12, 13, 14,
            14, 15, 12,
            // left
            16, 17, 18,
            18, 19, 16,
            // right
            20, 21, 22,
            22, 23, 20,
        };

        public Cube(ICamera camera)
        {
            VertexBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Vertices, BufferUsageHint.StaticDraw);
            NormalBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Normals, BufferUsageHint.StaticDraw);
            UVBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, UVs, BufferUsageHint.StaticDraw);
            IndexBuffer = new GLBufferObject<uint>(BufferTarget.ElementArrayBuffer, Indices, BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(1, 2, VertexAttribPointerType.Float, UVBuffer, false, 2 * UVBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(2, 3, VertexAttribPointerType.Float, NormalBuffer, false, 3 * NormalBuffer.DataSize, 0);

            var vertexShaderResource = new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/cube_vert.glsl");
            var fragmentShaderResource = new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/cube_frag.glsl");

            var vertexShader = new GLShader(vertexShaderResource.Stream, ShaderType.VertexShader);
            var fragmentShader = new GLShader(fragmentShaderResource.Stream, ShaderType.FragmentShader);

            Program = new GLProgram(vertexShader, fragmentShader);
            Texture = new Texture2D(new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/cube.png"));

            Program.Use();

            UniformModel = Program.GetUniform("ModelMatrix");
            UniformView = Program.GetUniform("ViewMatrix");
            UniformProjection = Program.GetUniform("ProjectionMatrix");

            ModelMatrix = Matrix4.Identity;
            ViewMatrix = Matrix4.Identity;
            ProjectionMatrix = Matrix4.Identity;

            Camera = camera;
        }

        public Cube(ICamera camera, GLProgram program)
        {
            VertexBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Vertices, BufferUsageHint.StaticDraw);
            NormalBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, Normals, BufferUsageHint.StaticDraw);
            UVBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, UVs, BufferUsageHint.StaticDraw);
            IndexBuffer = new GLBufferObject<uint>(BufferTarget.ElementArrayBuffer, Indices, BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(1, 2, VertexAttribPointerType.Float, UVBuffer, false, 2 * UVBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(2, 3, VertexAttribPointerType.Float, NormalBuffer, false, 3 * NormalBuffer.DataSize, 0);

            Program = program;
            Texture = new Texture2D(new EmbeddedResource("src/Minity.MinityEngine/Rendering/Resources/cube.png"));

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
            VertexBuffer.Dispose();
            Program.Dispose();
        }

        public void Render(double deltaTime)
        {
            Program.Use();
            Texture.Bind();

            // Bad implementation
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateTranslation(Position) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z);
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