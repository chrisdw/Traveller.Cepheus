using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldView : ContentPage
    {
        public WorldView(TravInfo travInfo, Configuration configuration)
        {
            InitializeComponent();
            BindingContext = travInfo;
            Factions.ItemsSource = travInfo.Factions;
            conflictReason.IsVisible = (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS);
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