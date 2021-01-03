namespace MinityEngine.Rendering
{
    public struct PipelineStateDescription
    {
        public IShader VertexShader { get; }
        public IShader FragmentShader { get; }
        public VertexLayoutDescription VertexLayoutDescription { get; }
        public DepthStencilStateDescription DepthStencilStateDescription { get; }
        public RasterizerStateDescription RasterizerStateDescription { get; }
        public PrimitiveTopology Topology { get; }

        public PipelineStateDescription(IShader vertexShader, IShader fragmentShader, in VertexLayoutDescription vertexLayoutDescription, in DepthStencilStateDescription depthStencilStateDescription, in RasterizerStateDescription rasterizerStateDescription, PrimitiveTopology topology)
        {
            VertexShader = vertexShader;
            FragmentShader = fragmentShader;
            VertexLayoutDescription = vertexLayoutDescription;
            DepthStencilStateDescription = depthStencilStateDescription;
            RasterizerStateDescription = rasterizerStateDescription;
            Topology = topology;
        }
    }
}