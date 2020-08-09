using System;
using OpenTK;

namespace DudutsEngine {
    public class Transform : Component {
        public Vector3 position = new Vector3(0);
        public Vector3 rotation = new Vector3(0);
        public Vector3 scale = new Vector3(1);

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
                Matrix4 parentMatrix = gameObject.parent?.transform.globalMatrix ?? Matrix4.Identity;
                return localMatrix * parentMatrix;
            }
        }

        public Vector3 globalPosition {
            get => globalMatrix.ExtractTranslation();
        }

        public Vector3 globalRotation {
            get {
                var q = globalMatrix.ExtractRotation();
                var sqy = q.Y * q.Y;
                var sqz = q.Z * q.Z;
                var sqw = q.W * q.W;
                return new Vector3(
                    MathHelper.RadiansToDegrees((float) Math.Asin(2 * (q.X * q.Z - q.W * q.Y))),
                    MathHelper.RadiansToDegrees((float) Math.Atan2(2 * q.X * q.W + 2 * q.Y * q.Z, 1 - 2 * (sqz + sqw))),
                    MathHelper.RadiansToDegrees((float) Math.Atan2(2 * q.X * q.Y + 2 * q.Z * q.W, 1 - 2 * (sqy + sqz)))
                );
            }
        }

        public Vector3 globalScale {
            get => globalMatrix.ExtractScale();
        }

        public Vector3 forward {
            get {
                return new Vector3(globalMatrix.Inverted().Column2).Normalized();
            }
        }

        public Vector3 up {
            get {
                return new Vector3(globalMatrix.Inverted().Column1).Normalized();
            }
        }

        public Vector3 right {
            get {
                return new Vector3(globalMatrix.Inverted().Column0).Normalized();
            }
        }
    }
}