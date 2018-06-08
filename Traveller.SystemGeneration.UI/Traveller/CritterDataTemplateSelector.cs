using org.DownesWard.Traveller.AnimalEncounters;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class CritterDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public DataTemplate CarnivoreTemplate { get; set; }
        public DataTemplate HerbivoreTemplate { get; set; }
        public DataTemplate EventTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var critter = item as Critter;

            if (critter == null)
            {
                return null;
            }
            if (critter.CritterType < 14)
            {
                return HerbivoreTemplate;
            }
            else if (critter.CritterType > 55)
            {
                return EventTemplate;
            }
            else
            {
                return CarnivoreTemplate;
            }
        }
    }
}