using System;
using Minity.MinityEngine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using Minity.MinityEngine.Rendering;
using Minity.MinityEngine.Rendering.LowLevel;

namespace Minity.App.Exercise.BasicLight
{
    public class MainScene : IScene, ISetupable, IRenderable, IDisposable, IResizable
    {
        private ICamera Camera { get; set; }
        private GLProgram LightProgram { get; set; }
        private GLProgram LightObjectProgram { get; set; }
        private CheckerFloor Checker { get; set; }
        private Cube Cube { get; set; }
        private Cube LightCube { get; set; }
        private GLUniform LightPositionUniform { get; set; }
        private GLUniform LightColorUniform { get; set; }
        private GLUniform LightObjectColorUniform { get; set; }
        private Vector3 LightColor { get; set; }
        private double ElapsedTime { get; set; }

        private static readonly double Speed = 10.0;

        public void Setup()
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

            Camera = new PerspectiveCamera(new Vector3(6f, 6f, 6f), new Vector3(0f, 0f, 0f), 1f, 1f, MathHelper.DegreesToRadians(60f), 0.1f, 100f);

            var lightVertexShader = new GLShader((new EmbeddedResource("src/Minity.App/Exercise008.BasicLight/light_vert.glsl")).Stream, ShaderType.VertexShader);
            var lightFragmentShader = new GLShader((new EmbeddedResource("src/Minity.App/Exercise008.BasicLight/light_frag.glsl")).Stream, ShaderType.FragmentShader);

            var lightObjectVertexShader = new GLShader((new EmbeddedResource("src/Minity.App/Exercise008.BasicLight/light_object_vert.glsl")).Stream, ShaderType.VertexShader);
            var lightObjectFragmentShader = new GLShader((new EmbeddedResource("src/Minity.App/Exercise008.BasicLight/light_object_frag.glsl")).Stream, ShaderType.FragmentShader);

            LightProgram = new GLProgram(lightVertexShader, lightFragmentShader);
            LightObjectProgram = new GLProgram(lightObjectVertexShader, lightObjectFragmentShader);

            Checker = new CheckerFloor(10, Camera, LightProgram);
            Cube = new Cube(Camera, LightProgram);
            Cube.Scale = new Vector3(2f, 2f, 2f);

            LightCube = new Cube(Camera, LightObjectProgram);
            LightCube.Position = new Vector3(3f, 3f, 3f);
            LightCube.Scale = new Vector3(0.2f, 0.2f, 0.2f);
            LightObjectProgram.Use();
            LightObjectColorUniform = LightObjectProgram.GetUniform("LightColor");

            LightProgram.Use();
            LightPositionUniform = LightProgram.GetUniform("LightPosition");
            LightColorUniform = LightProgram.GetUniform("LightColor");

            LightColor = new Vector3(1f, 1f, 1f);

            ElapsedTime = 0.0;
        }

        public void Dispose()
        {
            Checker.Dispose();
        }

        public void Render(double deltaTime)
        {
            var radians = (float)MathHelper.DegreesToRadians(ElapsedTime / Speed * 360.0);
            LightCube.Position = new Vector3(2f * MathF.Sin(radians), 1.5f + MathF.Sin(radians * 2f), 2f * MathF.Cos(radians));
            Camera.Position = new Vector3(6f * MathF.Cos(radians / 2f), 5f, 6f * MathF.Sin(radians / 2f));

            var lightPosition = LightCube.Position;
            var lightColor = LightColor;

            LightObjectProgram.Use();
            LightObjectColorUniform.Uniform3(ref lightColor);
            LightCube.Render(deltaTime);

            LightProgram.Use();
            LightPositionUniform.Uniform3(ref lightPosition);
            LightColorUniform.Uniform3(ref lightColor);

            Checker.Render(deltaTime);

            Cube.Position = new Vector3(4f, 1f, -4f);
            Cube.Render(deltaTime);

            Cube.Position = new Vector3(4f, 1f, 4f);
            Cube.Render(deltaTime);

            Cube.Position = new Vector3(-4f, 1f, -4f);
            Cube.Render(deltaTime);

            Cube.Position = new Vector3(-4f, 1f, 4f);
            Cube.Render(deltaTime);

            Cube.Position = new Vector3(0f, 1f, 0f);
            Cube.Render(deltaTime);

            ElapsedTime += deltaTime;
        }

        public void Resize(int width, int height)
        {
            Camera.Width = width;
            Camera.Height = height;
        }
    }
}
