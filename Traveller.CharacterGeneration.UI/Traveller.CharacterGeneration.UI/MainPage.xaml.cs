using org.DownesWard.Utilities;
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

        private async void Generate_Clicked(object sender, EventArgs e)
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
                var keepGoing = true;
                do
                {
                    if (career.Survival())
                    {
                        if (career.Commission())
                        {
                            career.Promotion();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Career", "Your character died.", "Ok");
                        keepGoing = false;
                        break;
                    }

                    for (var i = 0; i < career.TermSkills; i++)
                    {
                        career.CheckTableAvailablity();
                        var tables = new List<string>();
                        SkillTable table = null;
                        for (var j = 0; j < 4; j++)
                        {
                            if (career.SkillTables[j].Available)
                            {
                                tables.Add(career.SkillTables[j].Name);
                                table = career.SkillTables[j];
                            }
                        }
                        if (tables.Count > 1)
                        {
                            var result = await DisplayActionSheet("Select a skill table", null, null, tables.ToArray());
                            for (var j = 0; j < 4; j++)
                            {
                                if (career.SkillTables[j].Name.Equals(result))
                                {
                                    table = career.SkillTables[j];
                                }
                            }
                        }
                        var die = new Dice(6);
                        var roll = die.roll() - 1;
                        roll = roll.Clamp(0, 5);
                        var skill = table.Skills[roll];
                        var skills = skill.ResolveSkill();
                        if (skills.Count > 1)
                        {
                            var names = skills.Select(s => s.Name);
                            var result = await DisplayActionSheet("Select a skill", null, null, names.ToArray());
                            skill = skills.Where(s => s.Name == result).First();
                        }
                        else
                        {
                            skill = skills[0];
                        }
                        if (skill.Class == Skill.SkillClass.AttributeChange)
                        {
                            switch (skill.Name)
                            {
                                case "STR":
                                    character.Profile.Str.Value += skill.Level;
                                    break;
                                case "DEX":
                                    character.Profile.Dex.Value += skill.Level;
                                    break;
                                case "END":
                                    character.Profile.End.Value += skill.Level;
                                    break;
                                case "INT":
                                    character.Profile.Int.Value += skill.Level;
                                    break;
                                case "EDU":
                                    character.Profile.Edu.Value += skill.Level;
                                    break;
                                case "SOC":
                                    character.Profile.Soc.Value += skill.Level;
                                    break;
                            }
                        }
                        else
                        {
                            character.AddSkill(skill);
                        }
                    }
                    career.EndTerm();
                    if (!character.AgingCheck())
                    {
                        break;
                    }
                    
                    var reup = career.CanRenlist();
                    if (reup == BasicCareer.Renlistment.Must)
                    {
                        career.HandleRenlist(true);
                    }
                    else if (reup == BasicCareer.Renlistment.Can)
                    {
                        // Ask if they want to renlist
                        var result = await DisplayAlert("Career", string.Format("You are now {0}. Do you want to re-enlist", character.Age), "Yes", "No");
                        if (result)
                        {
                            career.HandleRenlist(true);
                        }
                        else
                        {
                            keepGoing = false;
                            career.HandleRenlist(false);
                        }
                    }
                    else
                    {
                        keepGoing = false;
                        career.HandleRenlist(false);
                    }
                } while (keepGoing);
            } 
        }
    }
}
