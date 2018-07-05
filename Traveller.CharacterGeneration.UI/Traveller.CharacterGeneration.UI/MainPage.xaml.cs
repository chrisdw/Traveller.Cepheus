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
            Character character = new Character
            {
                Culture = Constants.CultureType.Imperial,
                Sex = "Male",
                Style = Constants.GenerationStyle.Classic_Traveller,
                CharacterSpecies = Character.Species.Human_Imperial
            };
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
                            character.Profile[skill.Name].Value += skill.Level;
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
            career.MusterOut();
            var muster = career.MusterOutRolls();
            var cashRolls = muster.Clamp(0, 3);
            await DisplayAlert("Career", string.Format("You have {0} benefits rolls, no more than {1} can be on the cash table", muster, cashRolls), "OK");
            for (var i = 0; i < muster; i++)
            {
                if (cashRolls > 0)
                {
                    var result = await DisplayActionSheet("Select a table", null, null, "Cash", "Material");
                    if (result.Equals("Cash"))
                    {
                        cashRolls--;
                        career.ResolveCashBenefit();
                    }
                    else
                    {
                        var picks = career.ResolveMaterialBenefit();
                        if (picks.pick.HasFlag(Career.BenefitPick.Skill) && picks.pick.HasFlag(Career.BenefitPick.Weapon))
                        {
                           
                            // need to decide between skill or weapon
                            var picked = await DisplayAlert("Career", "You have received a weapon benefit, do you wish to take it as a skill instead?", "Yes", "No");

                            var list = Benefit.GetWeaponList(picks.benefit);
                            var title = string.Empty;
                            if (picked)
                            {
                                title = "Select a skill";
                            }
                            else
                            {
                                title = "Select a weapon";
                            }
                            var selected = await DisplayActionSheet(title, null, null, list.ToArray());
                            if (picked)
                            {
                                character.AddSkill(new Skill() { Level = 1, Name = selected });
                            }
                            else
                            {
                                character.AddBenefit(new Benefit() { Name = selected, TypeOfBenefit = Benefit.BenefitType.Weapon, Value = 1 });
                            }
                        }
                        else if (picks.pick == Career.BenefitPick.Skill)
                        {
                            var list = Benefit.GetWeaponList(picks.benefit);
                            var selected = await DisplayActionSheet("Select a skill", null, null, list.ToArray());
                            character.AddSkill(new Skill() { Level = 1, Name = selected });
                        }
                        else if (picks.pick == Career.BenefitPick.Weapon)
                        {
                            // select a class of weapon
                            var list = Benefit.GetWeaponList(picks.benefit);
                            var selected = await DisplayActionSheet("Select a weapon", null, null, list.ToArray());
                            character.AddBenefit(new Benefit() { Name = selected, TypeOfBenefit = Benefit.BenefitType.Weapon, Value = 1 });
                        }
                    }
                }
                else
                {
                    career.ResolveMaterialBenefit();
                }
            }
            // check to see if the total of skill levels is greater than the sum of
            // int and edu, if it is they have to be reduced
            var total = character.Skills.Values.Sum(a => a.Level);
            if (total > character.Profile.Int.Value + character.Profile.Edu.Value)
            {
                // Need to reduce skills count
                await DisplayAlert("Career", "Your skill total exeeeds the sum of INT and EDU, you need to reduce them", "OK");
            }
            var characterView = new CharacterViewer(character);
            await Navigation.PushAsync(characterView);
        }
    }
}
