using CommonClasses;
using Items;

namespace Items
{
    public class Valenki : AbstractShoe
    {
        public FloatPercentage HeatRetention { get; private set; }

        public Valenki(string name, float weight, float heatRetention) : base (name, weight)
        {
            HeatRetention = heatRetention;
        }
    }
}