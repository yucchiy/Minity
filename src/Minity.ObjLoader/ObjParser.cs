using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minity.ObjLoader
{
    public struct ObjParser
    {
        private StreamReader Reader { get; }

        public ObjParser(StreamReader reader)
        {
            Reader = reader;
        }

        public ObjParser(Stream stream) : this(new StreamReader(stream)) {}

        public async Task<Obj> ParseAsync()
        {
            var vertices = new List<Vec4>();
            var normals = new List<Vec3>();
            var textureCoordinates = new List<Vec3>();
            var faces = new List<Face>();
            var materialLibraries = new List<string>();

            while (true)
            {
                var line = await Reader.ReadLineAsync();
                if (line == null) break;

                var elements = line.Split(ElementSeparator);

                if (elements.Length <= 0)
                {
                    // is empty line??
                    continue;
                }

                // The first element represents the type of this line.
                switch (elements[0])
                {
                    case CommentLinePrefix:
                        // this is comment line.
                        break;
                    case GeometricVertexLinePrefix:
                        ParseGeometricVertex(elements, vertices);
                        break;
                    case NormalLinePrefix:
                        ParseVertexNormal(elements, normals);
                        break;
                    case TextureCoordinateLinePrefix:
                        ParseTextureCoordinate(elements, textureCoordinates);
                        break;
                    case PolygonalFaceLinePrefix:
                        ParsePolygonalFace(elements, faces);
                        break;
                    case MaterialLibraryLinePrefix:
                        break;
                    default:
                        break;
                }
            }

            return new Obj(
                vertices.ToArray(),
                normals.ToArray(),
                textureCoordinates.ToArray(),
                faces.ToArray()
            );
        }

        private void ParseGeometricVertex(string[] elements, List<Vec4> vertices)
        {
            switch (elements.Length)
            {
                case 4:
                    vertices.Add(new Vec4(Single.Parse(elements[1]), Single.Parse(elements[2]), Single.Parse(elements[3]), 1.0f));
                    return;
                case 5:
                    vertices.Add(new Vec4(Single.Parse(elements[1]), Single.Parse(elements[2]), Single.Parse(elements[3]), Single.Parse(elements[4])));
                    return;
            }

            throw new InvalidDataException($"failed to parse as geometirc vertex line.");
        }

        private void ParseVertexNormal(string[] elements, List<Vec3> vertexNormals)
        {
            if (elements.Length != 4) throw new InvalidDataException("failed to parse as vertex normal line.");
            vertexNormals.Add(new Vec3(Single.Parse(elements[1]), Single.Parse(elements[2]), Single.Parse(elements[3])));
        }

        private void ParseTextureCoordinate(string[] elements, List<Vec3> textureCoordinates)
        {
            switch (elements.Length)
            {
                case 3:
                    textureCoordinates.Add(new Vec3(Single.Parse(elements[1]), Single.Parse(elements[2]), 0f));
                    return;
                case 4:
                    textureCoordinates.Add(new Vec3(Single.Parse(elements[1]), Single.Parse(elements[2]), Single.Parse(elements[3])));
                    return;
            }

            throw new InvalidDataException($"failed to parse as texture coordinate line.");
        }

        private void ParsePolygonalFace(string[] elements, List<Face> faces)
        {
            if (elements.Length < 2) throw new InvalidDataException("failed to parse as polygonal face line.");

            var vertexIndices = new List<int>();
            var textureIndices = new List<int>();
            var normalIndices = new List<int>();
            for (var i = 1; i < elements.Length; ++i)
            {
                ParseFaceElement(elements[i].Split(FaceSeparator), vertexIndices, textureIndices, normalIndices);
            }

            faces.Add(new Face(vertexIndices.ToArray(), textureIndices.ToArray(), normalIndices.ToArray()));
        }

        private void ParseFaceElement(string[] elements, List<int> vertexIndices, List<int> textureIndices, List<int> normalIndices)
        {
            switch (elements.Length)
            {
                case 1:
                    vertexIndices.Add(Int32.Parse(elements[0]));
                    break;
                case 2:
                    vertexIndices.Add(Int32.Parse(elements[0]));
                    textureIndices.Add(Int32.Parse(elements[1]));
                    break;
                case 3:
                    if (string.IsNullOrEmpty(elements[1]))
                    {
                        vertexIndices.Add(Int32.Parse(elements[0]));
                        normalIndices.Add(Int32.Parse(elements[2]));
                    }
                    else
                    {
                        vertexIndices.Add(Int32.Parse(elements[0]));
                        textureIndices.Add(Int32.Parse(elements[1]));
                        normalIndices.Add(Int32.Parse(elements[2]));
                    }
                    break;
                default:
                    throw new InvalidDataException("failed to parse polygonal element.");
            }
        }

        private void ParseMaterialLibrary(string[] elements, List<string> materialLibraries)
        {
        }

        private static readonly string ElementSeparator = " ";
        private static readonly string FaceSeparator = "/";

        private const string CommentLinePrefix = "#";
        private const string GeometricVertexLinePrefix = "v";
        private const string NormalLinePrefix = "vn";
        private const string TextureCoordinateLinePrefix = "vt";
        private const string PolygonalFaceLinePrefix = "f";
        private const string MaterialLibraryLinePrefix = "matlib";
    }
}