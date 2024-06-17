#version 330

in vec4 vertexColor;
in vec3 normal;
in vec3 fragPos;
in vec2 texCoord;

out vec4 fragColor;

uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;
uniform sampler2D texture0;
//uniform double useTexture;


void main()
{
    vec3 lightDir = normalize(fragPos - lightPos);

    float diffuseStrenght = max(dot(normalize(normal), lightDir), 0);
    vec3 diffuse = diffuseStrenght * lightColor;

    float ambientStrength = 0.2;
    vec3 ambient = ambientStrength * lightColor;

    float specularStrength = 0.5;
    vec3 viewDir = normalize(fragPos - viewPos);
    vec3 reflectDir = normalize(reflect(-lightDir, normal));
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 64);
    vec3 specular = specularStrength * spec * lightColor;

    vec3 result = (ambient + diffuse + specular) * vec3(vertexColor);
    
    
    fragColor = vec4(result, 1.0);
    
}