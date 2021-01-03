using System;

namespace MinityEngine.Rendering
{
    public struct RasterizerStateDescription
    {
        public FaceCullMode CullMode;
        public PolygonFillMode FillMode;
        public FrontFace FrontFace;
        public bool DepthClipEnabled;
        public bool ScissorTestEnabled;

        public RasterizerStateDescription(FaceCullMode cullMode, PolygonFillMode fillMode, FrontFace frontFace, bool depthClipEnabled, bool scissorTestEnabled)
        {
            CullMode = cullMode;
            FillMode = fillMode;
            FrontFace = frontFace;
            DepthClipEnabled = depthClipEnabled;
            ScissorTestEnabled = scissorTestEnabled;
        }

        public static readonly RasterizerStateDescription Default = new RasterizerStateDescription
        {
            CullMode = FaceCullMode.Back,
            FillMode = PolygonFillMode.Solid,
            FrontFace = FrontFace.CounterClockwise,
            DepthClipEnabled = true,
            ScissorTestEnabled = false,
        };

       public static readonly RasterizerStateDescription CullNone = new RasterizerStateDescription
        {
            CullMode = FaceCullMode.None,
            FillMode = PolygonFillMode.Solid,
            FrontFace = FrontFace.CounterClockwise,
            DepthClipEnabled = true,
            ScissorTestEnabled = false,
        };
    }
}
