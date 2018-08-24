using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

		protected override void OnStart ()
		{
            AppCenter.Start("android=dfb3db80-01bd-42b5-83e8-e375307a2ab9;" +
                              "uwp=53bbf029-c983-42ec-8f57-b621e5fba79e;",
                              typeof(Analytics), typeof(Crashes));
            Analytics.TrackEvent("AppStarted");
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
