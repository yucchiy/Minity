using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Minity.ObjLoader;
using Minity.MinityEngine.Rendering.LowLevel;
using System.Collections.Generic;

namespace Minity.MinityEngine
{
    public class ObjModel : System.IDisposable
    {
        private GLBufferObject<float> VertexBuffer { get; }
        private GLBufferObject<float> NormalBuffer { get; }
        private GLBufferObject<uint> IndexBuffer { get; }
        private GLVertexArrayObject VertexArray { get; }
        private GLProgram Program { get; }
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

        public ObjModel(Obj obj, ICamera camera, GLProgram program)
        {
            VertexBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, GetVertices(obj), BufferUsageHint.StaticDraw);
            NormalBuffer = new GLBufferObject<float>(BufferTarget.ArrayBuffer, GetNormals(obj), BufferUsageHint.StaticDraw);
            IndexBuffer = new GLBufferObject<uint>(BufferTarget.ElementArrayBuffer, GetIndices(obj), BufferUsageHint.StaticDraw);

            VertexArray = new GLVertexArrayObject();
            VertexArray.BindAttribute<float>(0, 3, VertexAttribPointerType.Float, VertexBuffer, false, 3 * VertexBuffer.DataSize, 0);
            VertexArray.BindAttribute<float>(1, 3, VertexAttribPointerType.Float, NormalBuffer, false, 3 * NormalBuffer.DataSize, 0);

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
            VertexBuffer.Dispose();
        }

        private float[] GetVertices(Obj obj)
        {
            var vertices = new List<float>();
            for (var i = 0; i < obj.Faces.Length; ++i)
            {
                for (var j = 2; j < obj.Faces[i].VertexIndices.Length; ++j)
                {
                    var vertex1 = obj.Vertices[obj.Faces[i].VertexIndices[j - 2] - 1];
                    vertices.Add(vertex1.X);
                    vertices.Add(vertex1.Y);
                    vertices.Add(vertex1.Z);

                    var vertex2 = obj.Vertices[obj.Faces[i].VertexIndices[j - 1] - 1];
                    vertices.Add(vertex2.X);
                    vertices.Add(vertex2.Y);
                    vertices.Add(vertex2.Z);

                    var vertex3 = obj.Vertices[obj.Faces[i].VertexIndices[j - 0] - 1];
                    vertices.Add(vertex3.X);
                    vertices.Add(vertex3.Y);
                    vertices.Add(vertex3.Z);
                }
            }

            return vertices.ToArray();
        }

        private float[] GetNormals(Obj obj)
        {
            var normals = new List<float>();
            for (var i = 0; i < obj.Faces.Length; ++i)
            {
                for (var j = 2; j < obj.Faces[i].NormalIndices.Length; ++j)
                {
                    var normal1 = obj.Normals[obj.Faces[i].NormalIndices[j - 2] - 1];
                    normals.Add(normal1.X);
                    normals.Add(normal1.Y);
                    normals.Add(normal1.Z);

                    var normal2 = obj.Normals[obj.Faces[i].NormalIndices[j - 1] - 1];
                    normals.Add(normal2.X);
                    normals.Add(normal2.Y);
                    normals.Add(normal2.Z);

                    var normal3 = obj.Normals[obj.Faces[i].NormalIndices[j - 0] - 1];
                    normals.Add(normal3.X);
                    normals.Add(normal3.Y);
                    normals.Add(normal3.Z);
                }
            }
            
            return normals.ToArray();
        }

        private uint[] GetIndices(Obj obj)
        {
            var indices = new List<uint>();
            uint index = 0;
            for (var i = 0; i < obj.Faces.Length; ++i)
            {
                for (var j = 2; j < obj.Faces[i].NormalIndices.Length; ++j)
                {
                    for (var k = 0; k < 9; ++k)
                    {
                        indices.Add(index++);
                    }
                }
            }
            
            return indices.ToArray();
        }

        public void Render(double deltaTime)
        {
            Program.Use();

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