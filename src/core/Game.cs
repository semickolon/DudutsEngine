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
            GL.Enable(EnableCap.CullFace);
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1f);
            GL.CullFace(CullFaceMode.Back);

            var shader = new Shader("src/res/shader.vert", "src/res/shader.frag");
            var textures = new Texture[] { new Texture("src/res/hint_white.png") };
            var material = new Material(shader, textures);

            root = new GameObject(true);

            var monkey = new GameObject();
            monkey.AddComponent(new MeshRenderer(Mesh.FromOBJFile("src/res/suzanne.obj"), material));

            var cube = Mesh.FromOBJFile("src/res/cube.obj");

            var spinR = new GameObject();
            var spinG = new GameObject();
            var spinB = new GameObject();

            spinG.transform.rotation.Y = 90;
            spinB.transform.rotation.X = 90;

            var lights = new GameObject();
            var lightR = new PointLight(new Vector3(1, 0, 0));
            var lightG = new PointLight(new Vector3(0, 1, 0));
            var lightB = new PointLight(new Vector3(0, 0, 1));

            lightR.transform.scale *= 0.05f;
            lightG.transform.scale *= 0.05f;
            lightB.transform.scale *= 0.05f;

            lightR.AddComponent(new LightController() { moveSpeed = 1f });
            lightG.AddComponent(new LightController() { moveSpeed = 1.5f });
            lightB.AddComponent(new LightController() { moveSpeed = 2.5f });
            lightR.AddComponent(new MeshRenderer(cube, material));
            lightG.AddComponent(new MeshRenderer(cube, material));
            lightB.AddComponent(new MeshRenderer(cube, material));

            var plane = new GameObject();
            plane.AddComponent(new MeshRenderer(Mesh.FromOBJFile("src/res/plane.obj"), material));
            plane.transform.position.Y = -2f;
            // plane.transform.rotation.Z = 30f;

            var camera = new Camera();
            camera.transform.position.Z += 2f;
            camera.AddComponent(new CameraController());
            
            spinR.AddChild(lightR);
            spinG.AddChild(lightG);
            spinB.AddChild(lightB);
            root.AddChild(monkey);
            root.AddChild(plane);
            root.AddChild(spinR);
            root.AddChild(spinG);
            root.AddChild(spinB);
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