namespace DudutsEngine {
    public class Wiggler : Component {
        private float t = 0f;

        public Wiggler(float timeOffset = 0f) {
            t = timeOffset;
        }

        public override void Process(float delta) {
            t += delta;
            transform.position.X = (float) System.Math.Sin(t * 16f) * 0.05f;
        }
    }
}