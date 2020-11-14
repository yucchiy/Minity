#version 330 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec3 inNormal;

uniform mat4 ModelMatrix;
uniform mat4 ViewMatrix;
uniform mat4 ProjectionMatrix;

uniform vec3 LightPosition;

out vec3 OutputColor;
out vec3 OutputVertexPositionInCameraScape;
out vec3 OutputNormalDirectionInCameraSpace;
out vec3 OutputCameraDirectionInCameraSpace;
out vec3 OutputLightDirectionInCameraSpace;

void main()
{
    gl_Position = ProjectionMatrix * ViewMatrix * ModelMatrix * vec4(inPosition, 1.0);

    vec3 lightPositionInCameraSpace = vec3(ViewMatrix * vec4(LightPosition, 1.0));

    OutputVertexPositionInCameraScape = vec3(ViewMatrix * ModelMatrix * vec4(inPosition, 1.0));
    OutputNormalDirectionInCameraSpace = vec3(ViewMatrix * ModelMatrix * vec4(inNormal, 0.0));
    OutputCameraDirectionInCameraSpace = vec3(0, 0, 0) - OutputVertexPositionInCameraScape;
    OutputLightDirectionInCameraSpace = lightPositionInCameraSpace + OutputCameraDirectionInCameraSpace;
}