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

        private void regions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var region = (Region)e.SelectedItem;
            var critterList = new CritterList(region);
            Navigation.PushModalAsync(critterList);
        }

        private void butBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}