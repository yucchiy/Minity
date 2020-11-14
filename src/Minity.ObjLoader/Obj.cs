namespace Minity.ObjLoader
{
    public class Obj
    {
        public Vec4[] Vertices { get; }
        public Vec3[] Normals { get; }
        public Vec3[] TextureCoordinates { get; }
        public Face[] Faces { get; }

        public Obj(Vec4[] vertices, Vec3[] normals, Vec3[] textureCoordinates, Face[] faces)
        {
            Vertices = vertices;
            Normals = normals;
            TextureCoordinates = textureCoordinates;
            Faces = faces;
        }
    }
}