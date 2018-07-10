using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void Generate_Clicked(object sender, EventArgs e)
        {
        }

        private async void Wizard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GenerationWizard());
        }
    }
}
