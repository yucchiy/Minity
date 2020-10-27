#version 330 core

in vec4 OutputColor;
out vec4 FragColor;

void main()
{
    FragColor = vec4(OutputColor);
}