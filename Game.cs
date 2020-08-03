using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Game {
        GameWindow window;
        Shader shader;
        Mesh mesh;

        public Game() {
            window = new GameWindow(1024, 600);
            window.Title = "Duduts Engine";
        }

        public void Start() {
            window.Load += Load;
            window.Resize += Resize;
            window.UpdateFrame += UpdateFrame;
            window.RenderFrame += RenderFrame;
            window.Unload += Unload;
            window.Run(60.0);
        }

        void Load(object o, EventArgs e) {
            GL.ClearColor(0, 0, 0, 0);

            shader = new Shader("shader.vert", "shader.frag");

            mesh = new Mesh(new float[] {
                0.5f,  0.5f, 0.0f,  // top right
                0.5f, -0.5f, 0.0f,  // bottom right
                -0.5f, -0.5f, 0.0f,  // bottom left
                -0.5f,  0.5f, 0.0f  // top left
            }, new uint[] {
                0, 1, 3,
                1, 2, 3
            }, shader);
        }

        void Resize(object o, EventArgs e) {
            GL.Viewport(0, 0, window.Width, window.Height);
        }

        void UpdateFrame(object o, EventArgs e) {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape)) {
                window.Exit();
            }
        }

        void RenderFrame(object o, EventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            mesh.Render();
            window.SwapBuffers();
        }

        void Unload(object o, EventArgs e) {
            shader.Dispose();
            mesh.Dispose();
        }
    }
}