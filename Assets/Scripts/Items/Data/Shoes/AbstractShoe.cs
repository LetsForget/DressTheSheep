namespace Items
{
    public abstract class AbstractShoe : AbstractWearable
    {
        public override ItemType Type => ItemType.Shoes;
        public AbstractShoe(string name, float weight) : base(name, weight) { }
    }
}