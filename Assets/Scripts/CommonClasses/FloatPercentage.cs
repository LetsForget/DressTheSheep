namespace CommonClasses
{
    public class FloatPercentage
    {
        public static implicit operator FloatPercentage(float value)
        {
            if (value < 0)
            {
                return new FloatPercentage(0);
            }
            else
            {
                if (value > 100)
                {
                    return new FloatPercentage(100);
                }
                else
                {
                    return new FloatPercentage(value);
                }
            }
        }

        public static implicit operator float(FloatPercentage floatPercentage)
        {
            return floatPercentage._value;
        }

        private FloatPercentage(float value) { _value = value; }

        private float _value;
    }
}