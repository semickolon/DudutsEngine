using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Game {
        private static Stopwatch stopwatch = new Stopwatch();
        public static float GlobalTime {
            get => stopwatch.ElapsedMilliseconds / 1000.0f;
        }

        GameWindow window;
        GameObject gameObject;

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

            stopwatch.Start();
            window.Run(60.0);
        }

        void Load(object o, EventArgs e) {
            GL.ClearColor(0, 0, 0, 0);

            var shader = new Shader("src/res/shader.vert", "src/res/shader.frag");
            var textures = new Texture[] { new Texture("src/res/texture.png") };
            var material = new Material(shader, textures);
            var meshRenderer = new MeshRenderer(new float[] {
                0.5f,  0.5f, 0.0f,  // top right
                0.5f, -0.5f, 0.0f,  // bottom right
                -0.5f, -0.5f, 0.0f,  // bottom left
                -0.5f,  0.5f, 0.0f  // top left
            }, new uint[] {
                0, 1, 3,
                1, 2, 3
            }, new float[] {
                1, 0,
                1, 1,
                0, 1,
                0, 0
            }, material);
            
            gameObject = new GameObject();
            gameObject.AddComponent(meshRenderer);
            gameObject.AddComponent(new Rotator());
        }

        void Resize(object o, EventArgs e) {
            GL.Viewport(0, 0, window.Width, window.Height);
        }

        void UpdateFrame(object o, EventArgs e) {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape)) {
                window.Exit();
            }

            gameObject.Process(1f / 60f);
        }

        void RenderFrame(object o, EventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            gameObject.Render();
            window.SwapBuffers();
        }

        void Unload(object o, EventArgs e) {
            gameObject.Dispose();
        }
    }
}