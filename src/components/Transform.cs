using OpenTK;

namespace DudutsEngine {
    public class Transform : Component {
        public Vector3 position = new Vector3(0);
        public Vector3 rotation = new Vector3(0);
        public Vector3 scale = new Vector3(1);

        public Matrix4 localMatrix {
            get {
                Matrix4 mat4 = new Matrix4(
                    new Vector4(1, 0, 0, position.X),
                    new Vector4(0, 1, 0, position.Y),
                    new Vector4(0, 0, 1, position.Z),
                    new Vector4(0, 0, 0, 1)
                );
                mat4 *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
                mat4 *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
                mat4 *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
                mat4 *= Matrix4.CreateScale(scale);
                return mat4;
            }
        }
    }
}