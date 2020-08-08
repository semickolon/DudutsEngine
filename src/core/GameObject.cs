using System;
using System.Collections.Generic;

namespace DudutsEngine {
    public class GameObject : IDisposable {
        private List<Component> components = new List<Component>();
        public readonly Transform transform = new Transform();
        private bool disposed = false;

        public GameObject() {
            AddComponent(transform);
        }

        public void AddComponent(Component component) {
            components.Add(component);
            component.Attach(this);
        }

        public void Process(float delta) {
            components.ForEach(c => c.Process(delta));
        }

        public void Render() {
            components.ForEach(c => c.Render());
        }

        public void Dispose() {
            if (!disposed) {
                // ???
                disposed = true;
            }
        }
    }
}