using CommonClasses;

namespace Items
{
    public abstract class AbstractHat : AbstractWearable
    {
        public override ItemType Type => ItemType.Hat;
        public virtual FloatPercentage DegreeOfAristocracy { get; protected set; }

        public AbstractHat(string name, float weight) : base(name, weight) { }
    }
}