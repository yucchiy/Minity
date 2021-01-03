namespace MinityEngine.Rendering
{
    public struct VertexLayoutDescription
    {
        public uint Stride { get; }

        public VertexElementDescription[] Elements { get; }

        public VertexLayoutDescription(params VertexElementDescription[] elements)
        {
            Elements = elements;
            Stride = 0;
            for (var i = 0; i < Elements.Length; ++i)
            {
                Stride += RenderingHelper.GetSizeInBytes(Elements[i].Format);
            }
        }
    }
}