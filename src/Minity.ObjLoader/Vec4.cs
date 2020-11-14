namespace Minity.ObjLoader
{
    public struct Vec4
    {
        public readonly float X { get; }
        public readonly float Y { get; }
        public readonly float Z { get; }
        public readonly float W { get; }

        public Vec4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}