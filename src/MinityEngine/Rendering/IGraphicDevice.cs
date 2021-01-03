namespace MinityEngine.Rendering
{
    public interface IGraphicsDevice
    {
        IBuffer CreateBuffer(in BufferDescription description);
        IShader CreateShader(in ShaderDescription description);
        IPipelineState CreatePipelineState(in PipelineStateDescription description);

        void BindBuffer(IBuffer buffer);
        void BindPipelineState(IPipelineState pipelineState);

        void UpdateBuffer<T>(IBuffer buffer, T[] data) where T : unmanaged;

        void Draw(uint vertexCount, uint startIndex);
        void DrawIndexed(uint indexCount, uint startIndex);
    }
}