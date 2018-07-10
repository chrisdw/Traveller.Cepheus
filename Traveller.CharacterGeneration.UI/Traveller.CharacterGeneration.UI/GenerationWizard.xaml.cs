using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenerationWizard : CarouselPage
    {
        private GenerationConfiguration generationConfiguration = new GenerationConfiguration();
        private ICulture selectedCulture;
        private Constants.GenerationStyle generationStyle;
        private Character.Species species;

        public ICommand GenerateCommand { private set; get; }

        public GenerationWizard()
        {
            BindingContext = generationConfiguration;
            InitializeComponent();
        }

        private async void Generate_Clicked(object sender, EventArgs e)
        {
            Character character = new Character
            {
                Culture = selectedCulture.Id,
                Sex = generationConfiguration.Sex,
                Style = generationStyle,
                CharacterSpecies = species
            };
            character.Generate();

            var keepgoing = false;
            do
            {
                var clist = selectedCulture.Careers(character);
                var careerList = clist.Keys.Except(character.Careers.Select(c => c.Name));
                var selected = await DisplayActionSheet(Properties.Resources.Prompt_Select_Career, null, null, careerList.ToArray());
                var career = selectedCulture.GetBasicCareer(clist[selected]);
                career.Owner = character;
                if (career.Enlist())
                {
                    character.Careers.Add(career);
                    await ResolveBasicCareer(character, career);
                }
                else
                {
                    // Unable to enlist, submit to draft?
                    var res = await DisplayAlert(Properties.Resources.Title_App, 
                        Properties.Resources.Prompt_Draft, 
                        Properties.Resources.Button_Yes,
                        Properties.Resources.Button_No);
                    if (res)
                    {
                        do
                        {
                            career = selectedCulture.Drafted(character);
                        } while (character.Careers.Contains(career));
                        career.Drafted = true;
                        career.Owner = character;
                        await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Msg_Drafted, career.Name), Properties.Resources.Button_OK);
                        character.Careers.Add(career);
                        await ResolveBasicCareer(character, career);
                    }
                }
                if (selectedCulture.MultipleCareers && !character.Died && !(character.Careers.Sum(c => c.TermsServed) > 6))
                {
                    var doagain = await DisplayAlert(Properties.Resources.Title_App, 
                        Properties.Resources.Prompt_Another_Career, 
                        Properties.Resources.Button_Yes, 
                        Properties.Resources.Button_No);
                    if (doagain)
                    {
                        keepgoing = true;
                    }
                    else
                    {
                        keepgoing = false;
                    }
                }
            } while (keepgoing);
            // check to see if the total of skill levels is greater than the sum of
            // int and edu, if it is they have to be reduced
            var total = character.Skills.Values.Sum(a => a.Level);
            if (total > character.Profile.Int.Value + character.Profile.Edu.Value)
            {
                // Need to reduce skills count
                await DisplayAlert(Properties.Resources.Title_App, Properties.Resources.Msg_Skill_Total, Properties.Resources.Button_OK);
                var skillView = new SkillView(character);
                await Navigation.PushModalAsync(skillView);
            }
            var characterView = new CharacterViewer(character);
            await Navigation.PushAsync(characterView);
        }

        private void Ruleset_SelectedIndexChanged(object sender, EventArgs e)
        {
            Campaign.Items.Clear();
            switch (generationConfiguration.Ruleset)
            {
                case "Classic":
                    Campaign.Items.Add("3rd Imperium");
                    generationStyle = Constants.GenerationStyle.Classic_Traveller;
                    break;
                case "Mega Traveller":
                    generationStyle = Constants.GenerationStyle.Mega_Traveller;
                    Campaign.Items.Add("The Rebellion");
                    break;
                case "Cepheus Engine":
                    generationStyle = Constants.GenerationStyle.Cepheus_Engine;
                    Campaign.Items.Add("Generic");
                    Campaign.Items.Add("Hostile");
                    Campaign.Items.Add("Orbital");
                    break;
            }
        }

        private void Campaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            Culture.Items.Clear();
            switch (generationConfiguration.Campaign)
            {
                case "3rd Imperium":
                    Culture.Items.Add("Imperial");
                    Culture.Items.Add("Zhodani");
                    Culture.Items.Add("Solomani");
                    Culture.Items.Add("Aslan");
                    Culture.Items.Add("Vargr");
                    Culture.Items.Add("Darrian");
                    Culture.Items.Add("Sword Worlds");
                    Culture.Items.Add("Droyne");
                    break;
            }
        }

        private void Culture_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (generationConfiguration.Culture)
            {
                case "Imperial":
                    selectedCulture = new Classic.Imperial.Culture();
                    break;
            }
            if (selectedCulture != null)
            {
                Species.ItemsSource = selectedCulture.Species(generationStyle).Keys.ToList();
            }
        }

        private void Species_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sex.Items.Clear();
            if (selectedCulture != null)
            {
                species = selectedCulture.Species(generationStyle)[Species.SelectedItem.ToString()];
            }
            switch (generationConfiguration.Species)
            {
                default:
                    Sex.Items.Add("Male");
                    Sex.Items.Add("Female");
                    break;
            }
        }

        private async Task ResolveBasicCareer(Character character, BasicCareer career)
        {
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
                    character.Died = true;
                    character.Journal.Add(string.Format(Properties.Resources.Jrn_Killed, character.Age));
                    await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Msg_Killed, character.Age), Properties.Resources.Button_OK);
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
                        var result = await DisplayActionSheet(Properties.Resources.Prompt_Select_Skill_Table, null, null, tables.ToArray());
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
                        var result = await DisplayActionSheet(Properties.Resources.Prompt_Select_Skill, null, null, names.ToArray());
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
                    character.Died = true;
                    character.Journal.Add(string.Format(Properties.Resources.Jrn_Died, character.Age));
                    await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Msg_Died, character.Age), Properties.Resources.Button_OK);
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
                    var result = await DisplayAlert(Properties.Resources.Title_App, 
                        string.Format(Properties.Resources.Prompt_Renlist, character.Age),
                        Properties.Resources.Button_Yes,
                        Properties.Resources.Button_No);
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

            career.MusterOut();
            var muster = career.MusterOutRolls();
            var cashRolls = muster.Clamp(0, 3);
            await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Prompt_Benefit_Rolls, muster, cashRolls), Properties.Resources.Button_OK);
            for (var i = 0; i < muster; i++)
            {
                if (cashRolls > 0)
                {
                    var result = await DisplayActionSheet(Properties.Resources.Prompt_Select_Benefit_Table, 
                        null, null, Properties.Resources.Benefit_Cash, Properties.Resources.Benefit_Material);
                    if (result.Equals(Properties.Resources.Benefit_Cash))
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
                            var picked = await DisplayAlert(Properties.Resources.Title_App, Properties.Resources.Prompt_Weapon_Skill, Properties.Resources.Button_Yes, Properties.Resources.Button_No);

                            var list = Benefit.GetWeaponList(picks.benefit);
                            var title = string.Empty;
                            if (picked)
                            {
                                title = Properties.Resources.Prompt_Select_Skill;
                            }
                            else
                            {
                                title = Properties.Resources.Prompt_Select_Weapon;
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
                            var selected = await DisplayActionSheet(Properties.Resources.Prompt_Select_Skill, null, null, list.ToArray());
                            character.AddSkill(new Skill() { Level = 1, Name = selected });
                        }
                        else if (picks.pick == Career.BenefitPick.Weapon)
                        {
                            // select a class of weapon
                            var list = Benefit.GetWeaponList(picks.benefit);
                            var selected = await DisplayActionSheet(Properties.Resources.Prompt_Select_Weapon, null, null, list.ToArray());
                            character.AddBenefit(new Benefit() { Name = selected, TypeOfBenefit = Benefit.BenefitType.Weapon, Value = 1 });
                        }
                    }
                }
                else
                {
                    career.ResolveMaterialBenefit();
                }
            }
        }
    }
}