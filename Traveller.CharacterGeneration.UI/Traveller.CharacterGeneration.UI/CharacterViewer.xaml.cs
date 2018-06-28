using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}
	}
}