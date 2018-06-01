using System;
using Xamarin.Forms;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public partial class MainPage : ContentPage
	{
        Configuration Config { get; } = new Configuration();

		public MainPage()
		{
			InitializeComponent();
		}

        public void OnCampaignChanged(object sender, EventArgs e)
        {
            if (campaignPicker.SelectedIndex == 0)
            {
                Config.CurrentCampaign = Campaign.CLASSIC;
            }
            else if (campaignPicker.SelectedIndex == 1)
            {
                Config.CurrentCampaign = Campaign.THENEWERA;
            }
            else if (campaignPicker.SelectedIndex == 2)
            {
                Config.CurrentCampaign = Campaign.HOSTILE;
            }
            else
            {
                Config.CurrentCampaign = Campaign.HAMMERSSLAMMERS;
            }
        }

        public void OnGenerateClicked(object sender, EventArgs e)
        {
            var system = new StarSystem();
            Config.SpaceOpera = spaceOperaSwitch.IsToggled;
            Config.HardScience = hardScienceSwitch.IsToggled;

            system.Generate(Config);
            // As this is a basic generation, get a normal UPP
            UPPLabel.Text = system.Information.DisplayString();

            var worldView = new WorldView(system.Mainworld, Config);
            Navigation.PushModalAsync(worldView);
        }

    }
}
