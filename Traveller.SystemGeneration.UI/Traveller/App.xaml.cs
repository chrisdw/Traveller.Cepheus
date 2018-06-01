using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new Traveller.SystemGeneration.MainPage();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            Microsoft.AppCenter.AppCenter.Start(
                "uwp=23648e12-a43b-41bb-a828-568eaf846814;"  + 
                "android=6b33c373-f91a-4a52-94f9-5d7da47e46f1", 
                typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
