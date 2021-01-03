using System;
using OpenTK.Graphics.OpenGL4;

namespace MinityEngine.Rendering.OpenGL
{
    public class OpenGLShader : IShader, IDisposable
    {
        public int Handle { get; }
        public ShaderStage Stage { get; }
        public OpenGLShader(ShaderDescription descriptor)
        {
            Stage = descriptor.Stage;

            Handle = GL.CreateShader(OpenGLUtility.GetShaderType(Stage));
            OpenGLUtility.CheckError();

            GL.ShaderSource(Handle, descriptor.SourceCode);
            OpenGLUtility.CheckError();

            GL.CompileShader(Handle);
            OpenGLUtility.CheckError();

            var log = GL.GetShaderInfoLog(Handle);
            if (!string.IsNullOrEmpty(log)) throw new OpenGLException($"Shader Error: {log}");
        }

        public void Dispose()
        {
            GL.DeleteShader(Handle);
        }
    }
}