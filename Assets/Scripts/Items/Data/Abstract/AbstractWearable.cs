using System;

namespace Items
{
    public abstract class AbstractWearable
    {
        public string Name { get; private set; }
        public float Weight { get; private set; }
        public Guid Guid { get; private set; }

        public abstract ItemType Type { get; }

        public AbstractWearable(string name, float weight)
        {
            Name = name;
            Weight = weight;

            Guid = Guid.NewGuid();
        }
    }
}