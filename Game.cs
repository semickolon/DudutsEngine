using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Game {
        GameWindow window;

        public Game() {
            window = new GameWindow(1024, 600);
            window.Title = "Duduts Engine";
        }

        public void Start() {
            window.Load += Loaded;
            window.Resize += Resized;
            window.RenderFrame += RenderFrame;
            window.Run(1.0 / 60.0);
        }

        void Loaded(object o, EventArgs e) {
            GL.ClearColor(0, 0, 0, 0);
        }

        void Resized(object o, EventArgs e) {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 50, 0, 50, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        void RenderFrame(object o, EventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Begin(BeginMode.Triangles);
            GL.Vertex2(1, 1);
            GL.Vertex2(49, 1);
            GL.Vertex2(25, 49);
            GL.End();

            window.SwapBuffers();
        }
    }
}