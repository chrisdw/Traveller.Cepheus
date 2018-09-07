using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace org.DownesWard.Traveller.CharacterGeneration.UI.GTK
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();

            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("Traveller Character Generator");
            window.Show();

            Gtk.Application.Run();
        }
    }
}
