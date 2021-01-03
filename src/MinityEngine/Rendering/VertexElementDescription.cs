namespace MinityEngine.Rendering
{
    public struct VertexElementDescription
    {
        public string Name { get; }

        public VertexElementSemantic Semantic { get; }

        public VertexElementFormat Format { get; }

        public VertexElementDescription(string name, VertexElementSemantic semantic, VertexElementFormat format)
        {
            Name = name;
            Semantic = semantic;
            Format = format;
        }
    }
}