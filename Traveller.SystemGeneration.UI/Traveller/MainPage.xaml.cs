﻿using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Xamarin.Forms;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public partial class MainPage : ContentPage
    {
        private Configuration Config { get; } = new Configuration();
        private StarSystem CurrentStarSystem { get; set; }

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
                GenerateFactionsSwitch.On = true;
            }
        }

        private void HardScienceSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (hardScienceSwitch.On && !spaceOperaSwitch.On)
            {
                spaceOperaSwitch.On = true;
            }
        }

        private void StartportTablePicker_SelectedIndexChanged(object sender, EventArgs e)
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

        private void SpaceOperaSwitch_OnChanged(object sender, ToggledEventArgs e)
        {
            if (!spaceOperaSwitch.On && hardScienceSwitch.On)
            {
                hardScienceSwitch.On = false;
            }
        }

        private void GenerateButton_Clicked(object sender, EventArgs e)
        {
            Analytics.TrackEvent("SystemGenerated", 
                new Dictionary<string, string> {
                    { "Fullsystem" , fullSystemSwitch.On.ToString() },
                    { "Campaign", Config.CurrentCampaign.ToString() }
                }
            );
            Config.SpaceOpera = spaceOperaSwitch.On;
            Config.HardScience = hardScienceSwitch.On;
            Config.UseGaiaFactor = gaiaFactorSwitch.On;
            Config.GenerateTravInfo = travellerInfoSwitch.On;
            Config.UseFarenheight = farenheightSwitch.On;
            Config.BaseName = baseName.Text;
            Config.GenerateFactions = GenerateFactionsSwitch.On;
            if (fullSystemSwitch.On)
            {
                Config.Generation = GenerationType.FULL;
            }

            CurrentStarSystem = new StarSystem(Config);
            CurrentStarSystem.Develop(Config);

            panResult.IsVisible = true;
            // As this is a basic generation, get a normal UWP
            UWPLabel.Text = CurrentStarSystem.Information.DisplayString() + CurrentStarSystem.BG;
        }

        private async void ViewWorldButton_Clicked(object sender, EventArgs e)
        {
            if (CurrentStarSystem == null)
            {
                await DisplayAlert("Warning", "Please generate a system first.", "OK");
            }
            else
            {
                var worldView = new PlanetView(CurrentStarSystem.Mainworld, Config);
                await Navigation.PushAsync(worldView);
            }
        }

        private async void ViewSystemButton_Clicked(object sender, EventArgs e)
        {
            if (CurrentStarSystem == null)
            {
                await DisplayAlert("Warning", "Please generate a system first.", "OK");
            }
            else
            {
                var systemView = new SystemView(CurrentStarSystem, Config);
                await Navigation.PushAsync(systemView);
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (CurrentStarSystem == null)
            {
                await DisplayAlert("Warning", "Please generate a system first.", "OK");
            }
            else if (string.IsNullOrEmpty(Config.BaseName))
            {
                await DisplayAlert("Warning", "Please give the system a name.", "OK");
            }
            else
            {
                Analytics.TrackEvent("SystemSaved");
                var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var path = Path.Combine(docsPath, Config.BaseName + ".xml");
                if (File.Exists(path))
                {
                    var result = await DisplayAlert("Warning", "File exists: Overwrite?", "Yes", "No");
                    if (!result)
                    {
                        return;
                    }
                }
                XmlDocument doc = new XmlDocument();
                CurrentStarSystem.SaveToXML(doc, Config);
                var writer = XmlWriter.Create(path);
                doc.WriteTo(writer);
                writer.Close();
            }
        }

        private void baseName_Completed(object sender, EventArgs e)
        {
            Config.BaseName = baseName.Text;
        }
    }
}
