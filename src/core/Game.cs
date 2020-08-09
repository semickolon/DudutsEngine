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
            var mesh = new Mesh(new float[] {
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
            });

            root = new GameObject();

            var meshes = new GameObject(); 
            
            var mesh1 = new GameObject();
            mesh1.AddComponent(new MeshRenderer(mesh, material));
            mesh1.AddComponent(new Rotator());
            
            var mesh11 = new GameObject();
            mesh11.AddComponent(new MeshRenderer(mesh, material));
            mesh11.AddComponent(new Rotator());

            var mesh2 = new GameObject();
            mesh2.AddComponent(new MeshRenderer(mesh, material));

            var camera = new Camera();
            camera.transform.position.Z += 2f;

            camera.AddComponent(new CameraController());
            
            mesh1.AddChild(mesh11);
            meshes.AddChild(mesh1);
            meshes.AddChild(mesh2);
            root.AddChild(meshes);
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