using OpenTK;
using OpenTK.Input;

namespace DudutsEngine {
    public class CameraController : Component {
        public float moveSpeed = 2f;
        public float sensitivity = 0.1f;
        public Vector2 lastMousePos = new Vector2(0);

        public override void Process(float delta) {
            ProcessMove(delta);
            ProcessLookAround(delta);
        }

        private void ProcessMove(float delta) {
            KeyboardState input = Keyboard.GetState();
            Vector3 moveDir = new Vector3(0);

            if (input.IsKeyDown(Key.Space))
                return;

            if (input.IsKeyDown(Key.W)) {
                moveDir -= transform.forward;
            }
            if (input.IsKeyDown(Key.S)) {
                moveDir += transform.forward;
            }
            if (input.IsKeyDown(Key.D)) {
                moveDir += transform.right;
            }
            if (input.IsKeyDown(Key.A)) {
                moveDir -= transform.right;
            }
            if (input.IsKeyDown(Key.Tab)) {
                moveDir += transform.up;
            }
            if (input.IsKeyDown(Key.ShiftLeft)) {
                moveDir -= transform.up;
            }

            this.transform.position += moveDir * moveSpeed * delta;
        }

        private void ProcessLookAround(float delta) {
            MouseState input = Mouse.GetState();
            float dx = input.X - lastMousePos.X;
            float dy = input.Y - lastMousePos.Y;
            lastMousePos = new Vector2(input.X, input.Y);

            transform.rotation.X -= dy * sensitivity;
            transform.rotation.X = MathHelper.Clamp(transform.rotation.X, -89.99f, 89.99f);
            transform.rotation.Y -= dx * sensitivity;
        }
    }
}