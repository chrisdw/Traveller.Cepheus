using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldView : ContentPage
    {
        public WorldView(Planet travInfo, Configuration configuration)
        {
            InitializeComponent();
            BindingContext = travInfo.Normal;
            Factions.ItemsSource = travInfo.Normal.Factions;
            conflictReason.IsVisible = (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS);
            tneData.IsVisible = (configuration.CurrentCampaign == Campaign.THENEWERA);
            if (tneData.IsVisible)
            {
                tneData.BindingContext = travInfo.Collapse;
                tneFactions.ItemsSource = travInfo.Collapse.Factions;
            }
            if (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                Factions.ItemTemplate = (DataTemplate)Resources["hammersSlammersTemplate"];
                // No post collapse factions in a Hammer's Slammers campaign
            }
            else
            {
                Factions.ItemTemplate = (DataTemplate)Resources["classicTemplate"];
                tneFactions.ItemTemplate = (DataTemplate)Resources["classicTemplate"];
            }

        }

        private void butAnother_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}