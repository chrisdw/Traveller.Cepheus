using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GenerationWizard : CarouselPage
    {
        private GenerationConfiguration generationConfiguration = new GenerationConfiguration();
        private ICulture selectedCulture;
        private Constants.GenerationStyle generationStyle;
        public GenerationWizard ()
		{
            BindingContext = generationConfiguration;
			InitializeComponent ();
		}

        private async void Generate_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Test", generationConfiguration.Ruleset, "OK");
        }

        private void Ruleset_SelectedIndexChanged(object sender, EventArgs e)
        {
            Campaign.Items.Clear();
            switch (generationConfiguration.Ruleset)
            {
                case "Classic":
                    Campaign.Items.Add("3rd Imperium");
                    generationStyle = Constants.GenerationStyle.Classic_Traveller;
                    break;
                case "Mega Traveller":
                    generationStyle = Constants.GenerationStyle.Mega_Traveller;
                    Campaign.Items.Add("The Rebellion");
                    break;
                case "Cepheus Engine":
                    generationStyle = Constants.GenerationStyle.Cepheus_Engine;
                    Campaign.Items.Add("Generic");
                    Campaign.Items.Add("Hostile");
                    Campaign.Items.Add("Orbital");
                    break;
            }
        }

        private void Campaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            Culture.Items.Clear();
            switch (generationConfiguration.Campaign)
            {
                case "3rd Imperium":
                    Culture.Items.Add("Imperial");
                    Culture.Items.Add("Zhodani");
                    Culture.Items.Add("Solomani");
                    Culture.Items.Add("Aslan");
                    Culture.Items.Add("Vargr");
                    Culture.Items.Add("Darrian");
                    Culture.Items.Add("Sword Worlds");
                    Culture.Items.Add("Droyne");
                    break;
            }
        }

        private void Culture_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (generationConfiguration.Culture)
            {
                case "Imperial":
                    selectedCulture = new Classic.Imperial.Culture();
                    break;
            }
            if (selectedCulture != null)
            {
                Species.ItemsSource = selectedCulture.Species(generationStyle).Keys.ToList();
            }
        }

        private void Species_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sex.Items.Clear();
            switch (generationConfiguration.Species)
            {
                default:
                    Sex.Items.Add("Male");
                    Sex.Items.Add("Female");
                    break;
            }
        }
    }
}