namespace Minity.ObjLoader
{
    public struct Face
    {
        public readonly int[] VertexIndices { get; }
        public readonly int[] TextureIndices { get; }
        public readonly int[] NormalIndices { get; }

        public Face(int[] vertexIndices, int[] textureIndices, int[] normalIndicies)
        {
            VertexIndices = vertexIndices;
            TextureIndices = textureIndices;
            NormalIndices = normalIndicies;
        }
    }
}