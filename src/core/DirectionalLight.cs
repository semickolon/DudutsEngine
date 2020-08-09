using OpenTK;

namespace DudutsEngine {
    public class DirectionalLight : Light {
        public override Vector4 uniformPosition {
            get => new Vector4(transform.forward, 0);
        }

        public DirectionalLight(Vector3 color, float intensity = 1f) : base(color, intensity) {}
    }
}