using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
    public class GenerationConfiguration : INotifyPropertyChanged
    {
        private string ruleset;
        public string Ruleset {
            get
            {
                return ruleset;
            }
            set
            {
                if (value != ruleset)
                {
                    ruleset = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string campaign;
        public string Campaign {
            get
            {
                return campaign;
            }
            set
            {
                if (value != campaign)
                {
                    campaign = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string culture;

        public string Culture {
            get
            {
                return culture;
            }
            set
            {
                if (value != culture)
                {
                    culture = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string species;
        public string Species
        {
            get
            {
                return species;
            }
            set
            {
                if (value != species)
                {
                    species = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string sex;
        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                if (value != sex)
                {
                    sex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ConfigurationComplete
        {
            get
            {
                return string.IsNullOrEmpty(Ruleset) && string.IsNullOrEmpty(Campaign) && string.IsNullOrEmpty(Culture) && string.IsNullOrEmpty(Species) && string.IsNullOrEmpty(Sex);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
