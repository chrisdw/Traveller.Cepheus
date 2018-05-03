using System;

namespace org.DownesWard.Traveller.Shared
{
    public class TravCode
    {
        private int maxValue;
        private int currentValue;
        private readonly string list = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        public TravCode()
        {
            currentValue = 0;
            maxValue = list.Length - 1;
        }

        public TravCode(int maximum)
        {
            currentValue = 0;
            if (maximum >= list.Length)
            {
                maxValue = list.Length - 1;
            }
            else if (maximum < 0)
            {
                maxValue = 0;
            }
            else
            {
                maxValue = maximum;
            }
        }

        public int Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                if (value < 0 )
                {
                    currentValue = 0;
                }
                else if (value >= maxValue)
                {
                    currentValue = maxValue;
                }
                else
                {
                    currentValue = value;
                }
            }
        }

        public override string ToString()
        {
            return list.Substring(currentValue, 1);
        }
    }
}
