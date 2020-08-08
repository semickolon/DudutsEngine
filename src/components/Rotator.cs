using System;

namespace DudutsEngine {
    public class Rotator : Component {
        public override void Process(float delta) {
            transform.position.X = (float) Math.Sin(Game.GlobalTime) * 0.5f;
            transform.position.Y = (float) Math.Cos(Game.GlobalTime) * 0.5f;
            transform.rotation.X += delta * 30;
        }
    }
}