#version 330 core

uniform sampler2D texture1;

in vec2 OutputTexCoord;
out vec4 FragColor;

void main()
{
    FragColor = vec4(texture(texture1, OutputTexCoord).xyz, 1.0);
}