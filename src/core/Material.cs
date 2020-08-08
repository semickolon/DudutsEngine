using System;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Material: IDisposable {
        private static TextureUnit[] textureUnitList = {
            TextureUnit.Texture0, TextureUnit.Texture1, TextureUnit.Texture2, TextureUnit.Texture3,
            TextureUnit.Texture4, TextureUnit.Texture5, TextureUnit.Texture6, TextureUnit.Texture7
        };

        public readonly Shader shader;
        public readonly Texture[] textures;
        private bool disposed = false;

        public Material(Shader shader) : this(shader, new Texture[] {}) {}

        public Material(Shader shader, Texture[] textures) {
            this.shader = shader;
            this.textures = textures;
        }

        public void Use() {
            int textureCount = Math.Min(textureUnitList.Length, textures.Length);
            for (int i = 0; i < textureCount; i++) {
                var unit = textureUnitList[i];
                textures[i].Use(unit);
            }

            shader.Use();
        }

        public void Dispose() {
            if (!disposed) {
                // ???
                disposed = true;
            }
        }
    }
}