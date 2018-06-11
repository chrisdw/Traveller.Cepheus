using System;
using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.Shared
{
    public class TravCode
    {
        private readonly int maxValue;
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
            maxValue = maximum.Clamp(0, list.Length - 1);
        }

        public int Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = value.Clamp(0, maxValue);
            }
        }

        public override string ToString()
        {
            return list.Substring(currentValue, 1);
        }
    }
}
