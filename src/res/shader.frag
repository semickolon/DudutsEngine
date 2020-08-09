#version 330 core

in vec2 UV;
in vec3 NORMAL;
in vec3 FRAGPOS;
in mat4 WORLD_MATRIX;
in mat4 VIEW_MATRIX;
in mat4 PROJECTION_MATRIX;

out vec4 FragColor;

uniform float TIME;

uniform vec4 lightAmbient_Color = vec4(1);
uniform vec3 light0_Pos;
uniform vec4 light0_Color;
uniform vec3 light1_Pos;
uniform vec4 light1_Color;
uniform vec3 light2_Pos;
uniform vec4 light2_Color;
uniform vec3 light3_Pos;
uniform vec4 light3_Color;

uniform float ROUGHNESS = 0.15;
uniform float SPECULAR = 0.5;

uniform sampler2D texture0;

vec4 lc(vec4 color) {
    return vec4(color.xyz * color.w, 1);
}

vec4 lc(vec4 color, vec3 pos) {
    vec3 lightDir = pos - FRAGPOS;
    vec3 unitLightDir = normalize(lightDir);
    vec4 lcColor = lc(color);

    float Nd = max(dot(NORMAL, unitLightDir), 0);
    float attenuation = clamp(1 / pow(length(lightDir), 2), 0, 1);
    vec4 diffuse = lcColor * Nd * attenuation;
    
    vec3 viewDir = vec3(VIEW_MATRIX[0][2], VIEW_MATRIX[1][2], VIEW_MATRIX[2][2]);
    vec3 reflectDir = reflect(-unitLightDir, NORMAL);
    float shininess = mix(512, 2, ROUGHNESS);
    float Ns = pow(max(dot(reflectDir, viewDir), 0), shininess);
    vec4 specular = lcColor * Ns * SPECULAR;

    return diffuse + specular;
}

void main() {
    vec4 light = lc(lightAmbient_Color) + 
        lc(light0_Color, light0_Pos) + lc(light1_Color, light1_Pos) +
        lc(light2_Color, light2_Pos) + lc(light3_Color, light3_Pos);
    vec4 albedo = texture(texture0, UV);
    FragColor = light * albedo;
}
