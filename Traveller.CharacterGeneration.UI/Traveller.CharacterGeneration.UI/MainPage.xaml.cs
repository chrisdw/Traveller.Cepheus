using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void Generate_Clicked(object sender, EventArgs e)
        {
            Character character = new Character();
            character.Culture = Constants.CultureType.Imperial;
            character.Sex = "Male";
            character.Style = Constants.GenerationStyle.Classic_Traveller;
            character.CharacterSpecies = Character.Species.Human_Imperial;
            character.Generate();

            ICulture culture = new Imperial.Culture();
            var career = culture.GetBasicCareer(Career.CareerType.Imperial_Army);
            career.Owner = character;
            if (career.Enlist())
            {
                character.Careers.Add(career);
            }
        }
    }
}
