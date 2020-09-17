namespace Items
{
    public abstract class AbstractGlasses : AbstractWearable
    {
        public override ItemType Type => ItemType.Glasses;
        public float LightTransmittance  { get; protected set; }

        public AbstractGlasses(string name, float weight): base(name, weight) { }
    }
}