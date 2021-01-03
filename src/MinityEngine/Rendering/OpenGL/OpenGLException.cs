using System;

namespace MinityEngine.Rendering.OpenGL
{
    public class OpenGLException : Exception
    {
        public OpenGLException()
        {
        }

        public OpenGLException(string message) : base(message)
        {
        }

        public OpenGLException(string message, Exception inner) : base(message, inner)
        {
        }
   }
}