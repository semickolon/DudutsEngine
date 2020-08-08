using System;
using System.Collections.Generic;

namespace DudutsEngine {
    public class GameObject : IDisposable {
        private List<Component> components = new List<Component>();
        private List<GameObject> children = new List<GameObject>();
        public readonly Transform transform = new Transform();
        public GameObject parent { get; protected set; }
        private bool disposed = false;

        public GameObject() {
            AddComponent(transform);
        }

        public void AddComponent(Component component) {
            components.Add(component);
            component.Attach(this);
        }

        public void AddChild(GameObject gameObject) {
            if (gameObject.parent == null && gameObject != this) {
                children.Add(gameObject);
                gameObject.parent = this;
            }
        }

        public void Process(float delta) {
            children.ForEach(c => c.Process(delta));
            components.ForEach(c => c.Process(delta));
        }

        public void Render() {
            children.ForEach(c => c.Render());
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