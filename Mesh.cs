using System;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Mesh: IDisposable {
        public readonly int vao;
        public readonly int vbo;
        public readonly int ebo;
        public readonly int elementCount;
        public readonly Shader shader;
        private bool disposed = false;

        public Mesh(float[] vertices, uint[] indices, Shader shader) {
            this.elementCount = indices.Length;
            this.shader = shader;
            
            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);

            GL.GenBuffers(1, out ebo);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out vbo);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0);
        }

        public void Dispose() {
            if (!disposed) {
                GL.DeleteBuffer(ebo);
                GL.DeleteBuffer(vbo);
                GL.DeleteVertexArray(vao);
                disposed = true;
            }
        }
        
        public void Render() {
            shader.Use();
            GL.BindVertexArray(vao);
            GL.DrawElements(PrimitiveType.Triangles, elementCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }
    }
}