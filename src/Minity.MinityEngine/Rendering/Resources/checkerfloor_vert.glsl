#version 330 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec2 inTexCoord;

uniform mat4 ModelMatrix;
uniform mat4 ViewMatrix;
uniform mat4 ProjectionMatrix;

out vec2 OutputTexCoord;

void main()
{
    mat4 mvpMatrix = ProjectionMatrix * ViewMatrix * ModelMatrix;
    gl_Position = mvpMatrix * vec4(inPosition, 1.0);
    OutputTexCoord = inTexCoord;
}