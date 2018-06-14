using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldView : ContentPage
    {
        private Planet Planet;
        private Configuration _configuration;
        public WorldView(Planet planet, Configuration configuration)
        {
            _configuration = configuration;
            Planet = planet;
            InitializeComponent();
            Title = planet.Name;

            // Always set the binding contexts even if not currently visible
            BindingContext = planet.Normal;
            UWP.Text = planet.DisplayString;
            Factions.ItemsSource = planet.Normal.Factions;
            tneFactions.ItemsSource = planet.Collapse.Factions;
            tneData.BindingContext = planet.Collapse;
            Satellites.ItemsSource = planet.Satellites;

            Satellites.IsVisible = !(planet.Satellites.Count == 0);
            conflictReason.IsVisible = (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS);
            tneData.IsVisible = (configuration.CurrentCampaign == Campaign.THENEWERA);
            Encounters.IsVisible = planet.Life;

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

        private async void Encounters_Clicked(object sender, System.EventArgs e)
        {
            var regionList = new RegionList(Planet.Encounters);
            await Navigation.PushAsync(regionList);
        }

        private async void Satellites_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var planet = e.SelectedItem as Planet;
            var planetViewer = new WorldView(planet, _configuration);
            await Navigation.PushAsync(planetViewer);
        }
    }
}