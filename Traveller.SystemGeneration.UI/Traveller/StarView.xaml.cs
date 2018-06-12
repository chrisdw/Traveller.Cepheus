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
		public StarView (Star star)
		{
			InitializeComponent ();
            StarDesc.Text = star.DisplayString;
            var orbits = star.Orbits.Where(o => o.World != null).Select(o => o.World);
            Orbits.ItemsSource = orbits;
            Companions.ItemsSource = star.Companions;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Companions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var companion = e.SelectedItem as CompanionStar;
            var starViewer = new StarView(companion);
            Navigation.PushModalAsync(starViewer);
        }
    }
}