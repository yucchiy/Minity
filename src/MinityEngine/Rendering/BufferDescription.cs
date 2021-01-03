namespace MinityEngine.Rendering
{
    public struct BufferDescription
    {
        public uint SizeInBytes { get; }
        public BufferUsage Usage { get; }
        public BufferType Type { get; }

        public BufferDescription(uint sizeInBytes, BufferUsage usage, BufferType type)
        {
            SizeInBytes = sizeInBytes;
            Usage = usage;
            Type = type;
        }
    }
}