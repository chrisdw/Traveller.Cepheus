using org.DownesWard.Traveller.Language;
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
            if (character != null)
            {
                BindingContext = character;
                SkillsView.ItemsSource = character.Skills.Values.OrderBy(s => s.Name);
                BenefitsView.ItemsSource = character.Benefits.Values.OrderBy(b => b.Name);
            }
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

        private void LoadMenu_Activated(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // Get list of files in directory 
            var files = Directory.GetFiles(path, "*.xml");
            // Present list to user to select one
            var filenames = files.Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayActionSheet(Properties.Resources.Prompt_SelectCharacter, null, null, filenames);
                if (result != null)
                {
                    // Open file
                    var pathname = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), result + ".xml");
                    var reader = XmlReader.Create(pathname);
                    var doc = new XmlDocument();
                    doc.Load(reader);
                    var styleStr = doc.GetElementsByTagName("System")[0].InnerText;
                    if (styleStr.Contains("Cepheus"))
                    {
                        var character = Cepheus.Character.Load(doc);
                        BindingContext = character;
                        SkillsView.ItemsSource = character.Skills.Values.OrderBy(s => s.Name);
                        BenefitsView.ItemsSource = character.Benefits.Values.OrderBy(b => b.Name);
                    }
                    else
                    {
                        // Get XML and look to see what the base character we need is
                        // Load the character from that XML
                        var character = Character.Load(doc);
                        BindingContext = character;
                        SkillsView.ItemsSource = character.Skills.Values.OrderBy(s => s.Name);
                        BenefitsView.ItemsSource = character.Benefits.Values.OrderBy(b => b.Name);
                    }
                }
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Get the list of langauages
            var langs = new Languages();
            var list = langs.Keys.ToArray();

            if (list.Length > 0)
            {
                var character = BindingContext as Character;
                if (character.Culture == Constants.CultureType.Zhodani && langs.Keys.Contains("zhodani"))
                {
                    var language = langs["zhodani"];
                    if (character.Profile.Soc.Value < 10)
                    {
                        Name.Text = language.GenerateWord() + " " + language.GenerateWord();
                    }
                    else
                    {
                        var suffix = string.Empty;
                        switch (character.Profile.Soc.Value)
                        {
                            case 10:
                                suffix = "iepr";
                                break;
                            case 11:
                                suffix = "atl";
                                break;
                            case 12:
                                suffix = "stebr";
                                break;
                            case 13:
                                suffix = "tlas";
                                break;
                            case 14:
                                suffix = "tlasche'";
                                break;
                            case 15:
                                suffix = "iashav";
                                break;
                        }
                        Name.Text = language.GenerateWord() + suffix;
                    }
                }
                else {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var select = await DisplayActionSheet(Properties.Resources.Prompt_SelectLanguage, Properties.Resources.Button_Cancel, null, list);
                        if (select != null && select != Properties.Resources.Button_Cancel)
                        {
                            var language = langs[select];

                            Name.Text = language.GenerateWord() + " " + language.GenerateWord();
                        }
                    });
                }
            }
        }
    }
}