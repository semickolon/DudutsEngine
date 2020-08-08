using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class MeshRenderer : Component {
        public readonly int vao;
        public readonly int ebo;
        public readonly int vbo;
        public readonly int uvbo;
        public readonly int elementCount;
        public readonly Material material;

        public MeshRenderer(float[] vertices, uint[] indices, float[] uvs, Material material) {
            this.elementCount = indices.Length;
            this.material = material;
            
            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);

            GL.GenBuffers(1, out ebo);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out vbo);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.GenBuffers(1, out uvbo);
            GL.BindBuffer(BufferTarget.ArrayBuffer, uvbo);
            GL.BufferData(BufferTarget.ArrayBuffer, uvs.Length * sizeof(float), uvs, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0);
        }
        
        public override void Render() {
            material.Use();
            Matrix4 mat4 = transform.localMatrix;
            material.shader.SetMatrix4("transform", ref mat4);

            GL.BindVertexArray(vao);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.DrawElements(PrimitiveType.Triangles, elementCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        protected override void _Dispose() {
            GL.DeleteBuffer(ebo);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(uvbo);
            GL.DeleteVertexArray(vao);
        }
    }
}