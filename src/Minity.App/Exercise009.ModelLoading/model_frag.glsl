#version 330 core

uniform sampler2D texture1;
uniform mat4 ViewMatrix;
uniform vec3 LightPosition;
uniform vec3 LightColor;
uniform vec3 CameraPosition;

in vec3 OutputColor;
in vec3 OutputVertexPositionInCameraScape;
in vec3 OutputNormalDirectionInCameraSpace;
in vec3 OutputCameraDirectionInCameraSpace;
in vec3 OutputLightDirectionInCameraSpace;

out vec4 FragColor;

void main()
{
    vec3 l = normalize(OutputLightDirectionInCameraSpace);
    vec3 n = normalize(OutputNormalDirectionInCameraSpace);
    vec3 diffuseColor = max(dot(n, l), 0.0) * LightColor;

    vec3 e = normalize(OutputCameraDirectionInCameraSpace);
    vec3 r = reflect(-l, n);
    vec3 specularColor = pow(max(dot(e, r), 0.0), 32) * LightColor;

    FragColor = vec4((specularColor + diffuseColor + vec3(0.2, 0.2, 0.2)) * vec3(1.0), 1.0);
}