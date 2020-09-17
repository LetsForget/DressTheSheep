using UnityEngine;

namespace CommonClasses
{
    public class TransformData
    {
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3 LocalScale { get; private set; }
        public Transform Parent { get; private set; }

        public TransformData (Transform transform)
        {
            Position = transform.position;
            Rotation = transform.rotation;
            LocalScale = transform.localScale;
            Parent = transform.parent;
        }
    }
}