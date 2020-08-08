using System;

namespace DudutsEngine {
    public class Component : IDisposable {
        public GameObject gameObject { get; private set; }
        public Transform transform { get => gameObject.transform; }
        private bool disposed = false;

        public void Attach(GameObject gameObject) {
            if (this.gameObject == null)
                this.gameObject = gameObject;
        }

        public virtual void Ready() {}

        public virtual void Process(float delta) {}

        public virtual void Render() {}

        protected virtual void _Dispose() {}

        public void Dispose() {
            if (!disposed) {
                _Dispose();
                disposed = true;
            }
        }
    }
}