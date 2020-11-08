using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Minity.MinityEngine.Rendering.LowLevel
{
    public class GLUniform
    {
        public GLProgram Program { get; }
        public int Location { get; }

        public GLUniform(GLProgram program, string name)
        {
            Location = GL.GetUniformLocation(program.Handle, name);
            if (Location == -1) throw new System.ArgumentException($"Uniform Error: name = {name} is not found in program = {program.Handle}");

            Program = program;
        }

        public void Uniform1(double value)
        {
            Program.Use();
            GL.Uniform1(Location, value);
        }

        public void Uniform1(float value)
        {
            Program.Use();
            GL.Uniform1(Location, value);
        }

        public void Uniform1(int value)
        {
            Program.Use();
            GL.Uniform1(Location, value);
        }

        public void Uniform3(ref Vector3 value)
        {
            Program.Use();
            GL.Uniform3(Location, ref value);
        }

        public void Matrix4(bool transpose, ref Matrix4 value)
        {
            Program.Use();
            GL.UniformMatrix4(Location, transpose, ref value);
        }
    }
}