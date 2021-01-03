using MinityEngine;
using MinityEngine.Rendering;
using MinityEngine.Rendering.OpenGL;

namespace MinityApp
{
    public class SampleColoredTriangleScene : IScene, ISetupable, IRenderable
    {
        private readonly float[] Vertices = new float[]
        {
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, // Top vertex
        };

        private readonly string VertexShaderSourceCode = 
@"
#version 330 core

layout(location = 0) in vec3 InPosition;
layout(location = 1) in vec3 InColor;

out vec3 OutColor;

void main()
{
    gl_Position = vec4(InPosition, 1.0);
    OutColor = InColor;
}
";
        private readonly string FragmentShaderSourceCode = 
@"
#version 330 core

in vec3 OutColor;
out vec4 FragColor;

void main()
{
    FragColor = vec4(OutColor, 1.0);
}
";

        private IBuffer VertexBuffer;
        private IPipelineState PipelineState;
        private IGraphicsDevice GraphicsDevice;

        public void Setup()
        {
            GraphicsDevice = new OpenGLGraphicDevice();

            VertexBuffer = GraphicsDevice.CreateBuffer(new MinityEngine.Rendering.BufferDescription(0, BufferUsage.Static, BufferType.VertexBuffer));
            GraphicsDevice.UpdateBuffer<float>(VertexBuffer, Vertices);

            var vertexShader = GraphicsDevice.CreateShader(new ShaderDescription(ShaderStage.VertexStage, VertexShaderSourceCode));
            var fragmentShader = GraphicsDevice.CreateShader(new ShaderDescription(ShaderStage.FragmentStage, FragmentShaderSourceCode));

            PipelineState = GraphicsDevice.CreatePipelineState(
                new PipelineStateDescription(
                    vertexShader,
                    fragmentShader,
                    new VertexLayoutDescription(
                        new VertexElementDescription("InPosition", VertexElementSemantic.Position, VertexElementFormat.Float3),
                        new VertexElementDescription("InColor", VertexElementSemantic.Color, VertexElementFormat.Float3)
                    ),
                    new DepthStencilStateDescription(
                        true
                    ),
                    RasterizerStateDescription.Default,
                    PrimitiveTopology.TriangleList
                )
            );
        }

        public void Render(double deltaTime)
        {
            GraphicsDevice.BindBuffer(VertexBuffer);
            GraphicsDevice.BindPipelineState(PipelineState);
            GraphicsDevice.Draw(3, 0);
        }
    }
}