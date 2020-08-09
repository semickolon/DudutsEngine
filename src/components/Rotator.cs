using System;

namespace DudutsEngine {
    public class Rotator : Component {
        public override void Process(float delta) {
            transform.rotation.X += delta * 45;
        }
    }
}