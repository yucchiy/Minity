using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public class GLUniform
    {
        public int Location { get; }

        public GLUniform(GLProgram program, string name)
        {
            Location = GL.GetUniformLocation(program.Handle, name);
            if (Location == -1) throw new System.ArgumentException($"Uniform Error: name = {name} is not found in program = {program.Handle}");
        }

        public void Uniform1(double value)
        {
            GL.Uniform1(Location, value);
        }

        public void Uniform1(float value)
        {
            GL.Uniform1(Location, value);
        }

        public void Uniform1(int value)
        {
            GL.Uniform1(Location, value);
        }

        public void Matrix4(bool transpose, ref Matrix4 value)
        {
            GL.UniformMatrix4(Location, transpose, ref value);
        }
    }
}