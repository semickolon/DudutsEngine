using System;
using System.Collections.Generic;

namespace DudutsEngine {
    public class GameObject : IDisposable {
        private List<Component> components = new List<Component>();
        private List<GameObject> children = new List<GameObject>();
        public readonly Transform transform = new Transform();
        public GameObject parent { get; protected set; }
        public bool activeInTree {
            get => active && (parent?.activeInTree ?? root);
        }
        public bool isInTree {
            get => parent?.isInTree ?? root;
        }
        public bool active = true;
        private bool root = false;
        private bool disposed = false;

        public GameObject(bool root = false) {
            this.root = root;
            AddComponent(transform);
        }

        public void AddComponent(Component component) {
            components.Add(component);
            component.Attach(this);
        }

        public void AddChild(GameObject child) {
            if (child.parent == null && child != this) {
                children.Add(child);
                child.parent = this;

                if (isInTree)
                    child.EnterTree();
            }
        }

        public void RemoveChild(GameObject child) {
            if (children.Contains(child)) {
                children.Remove(child);
                child.parent = null;

                if (isInTree)
                    child.ExitTree();
            }
        }

        protected virtual void EnterTree() {
            children.ForEach(c => c.EnterTree());
        }

        protected virtual void ExitTree() {
            children.ForEach(c => c.ExitTree());
        }

        public void Process(float delta) {
            if (activeInTree) {
                children.ForEach(c => c.Process(delta));
                components.ForEach(c => c.Process(delta));
            }
        }

        public void Render() {
            if (activeInTree) {
                children.ForEach(c => c.Render());
                components.ForEach(c => c.Render());
            }
        }

        public void Dispose() {
            if (!disposed) {
                // ???
                disposed = true;
            }
        }
    }
}