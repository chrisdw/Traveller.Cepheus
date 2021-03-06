﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace org.DownesWard.Traveller.SystemGeneration.UI.GTK
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();

            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("Traveller System Generator");
            window.Show();

            Gtk.Application.Run();
        }
    }
}
