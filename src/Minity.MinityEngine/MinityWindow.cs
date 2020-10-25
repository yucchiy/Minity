using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;

namespace Minity.MinityEngine
{
    public class MinityWindow : GameWindow
    {
        public Minity Minity { get; }

        public MinityWindow(IScene scene) : base(
            new GameWindowSettings()
            {
                RenderFrequency = 60.0,
                UpdateFrequency = 60.0,
            },
            new NativeWindowSettings()
            {
                APIVersion = new System.Version(4, 1),
                Flags = ContextFlags.ForwardCompatible,
            }
        )
        {
            Minity = new Minity(scene, Context);
        }

        protected override void OnLoad()
        {
            Minity.Setup();

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            Minity.Dispose();
            
            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            Minity.Update(args.Time);

            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Minity.Render(args.Time);
            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            Minity.Resize(e.Width, e.Height);
            base.OnResize(e);
        }
    }
}