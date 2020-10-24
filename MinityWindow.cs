using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Minity
{
    public class MinityWindow : GameWindow
    {
        public MinityWindow() : base(
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
        }
    }
}