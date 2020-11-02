using System;
using Minity.MinityEngine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using Minity.MinityEngine.Rendering;

namespace Minity.App.Exercise.Camera
{
    public class MainScene : IScene, ISetupable, IRenderable, IDisposable, IResizable
    {
        private ICamera Camera { get; set; }
        private CheckerFloor Checker { get; set; }
        private double ElapsedTime { get; set; }

        private static readonly double Speed = 10.0;

        public void Setup()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Camera = new PerspectiveCamera(new Vector3(0f, 0f, 2f), new Vector3(0f, 0f, 0f), 1f, 1f, MathHelper.DegreesToRadians(60f), 0.1f, 100f);
            Checker = new CheckerFloor(10, Camera);

            ElapsedTime = 0.0;
        }

        public void Dispose()
        {
            Checker.Dispose();
        }

        public void Render(double deltaTime)
        {
            var radians = (float)MathHelper.DegreesToRadians(ElapsedTime / Speed * 360.0);
            Camera.Position = new Vector3(3f * MathF.Sin(radians), 1.5f, 3f * MathF.Cos(radians));

            Checker.Render(deltaTime);

            ElapsedTime += deltaTime;
        }

        public void Resize(int width, int height)
        {
            Camera.Width = width;
            Camera.Height = height;
        }
    }
}
