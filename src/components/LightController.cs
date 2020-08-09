using System;
using OpenTK;

namespace DudutsEngine {
    public class LightController : Component {
        public float moveSpeed = 2f;
        public float radius = 1.5f;
        private float t = 0f;

        public override void Process(float delta) {
            t += delta;
            transform.position.X = (float) Math.Cos(t * moveSpeed) * radius;
            transform.position.Y = (float) Math.Sin(t * moveSpeed) * radius;
        }
    }
}