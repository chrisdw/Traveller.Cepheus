using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StarView : ContentPage
	{
        private Configuration _configuration;
        public StarView (Star star, Configuration configuration)
		{
            _configuration = configuration;

            InitializeComponent ();
            Title = star.DisplayString;
            var orbits = star.Orbits.Where(o => o.World != null).Select(o => o.World);
            Orbits.ItemsSource = orbits;
            Companions.ItemsSource = star.Companions;
        }

        private async void Companions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var companion = e.SelectedItem as CompanionStar;
            var starViewer = new StarView(companion, _configuration);
            await Navigation.PushAsync(starViewer);
        }

        private async void Orbits_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var planet = e.SelectedItem as Planet;
            var planetViewer = new WorldView(planet, _configuration);
            await Navigation.PushAsync(planetViewer);
        }
    }
}