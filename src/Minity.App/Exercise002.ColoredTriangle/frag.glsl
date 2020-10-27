#version 330 core

in vec3 OutputColor;
out vec4 FragColor;

void main()
{
    FragColor = vec4(OutputColor, 1.0);
    // FragColor = vec4(1.0, 1.0, 1.0, 1.0);
}