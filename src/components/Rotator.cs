using System;

namespace DudutsEngine {
    public class Rotator : Component {
        public override void Process(float delta) {
            transform.position.X = (float) Math.Sin(Game.instance.GlobalTime) * 0.5f;
            transform.position.Y = (float) Math.Cos(Game.instance.GlobalTime) * 0.5f;
            transform.rotation.X += delta * 30;
            transform.rotation.Y += delta * 17;
        }
    }
}