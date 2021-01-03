namespace MinityEngine.Rendering
{
    public interface IPipelineState
    {
        IShader VertexShader { get; }
        IShader FragmentShader { get; }

        void Bind();
    }
}