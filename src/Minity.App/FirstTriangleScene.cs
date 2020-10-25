using System;
using Minity.MinityEngine;
using OpenTK.Graphics.OpenGL4;

namespace Minity.App
{
    
    public class FirstTriangleScene : IScene, IDisposable
    {
        private int VertexBufferObject { get; set; }
        private int VertexArrayObject { get; set; } 

        private readonly float[] Vertices = new float[]
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        };

        public void Setup()
        {
            GL.ClearColor(0.8f, 0.8f, 0.8f, 1.0f);

            VertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticRead);
        }

        public void Dispose()
        {
        }

        public void Update(double deltaTime)
        {
        }

        public void Render(double deltaTime)
        {
        }
    }
}