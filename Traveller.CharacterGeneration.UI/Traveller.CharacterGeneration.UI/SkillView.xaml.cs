using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkillView : ContentPage
    {
        public SkillView(Character character)
        {
            InitializeComponent();
            BindingContext = character;
            SkillsView.ItemsSource = character.Skills.Values.OrderBy(s => s.Name);
        }

        private void Decrease_Clicked(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            var sk = mi.CommandParameter as Skill;
            sk.Level--;
            var character = BindingContext as Character;
            CheckTotal(character);
        }

        private void CheckTotal(Character character)
        {
            var total = character.Skills.Values.Sum(a => a.Level);
            if (total <= character.Profile.Int.Value + character.Profile.Edu.Value)
            {
                OK.IsEnabled = true;
            }
        }

        private async void OK_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Removed_Clicked(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            var sk = mi.CommandParameter as Skill;
            var character = BindingContext as Character;
            character.Skills.Remove(sk.Name);
            CheckTotal(character);
        }
    }
}