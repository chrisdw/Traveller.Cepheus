using org.DownesWard.Traveller.Shared.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace org.DownesWard.Traveller.SystemGeneration
{
	public partial class MainPage : ContentPage
	{
        Configuration config { get; } = new Configuration();

		public MainPage()
		{
			InitializeComponent();
		}

        public void OnCampaignChanged(object sender, EventArgs e)
        {
            if (campaignPicker.SelectedIndex == 0)
            {
                config.CurrentCampaign = Campaign.CLASSIC;
            }
            else if (campaignPicker.SelectedIndex == 1)
            {
                config.CurrentCampaign = Campaign.HOSTILE;
            }
            else
            {
                config.CurrentCampaign = Campaign.HAMMERSSLAMMERS;
            }
        }

        public void OnGenerateClicked(object sender, EventArgs e)
        {
            var system = new StarSystem();
            config.SpaceOpera = spaceOperaSwitch.IsToggled;
            config.HardScience = hardScienceSwitch.IsToggled;

            system.Generate(config);
            // As this is a basic generation, get a normal UPP
            UPPLabel.Text = system.Information.DisplayString();
        }

    }
}
