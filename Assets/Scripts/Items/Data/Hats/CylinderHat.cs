using CommonClasses;
using System;

namespace Items
{
    public class CylinderHat : AbstractHat
    {
        public override FloatPercentage DegreeOfAristocracy => 100;
        public CylinderHat(string name, float weight) : base(name, weight) { }
    }
}