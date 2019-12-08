#version 330 core
out vec4 FragColor;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
    vec4 FragPosLightSpace;
} fs_in;

uniform sampler2D diffuseTexture;
uniform sampler2D shadowMap;

uniform vec3 lightPos;
uniform vec3 lightDir;
uniform vec3 viewPos;

float ShadowCalculation(vec4 fragPosLightSpace)
{
    // perform perspective divide
    vec3 projCoords = fragPosLightSpace.xyz / 2.0 + vec3(0.5);
    // transform to [0,1] range
//    projCoords = projCoords * 0.5 + vec3(0.5);
    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
//    float a = 15.0;
//    float closestDepth = texture(shadowMap, vec2(projCoords.x * a, projCoords.y * a)).r;
    float closestDepth = texture(shadowMap, projCoords.xy).r;
    // get depth of current fragment from light's perspective
    float currentDepth = projCoords.z;
    // check whether current frag pos is in shadow
    float shadow = currentDepth > closestDepth + 0.01 ? 1.0 : 0.0;
//    if (abs(projCoords.x) < 0.4 && abs(projCoords.y) < 0.4) {
//        return 1.0;
//    }
//    return 0.0;
    return shadow;
    


//    // perform perspective divide
//    vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
//    // transform to [0,1] range
//    projCoords = projCoords * 0.5 + vec3(0.5);
//    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
//    float closestDepth = texture(shadowMap, projCoords.xy).r;
//    // get depth of current fragment from light's perspective
//    float currentDepth = projCoords.z;
//    // check whether current frag pos is in shadow
//    float shadow = currentDepth > closestDepth  ? 1.0 : 0.0;
//
//    return shadow;
}

void main()
{
    vec3 color = texture(diffuseTexture, fs_in.TexCoords).rgb;
    vec3 normal = normalize(fs_in.Normal);
    vec3 lightColor = vec3(0.5);
    // ambient
    vec3 ambient = 0.3 * color;
    // diffuse
    vec3 normalizedLightDir = normalize(-lightDir);
    float diff = max(dot(normalizedLightDir, normal), 0.0);
    vec3 diffuse = diff * lightColor;
    // specular
    vec3 viewDir = normalize(viewPos - fs_in.FragPos);
    vec3 reflectDir = reflect(-normalizedLightDir, normal);
    float spec = 0.0;
    vec3 halfwayDir = normalize(normalizedLightDir + viewDir);
    spec = pow(max(dot(normal, halfwayDir), 0.0), 64.0);
    vec3 specular = spec * lightColor;
    // calculate shadow
    float shadow = ShadowCalculation(fs_in.FragPosLightSpace);
//    vec3 lighting1 = (ambient + diffuse + specular) * color;
    vec3 lighting = (ambient + (1.0 - shadow) * (diffuse + specular)) * color;
    
//    FragColor = vec4(vec3(shadow), 1.0);
    FragColor = vec4(lighting, 1.0);
}
