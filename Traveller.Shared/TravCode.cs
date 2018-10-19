using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.Shared
{
    public class TravCode : INotifyPropertyChanged
    {
        private readonly int maxValue;
        private int currentValue;
        private readonly string list = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public string ShortName { get; set; }

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

        public TravCode(int maximum, string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
            currentValue = 0;
            maxValue = maximum.Clamp(0, list.Length - 1);
        }

        public TravCode(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
            currentValue = 0;
            maxValue = list.Length - 1;
        }

        public int Modifier {
            get
            {
                if (Value <= 2) return -2;
                if (Value >= 3 && Value <= 5) return -1;
                if (Value >= 6 && Value <= 8) return 0;
                if (Value >= 9 && Value <= 11) return 1;
                if (Value >= 12 && Value <= 14) return 2;
                if (Value >= 15 && Value <= 17) return 3;
                if (Value >= 18 && Value <= 20) return 4;
                if (Value >= 21 && Value <= 23) return 5;
                if (Value >= 24 && Value <= 26) return 6;
                if (Value >= 27 && Value <= 29) return 7;
                if (Value >= 30 && Value <= 32) return 8;
                if (Value >= 33) return 9;
                return 0;
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
                if (value != currentValue)
                {
                    currentValue = value.Clamp(0, maxValue);
                    NotifyPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return list.Substring(currentValue, 1);
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
