#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform float TIME;

out vec2 UV;

void main() {
    UV = vec2(aTexCoord.x, aTexCoord.y);

    gl_Position = projection * view * model * vec4(aPosition, 1.0);
}
