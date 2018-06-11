using org.DownesWard.Traveller.AnimalEncounters;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CritterList : ContentPage
    {
        public ObservableCollection<Critter> Items { get; set; }

        public CritterList(Region region)
        {
            InitializeComponent();
            BindingContext = region;

            Items = new ObservableCollection<Critter>(region.Critters);
			
			critterList.ItemsSource = Items;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}
