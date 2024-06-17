#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 normalCoords;
layout(location = 2) in vec2 texCoords;

uniform mat4 model;
uniform vec4 aColor;
uniform mat4 view;
uniform mat4 projection;


out vec4 vertexColor;
out vec3 normal;
out vec3 fragPos;
out vec2 texCoord;


float rand(vec2 co)
{
    return fract(sin(dot(co, vec2(12.9898, 78.233))) * 43758.5453);
}

void main(void)
{
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;

    vertexColor = aColor;
    normal =  normalize(normalCoords * mat3(transpose(inverse(model))));
    fragPos = vec3(vec4(aPosition, 1.0) * model);
    texCoord = texCoords;
}

