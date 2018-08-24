using System;
using System.Linq;

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
            //var character = BindingContext as Character;
            //var xml = character.Serialize();
        }
    }
}