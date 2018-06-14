using org.DownesWard.Traveller.AnimalEncounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class TemperatureData
    {
        public int Row { get; set; }
        public string Summer { get; set; }
        public string Fall { get; set; }
        public string Winter { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanetView : TabbedPage
    {
        private Planet Planet;
        private Configuration _configuration;
        public List<TemperatureData> Temperature { get; } = new List<TemperatureData>();

        public PlanetView(Planet planet, Configuration configuration)
        {
            _configuration = configuration;
            Planet = planet;
            InitializeComponent();
            Title = planet.Name;

            // Always set the binding contexts even if not currently visible
            BindingContext = Planet.Normal;
            UWP.Text = Planet.DisplayString;
            Factions.ItemsSource = Planet.Normal.Factions;
            tneFactions.ItemsSource = Planet.Collapse.Factions;
            tneData.BindingContext = Planet.Collapse;
            Satellites.ItemsSource = Planet.Satellites;
            if (Planet.Life)
            {
                RegionPicker.BindingContext = Planet.Encounters;
                RegionPicker.ItemsSource = Planet.Encounters.Regions;
                RegionPicker.SelectedIndex = 0;
            }

            Satellites.IsVisible = !(Planet.Satellites.Count == 0);
            conflictReason.IsVisible = (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS);
            tneData.IsVisible = (configuration.CurrentCampaign == Campaign.THENEWERA);
            tneFactionData.IsVisible = (configuration.CurrentCampaign == Campaign.THENEWERA);
            // Annoyingly this just hides all the children, not the tab iteself
            Encounters.IsVisible = Planet.Life;

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

            for (var i = 0; i < (Constants.NUM_HEX_ROWS * 2) - 1; i += 2)
            {
                var temp = new TemperatureData()
                {
                    Row = (i / 2 + 1)
                };
                if (configuration.UseFarenheight)
                {
                    temp.Summer = string.Format("{0:N2}/{1:N2}", Common.CtoF(Planet.Summer[i]), Common.CtoF(Planet.Summer[i + 1]));
                    temp.Fall = string.Format("{0:N2}/{1:N2}", Common.CtoF(Planet.Fall[i]), Common.CtoF(Planet.Fall[i + 1]));
                    temp.Winter = string.Format("{0:N2}/{1:N2}", Common.CtoF(Planet.Winter[i]), Common.CtoF(Planet.Winter[i + 1]));
                }
                else
                {
                    temp.Summer = string.Format("{0:N2}/{1:N2}", Planet.Summer[i], Planet.Summer[i+1]);
                    temp.Fall = string.Format("{0:N2}/{1:N2}", Planet.Fall[i], Planet.Fall[i + 1]);
                    temp.Winter = string.Format("{0:N2}/{1:N2}", Planet.Winter[i], Planet.Winter[i + 1]);
                }
                Temperature.Add(temp);
            }

            TemperatureListView.ItemsSource = Temperature;
        }

        private async void Satellites_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var planet = e.SelectedItem as Planet;
            var planetViewer = new PlanetView(planet, _configuration);
            await Navigation.PushAsync(planetViewer);
        }

        private void RegionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var region = RegionPicker.SelectedItem as Region;
            critterList.ItemsSource = region.Critters;
        }
    }
}