namespace Minity.ObjLoader
{
    public struct Vec3
    {
        public readonly float X { get; }
        public readonly float Y { get; }
        public readonly float Z { get; }

        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}