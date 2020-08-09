#version 330 core

in vec2 UV;

out vec4 FragColor;

uniform float TIME;

uniform sampler2D texture0;

void main() {
    float z = sin(TIME * 10) / 2 + 0.5;
    FragColor = mix(vec4(UV.x, UV.y, z, 1), texture(texture0, UV), 1);
}
