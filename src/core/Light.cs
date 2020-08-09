using System.Collections.Generic;
using OpenTK;

namespace DudutsEngine {
    public abstract class Light : GameObject {
        public static readonly List<Light> allLights = new List<Light>();
        public Vector3 color;
        public float intensity;

        public virtual Vector4 uniformColor {
            get => new Vector4(color, intensity);
        }

        public abstract Vector4 uniformPosition { get; }

        protected Light(Vector3 color, float intensity) {
            this.color = color;
            this.intensity = intensity;
        }

        protected override void EnterTree() {
            base.EnterTree();
            allLights.Add(this);
        }

        protected override void ExitTree() {
            base.ExitTree();
            allLights.Remove(this);
        }
    }
}