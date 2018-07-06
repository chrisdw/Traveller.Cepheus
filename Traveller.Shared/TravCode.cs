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
