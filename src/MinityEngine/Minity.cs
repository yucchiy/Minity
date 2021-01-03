using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;

namespace MinityEngine
{
    public class Minity : System.IDisposable
    {
        public IScene ActiveScene { get; }
        public IUpdatable SceneUpdatable { get; }
        public IRenderable SceneRenderable { get; }
        public IResizable SceneResizable { get; }

        public IGraphicsContext GraphicsContext { get; }

        public NativeWindow Window { get; }

        public Minity(IScene scene, IGraphicsContext graphicsContext, NativeWindow window)
        {
            ActiveScene = scene;
            GraphicsContext = graphicsContext;
            Window = window;

            SceneUpdatable = ActiveScene as IUpdatable;
            SceneRenderable = ActiveScene as IRenderable;
            SceneResizable = ActiveScene as IResizable;
        }

        public void Setup()
        {
            GL.Enable(EnableCap.DepthTest);
            if (ActiveScene is ISetupable setupable) setupable.Setup();
        }

        public void Dispose()
        {
            if (ActiveScene is System.IDisposable disposable) disposable.Dispose();
        }

        public void Update(double deltaTime)
        {
            SceneUpdatable?.Update(deltaTime);
        }

        public void Render(double deltaTime, IGraphicsContext context)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            SceneRenderable?.Render(deltaTime);
            context.SwapBuffers();
        }

        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            SceneResizable?.Resize(width, height);
        }
    }
}