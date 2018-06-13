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
	public partial class SystemView : ContentPage
	{
        private Configuration _configuration;

		public SystemView (StarSystem starSystem, Configuration configuration)
		{
            _configuration = configuration;
			InitializeComponent ();
            PrimaryDesc.Text = starSystem.Primary.DisplayString;
            var orbits = starSystem.Primary.Orbits.Where(o => o.World != null).Select(o => o.World);
            Orbits.ItemsSource = orbits;
            Companions.ItemsSource = starSystem.Primary.Companions;
		}

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Companions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var companion = e.SelectedItem as CompanionStar;
            var starViewer = new StarView(companion, _configuration);
            Navigation.PushModalAsync(starViewer);
        }

        private void Orbits_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var planet = e.SelectedItem as Planet;
            var planetViewer = new WorldView(planet, _configuration);
            Navigation.PushModalAsync(planetViewer);
        }
    }
}