using System.Collections.Generic;
using OpenTK;

namespace DudutsEngine {
    public class PointLight : GameObject {
        public static readonly List<PointLight> allLights = new List<PointLight>();

        public Vector3 color;
        public float intensity;
        public float attenuation;

        public Vector4 uniformColor {
            get => new Vector4(color, intensity);
        }

        public PointLight(Vector3 color, float intensity = 1f, float attenuation = 1f) {
            this.color = color;
            this.intensity = intensity;
            this.attenuation = attenuation;
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