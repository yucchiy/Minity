#version 330 core

uniform vec3 LightColor;

out vec4 FragColor;

void main()
{
    FragColor = vec4(LightColor, 1);
}