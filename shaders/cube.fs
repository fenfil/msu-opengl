#version 330 core
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

struct Light {
    vec3 direction;
    vec3 color;
};

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

float ambientStrength = 0.1;
float diffuseStrength = 0.9;

void main()
{
    // ambient
    vec3 ambient = light.color * texture(material.diffuse, TexCoords).rgb * ambientStrength;
      
    // diffuse
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(-light.direction);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diffuseStrength * light.color * diff * texture(material.diffuse, TexCoords).rgb;
    
//    // specular
//    vec3 viewDir = normalize(viewPos - FragPos);
//    vec3 reflectDir = reflect(-lightDir, norm);
//    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
//    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;

//    float distance = length(light.position - FragPos);
//    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));
        
    vec3 result = ambient + diffuse;
    FragColor = vec4(result, 1.0);
}
