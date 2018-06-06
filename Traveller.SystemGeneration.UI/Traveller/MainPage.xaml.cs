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

        private void hardScienceSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (hardScienceSwitch.On && !spaceOperaSwitch.On)
            {
                spaceOperaSwitch.On = true;
            }
        }

        private void startportTablePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startportTablePicker.SelectedIndex == 0)
            {
                Config.StarportTable = StarportTableType.BACKWATER;
            }
            else if (startportTablePicker.SelectedIndex == 1)
            {
                Config.StarportTable = StarportTableType.STANDARD;
            }
            else if (startportTablePicker.SelectedIndex == 2)
            {
                Config.StarportTable = StarportTableType.MATURE;
            }
            else
            {
                Config.StarportTable = StarportTableType.CLUSTER;
            }
        }

        private void spaceOperaSwitch_OnChanged(object sender, ToggledEventArgs e)
        {
            if (!spaceOperaSwitch.On && hardScienceSwitch.On)
            {
                hardScienceSwitch.On = false;
            }
        }

        private void generateButton_Clicked(object sender, EventArgs e)
        {
            var system = new StarSystem();
            Config.SpaceOpera = spaceOperaSwitch.On;
            Config.HardScience = hardScienceSwitch.On;
            Config.UseGaiaFactor = gaiaFactorSwitch.On;
            Config.GenerateTravInfo = travellerInfoSwitch.On;
            Config.UseFareheight = farenheightSwitch.On;
            Config.BaseName = baseName.Text;
            if (fullSystemSwitch.On)
            {
                Config.Generation = GenerationType.FULL;
            }
            system.Generate(Config);
            // As this is a basic generation, get a normal UPP
            UPPLabel.Text = system.Information.DisplayString();

            var worldView = new WorldView(system.Mainworld, Config);
            Navigation.PushModalAsync(worldView);
        }
    }
}
