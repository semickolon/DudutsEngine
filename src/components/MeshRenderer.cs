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

            GL.BindVertexArray(mesh.vao);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.DrawElements(PrimitiveType.Triangles, mesh.elementCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        protected override void _Dispose() {
            // mesh.Dispose();
        }
    }
}