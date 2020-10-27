#version 330 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec3 inColor;

uniform float elapsedTime;
out vec4 OutputColor;

void main()
{
    gl_Position = vec4(inPosition, 1.0);
    OutputColor = sin(elapsedTime) * vec4(inColor, 1.0);
}