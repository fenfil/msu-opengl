#version 330 core
out vec4 FragColor;

struct Material {
    vec3 ambient;
    vec3 specular;
    float shininess;
};

struct Light {
    vec3 position;

    vec3 specular;

    float constant;
    float linear;
    float quadratic;
};

in vec3 FragPos;

uniform vec3 viewPos;
uniform vec3 Normal;
uniform Material material;
uniform Light light;

void main()
{
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);

//    // ambient
    vec3 ambient = vec3(0.2);
//
//    // diffuse
//    float diff = abs(dot(norm, lightDir));
//    float diff = max(dot(norm, lightDir), 0.0);
//    vec3 diffuse = light.diffuse * diff * material.diffuse;
//
//    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(abs(dot(viewDir, reflectDir)), 32.0);
    vec3 specular = 0.5 * light.specular * spec * material.specular;

    FragColor = vec4(ambient + specular, 0.6);
}
