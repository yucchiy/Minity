namespace MinityEngine.Rendering
{
    public struct DepthStencilStateDescription
    {
        public bool DepthTestEnabled { get; }

        public DepthStencilStateDescription(bool depthTestEnabled)
        {
            DepthTestEnabled = depthTestEnabled;
        }
    }
}