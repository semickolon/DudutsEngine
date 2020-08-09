#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform float TIME;

out vec2 UV;
out vec3 NORMAL;
out vec3 FRAGPOS;
out mat4 WORLD_MATRIX;
out mat4 VIEW_MATRIX;
out mat4 PROJECTION_MATRIX;

void main() {
    UV = vec2(aTexCoord.x, aTexCoord.y);
    NORMAL = mat3(transpose(inverse(model))) * aNormal;
    FRAGPOS = vec3(model * vec4(aPosition, 1));
    WORLD_MATRIX = model;
    VIEW_MATRIX = view;
    PROJECTION_MATRIX = projection;

    gl_Position = projection * view * model * vec4(aPosition, 1);
}
