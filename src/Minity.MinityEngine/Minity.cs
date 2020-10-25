using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine
{
    public class Minity : System.IDisposable
    {
        public IScene ActiveScene { get; }

        public IGraphicsContext GraphicsContext { get; }

        public Minity(IScene scene, IGraphicsContext graphicsContext)
        {
            ActiveScene = scene;
            GraphicsContext = graphicsContext;
        }

        public void Setup()
        {
            ActiveScene.Setup();
        }

        public void Dispose()
        {
            if (ActiveScene is System.IDisposable disposable) disposable.Dispose();
        }

        public void Update(double deltaTime)
        {
            ActiveScene.Update(deltaTime);
        }

        public void Render(double deltaTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GraphicsContext.SwapBuffers();

            ActiveScene.Render(deltaTime);
        }

        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }
    }
}