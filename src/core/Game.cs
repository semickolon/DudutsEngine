using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Game {
        private static Game _instance;
        public static Game instance {
            get {
                if (_instance == null)
                    _instance = new Game();
                return _instance;
            }
        }

        GameWindow window;
        GameObject root;

        private Stopwatch stopwatch = new Stopwatch();
        public float GlobalTime {
            get => stopwatch.ElapsedMilliseconds / 1000.0f;
        }
        public float WindowAspectRatio {
            get => (float)(window.Width) / window.Height;
        }

        private Game() {
            window = new GameWindow(1024, 600);
            window.Title = "Duduts Engine";
            window.CursorVisible = false;
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
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0, 0, 0, 0);

            var shader = new Shader("src/res/shader.vert", "src/res/shader.frag");
            var textures = new Texture[] { new Texture("src/res/texture.png") };
            var material = new Material(shader, textures);
            var mesh = Mesh.FromOBJFile("src/res/suzanne.obj");

            root = new GameObject();

            var cube = new GameObject();
            cube.AddComponent(new MeshRenderer(mesh, material));
            // cube.AddComponent(new Rotator());

            var camera = new Camera();
            camera.transform.position.Z += 2f;
            camera.AddComponent(new CameraController());
            
            root.AddChild(cube);
            root.AddChild(camera);
        }

        void Resize(object o, EventArgs e) {
            GL.Viewport(0, 0, window.Width, window.Height);
        }

        void UpdateFrame(object o, EventArgs e) {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape)) {
                window.Exit();
            }

            root.Process(1f / 60f);
        }

        void RenderFrame(object o, EventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            root.Render();
            window.SwapBuffers();
        }

        void Unload(object o, EventArgs e) {
            root.Dispose();
        }
    }
}