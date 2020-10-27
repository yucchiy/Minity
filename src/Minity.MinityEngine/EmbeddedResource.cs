using System;
using System.IO;
using System.Reflection;

namespace Minity.MinityEngine
{
    public class EmbeddedResource : IDisposable
    {
        public static readonly string ProjectName = "Minity";

        public Stream Stream => Assembly.GetManifestResourceStream(GetName());

        private Assembly Assembly { get; }
        private string FilePath { get; }

        public EmbeddedResource(string filePath)
        {
            Assembly = typeof(EmbeddedResource).GetTypeInfo().Assembly;
            FilePath = filePath;
        }

        public void Dispose()
        {
        }

        private string GetName()
        {
            return ProjectName + "." + FilePath.Replace("/", ".");
        }
    }
}