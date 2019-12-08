#version 330 core
out vec4 FragColor;

in vec3 FragPos;

float color;

void main()
{
    color = mod(FragPos.x * FragPos.y * FragPos.z, 0.01);
    if (color > 0.005)
    {
        color = 1.0;
    }
    else
    {
        color = 0.0;
    }
    FragColor = vec4(color, 0.0, 0.0, 1.0);
//    FragColor = vec4(1.0, 0.0, 0.0, 1.0);
}
