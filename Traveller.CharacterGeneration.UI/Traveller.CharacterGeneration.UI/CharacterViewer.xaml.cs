using System;
using System.IO;
using System.Linq;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterViewer : TabbedPage
	{
		public CharacterViewer (Character character)
		{
			InitializeComponent ();
            BindingContext = character;
            SkillsView.ItemsSource = character.Skills.Values.OrderBy(s => s.Name);
            BenefitsView.ItemsSource = character.Benefits.Values.OrderBy(b => b.Name);
		}

        private async void SaveMenu_Activated(object sender, EventArgs e)
        {

            var character = BindingContext as Character;
            if (string.IsNullOrWhiteSpace(character.Name))
            {
                await DisplayAlert(Properties.Resources.Title_App, Properties.Resources.Prompt_CharacterName, Properties.Resources.Button_OK);
                return;
            }
            var doc = new XmlDocument();
            character.SaveXML(doc);
            string file = character.Name + ".xml";
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), file);
            if (File.Exists(fileName))
            {
                var result = await DisplayAlert(Properties.Resources.Title_App, Properties.Resources.Prompt_FileExists, 
                    Properties.Resources.Button_Yes, Properties.Resources.Button_No);
                if (result)
                {
                    File.Delete(fileName);
                }
                else
                {
                    return;
                }
            }
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("\t"),
                OmitXmlDeclaration = true
            };
            var writer = XmlWriter.Create(fileName, settings);
            doc.WriteTo(writer);
            writer.Close();
        }

        private async void LoadMenu_Activated(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // Get list of files in directory 
            var files = Directory.GetFiles(path, "*.xml");
            // Present list to user to select one
            var filenames = files.Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayActionSheet("Select a character", null, null, filenames);
                if (result != null)
                {
                    // Open file
                    var pathname = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), result + ".xml");
                    var reader = XmlReader.Create(pathname);
                    var doc = new XmlDocument();
                    doc.Load(reader);
                    // Get XML and look to see what the base character we need is
                    // Load the character from that XML
                }
            });
        }
    }
}