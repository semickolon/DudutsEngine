using OpenTK;

namespace DudutsEngine {
    public class Transform : Component {
        public Vector3 position = new Vector3(0);
        public Vector3 rotation = new Vector3(0);
        public Vector3 scale = new Vector3(1);

        public Vector3 globalPosition {
            get {
                Vector3 parentPosition = gameObject.parent?.transform.globalPosition ?? new Vector3(0);
                return parentPosition + position;
            }
        }

        public Vector3 globalRotation {
            get {
                Vector3 parentRotation = gameObject.parent?.transform.globalRotation ?? new Vector3(0);
                return parentRotation + rotation;
            }
        }

        public Vector3 globalScale {
            get {
                Vector3 parentScale = gameObject.parent?.transform.globalScale ?? new Vector3(1);
                return parentScale * scale;
            }
        }

        public Matrix4 localMatrix {
            get {
                Matrix4 mat4 = Matrix4.Identity;
                mat4 *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
                mat4 *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
                mat4 *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
                mat4 *= Matrix4.CreateScale(scale);
                mat4 *= Matrix4.CreateTranslation(position);
                return mat4;
            }
        }

        public Matrix4 globalMatrix {
            get {
                Matrix4 mat4 = Matrix4.Identity;
                mat4 *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(globalRotation.X));
                mat4 *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(globalRotation.Y));
                mat4 *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(globalRotation.Z));
                mat4 *= Matrix4.CreateScale(globalScale);
                mat4 *= Matrix4.CreateTranslation(globalPosition);
                return mat4;
            }
        }
    }
}