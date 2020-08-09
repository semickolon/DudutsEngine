using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class MeshRenderer : Component {
        public readonly Mesh mesh;
        public readonly Material material;

        public MeshRenderer(Mesh mesh, Material material) {
            this.mesh = mesh;
            this.material = material;
        }
        
        public override void Render() {
            material.Use();

            Matrix4 model = transform.globalMatrix;
            Matrix4 view = Camera.current.viewMatrix;
            Matrix4 projection = Camera.current.projectionMatrix;
            material.shader.SetMatrix4("model", ref model);
            material.shader.SetMatrix4("view", ref view);
            material.shader.SetMatrix4("projection", ref projection);

            material.shader.SetVector4("lightAmbient_Color", new Vector4(1, 1, 1, 0.1f));

            for (int i = 0; i < 4; i++) {
                if (i < PointLight.allLights.Count) {
                    var light = PointLight.allLights[i];
                    material.shader.SetVector3($"light{i}_Pos", light.transform.globalPosition);
                    material.shader.SetVector4($"light{i}_Color", light.uniformColor);
                } else {
                    material.shader.SetVector3($"light{i}_Pos", new Vector3(0));
                    material.shader.SetVector4($"light{i}_Color", new Vector4(0));
                }
            }

            GL.BindVertexArray(mesh.vao);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.DrawElements(PrimitiveType.Triangles, mesh.elementCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.BindVertexArray(0);
        }

        protected override void _Dispose() {
            // mesh.Dispose();
        }
    }
}