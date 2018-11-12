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

        private void SaveMenu_Activated(object sender, EventArgs e)
        {
            var character = BindingContext as Character;
            var doc = new XmlDocument();
            character.SaveXML(doc);
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "character.xml");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;
            var writer = XmlWriter.Create(fileName, settings);
            doc.WriteTo(writer);
            writer.Close();
        }
    }
}