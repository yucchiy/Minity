using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace MinityEngine.Rendering.OpenGL
{
    public class OpenGLPipelineState : IPipelineState
    {
        public IShader VertexShader { get; }
        public IShader FragmentShader { get; }

        public OpenGLShader OpenGLVertexShader => VertexShader as OpenGLShader;
        public OpenGLShader OpenGLFragmentShader => FragmentShader as OpenGLShader;
        public PrimitiveType PrimitiveType => OpenGLUtility.GetPrimitiveType(PrimitiveTopology);

        private PrimitiveTopology PrimitiveTopology { get; }
        private RasterizerStateDescription RasterizerState { get; }
        private DepthStencilStateDescription DepthStencilState { get; }

        private int ProgramHandle { get; }
        private int VertexArrayObjectHandle { get; }

        public OpenGLPipelineState(in PipelineStateDescription description)
        {
            VertexShader = description.VertexShader;
            FragmentShader = description.FragmentShader;

            PrimitiveTopology = description.Topology;
            DepthStencilState = description.DepthStencilStateDescription;
            RasterizerState = description.RasterizerStateDescription;

            ProgramHandle = GL.CreateProgram();
            OpenGLUtility.CheckError();

            GL.AttachShader(ProgramHandle, OpenGLVertexShader.Handle);
            OpenGLUtility.CheckError();
            GL.AttachShader(ProgramHandle, OpenGLFragmentShader.Handle);
            OpenGLUtility.CheckError();

            GL.LinkProgram(ProgramHandle);
            OpenGLUtility.CheckError();

            VertexArrayObjectHandle = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObjectHandle);
            OpenGLUtility.CheckError();

            var elements = description.VertexLayoutDescription.Elements;
            var offsetInBytes = 0U;
            for (var i = 0; i < elements.Length; ++i)
            {
                GL.EnableVertexAttribArray(i);
                OpenGLUtility.CheckError();

                var element = elements[i];

                var location = GL.GetAttribLocation(ProgramHandle, element.Name);
                OpenGLUtility.CheckError();

                var elementSizeInBytes = OpenGLUtility.GetSizeInBytes(element.Format);
                GL.VertexAttribPointer(location, OpenGLUtility.GetElementCount(element.Format), OpenGLUtility.GetVertexAttribPointerType(element.Format), false, (int)description.VertexLayoutDescription.Stride, (int)offsetInBytes);
                OpenGLUtility.CheckError();

                offsetInBytes += elementSizeInBytes;
            }

            GL.BindVertexArray(0);
        }

        public void Bind()
        {
            if (DepthStencilState.DepthTestEnabled)
            {
                GL.Enable(EnableCap.DepthTest);
            }
            else
            {
                GL.Disable(EnableCap.DepthTest);
            }

            if (RasterizerState.CullMode == FaceCullMode.None)
            {
                GL.Disable(EnableCap.CullFace);
                OpenGLUtility.CheckError();
            }
            else
            {
                GL.Enable(EnableCap.CullFace);
                OpenGLUtility.CheckError();

                GL.CullFace(OpenGLUtility.GetCullFaceMode(RasterizerState.CullMode));
                OpenGLUtility.CheckError();
            }

            GL.FrontFace(OpenGLUtility.GetFrontFaceDirection(RasterizerState.FrontFace));

            GL.BindVertexArray(VertexArrayObjectHandle);
            OpenGLUtility.CheckError();

            GL.UseProgram(ProgramHandle);
            OpenGLUtility.CheckError();
        }
    }
}