using org.DownesWard.Traveller.AnimalEncounters;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegionList : ContentPage
	{
		public RegionList (AnimalEncounters.Encounters encounters)
		{
  			InitializeComponent ();
            BindingContext = encounters;
            regions.ItemsSource = encounters.Regions;
        }

        private async void regions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var region = (Region)e.SelectedItem;
            var critterList = new CritterList(region);
            await Navigation.PushAsync(critterList);
        }
    }
}