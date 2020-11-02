using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine
{
    public class Minity : System.IDisposable
    {
        public IScene ActiveScene { get; }

        public IGraphicsContext GraphicsContext { get; }

        public NativeWindow Window { get; }

        public Minity(IScene scene, IGraphicsContext graphicsContext, NativeWindow window)
        {
            ActiveScene = scene;
            GraphicsContext = graphicsContext;
            Window = window;
        }

        public void Setup()
        {
            ActiveScene.Setup();
        }

        public void Dispose()
        {
            if (ActiveScene is System.IDisposable disposable) disposable.Dispose();
            Resize(Window.Size.X, Window.Size.Y);
        }

        public void Update(double deltaTime)
        {
            ActiveScene.Update(deltaTime);
        }

        public void Render(double deltaTime, IGraphicsContext context)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            ActiveScene.Render(deltaTime);

            context.SwapBuffers();
        }

        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            if (ActiveScene is IResizable resizable) resizable.Resize(width, height);
        }
    }
}