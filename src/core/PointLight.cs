using OpenTK;

namespace DudutsEngine {
    public class PointLight : Light {
        public override Vector4 uniformPosition {
            get => new Vector4(transform.globalPosition, 1);
        }

        public PointLight(Vector3 color, float intensity = 1f) : base(color, intensity) {}
    }
}