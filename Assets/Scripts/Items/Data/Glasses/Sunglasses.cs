using System;

namespace Items
{
    public class Sunglasses : AbstractGlasses
    {
        public Sunglasses(string name, float weight, float lightTransmittance) : base(name, weight)
        {
            LightTransmittance = lightTransmittance;
        }
    }
}