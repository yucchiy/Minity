using System;

namespace MinityEngine.Rendering
{
    public class ImGUIRenderer : IDisposable
    {
        public ImGUIRenderer(IGraphicsDevice device)
        {
            var context = ImGuiNET.ImGui.CreateContext();
        }

        public void Dispose()
        {
        }
    }
}