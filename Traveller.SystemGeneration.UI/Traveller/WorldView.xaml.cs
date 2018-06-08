using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldView : ContentPage
    {
        private Planet Planet;
        public WorldView(Planet planet, Configuration configuration)
        {
            Planet = planet;
            InitializeComponent();

            // Always set the binding contexts even if not currently visible
            BindingContext = planet.Normal;
            Factions.ItemsSource = planet.Normal.Factions;
            tneFactions.ItemsSource = planet.Collapse.Factions;
            tneData.BindingContext = planet.Collapse;

            conflictReason.IsVisible = (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS);
            tneData.IsVisible = (configuration.CurrentCampaign == Campaign.THENEWERA);
            butEncounters.IsVisible = planet.Life;

            if (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                Factions.ItemTemplate = (DataTemplate)Resources["hammersSlammersTemplate"];
                // No post collapse factions in a Hammer's Slammers campaign
                tneFactions.ItemTemplate = (DataTemplate)Resources["classicTemplate"];
            }
            else
            {
                Factions.ItemTemplate = (DataTemplate)Resources["classicTemplate"];
                tneFactions.ItemTemplate = (DataTemplate)Resources["classicTemplate"];
            }
        }

        private void butAnother_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void butEncounters_Clicked(object sender, System.EventArgs e)
        {
            var regionList = new RegionList(Planet.Encounters);
            Navigation.PushModalAsync(regionList);
        }
    }
}