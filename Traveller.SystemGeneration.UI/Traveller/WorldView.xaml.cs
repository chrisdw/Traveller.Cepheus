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
            tneCollapse.IsVisible = (configuration.CurrentCampaign == Campaign.THENEWERA);
            if (tneCollapse.IsVisible)
            {
                collapseUPP.Text = travInfo.Collapse.UPPString;
            }
            if (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                Factions.ItemTemplate = (DataTemplate)Resources["hammersSlammersTemplate"];
            }
            else
            {
                Factions.ItemTemplate = (DataTemplate)Resources["classicTemplate"];
            }
        }

        private void butAnother_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}