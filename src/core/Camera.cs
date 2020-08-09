using OpenTK;

namespace DudutsEngine {
    public class Camera : GameObject {
        private static Camera _current;
        public static Camera current {
            get => _current ?? fallback;
        }
        public static Camera fallback = new Camera();

        public float fov;
        public float zNear;
        public float zFar;

        public Matrix4 viewMatrix {
            get => transform.globalMatrix.ClearScale().Inverted();
        }

        public Matrix4 projectionMatrix {
            get => Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(fov), Game.instance.WindowAspectRatio, zNear, zFar
            );
        }

        public Camera(float fov = 60f, float zNear = 0.01f, float zFar = 100f) : base() {
            this.fov = fov;
            this.zNear = zNear;
            this.zFar = zFar;
        }

        protected override void EnterTree() {
            base.EnterTree();
            if (_current == null)
                MakeCurrent();
        }

        protected override void ExitTree() {
            base.ExitTree();
            if (_current == this)
                _current = null;
        }

        public void MakeCurrent() {
            _current = this;
        }
    }
}