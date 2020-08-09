using System;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Mesh : IDisposable {
        public readonly int vao;
        public readonly int elementCount;
        private int ebo;
        private int vbo;
        private int uvbo;
        private int nbo;
        private bool disposed = false;

        public Mesh(float[] vertices, float[] uvs, float[] normals, uint[] indices) {
            this.elementCount = indices.Length;
            
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

            GL.GenBuffers(1, out nbo);
            GL.BindBuffer(BufferTarget.ArrayBuffer, nbo);
            GL.BufferData(BufferTarget.ArrayBuffer, normals.Length * sizeof(float), normals, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0);
        }

        public static Mesh FromOBJFile(string objPath) {
            float[] vertices, uvs, normals;
            uint[] indices;
            ObjLoader.Load(objPath, out vertices, out uvs, out normals, out indices);
            return new Mesh(vertices, uvs, normals, indices);
        }

        public void Dispose() {
            if (!disposed) {
                GL.DeleteBuffer(ebo);
                GL.DeleteBuffer(vbo);
                GL.DeleteBuffer(uvbo);
                GL.DeleteVertexArray(vao);
                disposed = true;
            }
        }
    }
}