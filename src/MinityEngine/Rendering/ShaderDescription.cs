namespace MinityEngine.Rendering
{
    public struct ShaderDescription
    {
        public ShaderStage Stage { get; }
        public string SourceCode { get; }

        public ShaderDescription(ShaderStage stage, string sourceCode)
        {
            Stage = stage;
            SourceCode = sourceCode;
        }
    }
}