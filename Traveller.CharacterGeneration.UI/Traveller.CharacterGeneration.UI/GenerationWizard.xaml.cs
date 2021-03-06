﻿using Microsoft.AppCenter.Analytics;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace org.DownesWard.Traveller.CharacterGeneration.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenerationWizard : CarouselPage
    {
        public GenerationConfiguration GenerationConfiguration { get; } = new GenerationConfiguration();
        private ICulture selectedCulture;
        private Constants.GenerationStyle generationStyle;
        private Character.Species species;

        public ObservableCollection<string> Rulesets { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Campaigns { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Cultures { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Species { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Sexes { get; } = new ObservableCollection<string>();

        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public GenerationWizard()
        {
            BindingContext = this;
            InitializeComponent();
            GenerationConfiguration.PropertyChanged += (a, b) =>
            {
                Generate.IsEnabled = GenerationConfiguration.ConfigurationComplete;
            };
            var list = Ruleset.GetSupportRulesets();
            foreach (var item in list)
            {
                Rulesets.Add(item);
            }
        }

        private async void SkillOffered(object sender, Career.SkillOfferedEventArgs e)
        {
            var skill = e.OfferedSkill;
            var level = skill.Level;
            var skills = e.OfferedSkill.ResolveSkill();

            // TODO: Work out why this is needed
            if (skill.Name.Equals("Prole") || skill.Name.Equals("Talent") || skill.Name.Equals("Psi"))
            {
                return;
            }
            await semaphore.WaitAsync();
            try
            {
                if (skills.Count > 1)
                {
                    var names = skills.Select(s => s.Name).Distinct().OrderBy(s => s);
                    var result = await DisplayActionSheet(string.Format(Properties.Resources.Prompt_Select_Cascade, skill.Name), null, null, names.ToArray());
                    if (!string.IsNullOrEmpty(result))
                    {
                        skill = skills.Where(s => s.Name == result).First();
                    }
                }
                else
                {
                    skill = skills[0];
                }
                if (skill.Class == Skill.SkillClass.AttributeChange)
                {
                    if (GenerationConfiguration.VerboseSkills)
                    {
                        await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Msg_Attribute_Raised, skill.Name, skill.Level), Properties.Resources.Button_OK);
                    }
                    e.Owner.AddAttribute(skill.Name, level);
                }
                else
                {
                    if (GenerationConfiguration.VerboseSkills)
                    {
                        await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Msg_Skill_Received, skill.Name, skill.Level), Properties.Resources.Button_OK);
                    }
                    // Make sure that when resolving a cascade the added skill is
                    // at the level of the top level skill
                    skill.Level = level;
                    e.Owner.AddSkill(skill);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        private async void Generate_Clicked(object sender, EventArgs e)
        {
            Analytics.TrackEvent("Character Generator");
            Character character = null;
            var config = new Character.Configuration()
            {
                Ruleset = GenerationConfiguration.Ruleset,
                Campaign = GenerationConfiguration.Campaign,
                Culture = selectedCulture.Id,
                Sex = GenerationConfiguration.Sex,
                Style = generationStyle,
                CharacterSpecies = species
            };
            character = Character.CreateCharacter(config);
            if (character is Cepheus.Character)
            {
                var cc = character as Cepheus.Character;
                cc.SkillOffered += SkillOffered;
            }

            character.Generate();

            // Let the user see theie stats so they can make a more informed choice about
            // which career to select
            await DisplayAlert("Profile", string.Format("Your UPP is {0}", character.Profile.Display), Properties.Resources.Button_OK);

            var keepgoing = false;
            do
            {
                var clist = selectedCulture.Careers(character);
                // You can't do the same career twice execpt for outcast and drifter - unless forced via the draft
                var careerList = clist.Keys.Except(character.Careers
                    .Where(c => !(c.Name.Equals("Outcast") || c.Name.Equals("Drifter")))
                    .Select(c => c.Name));
                var selected = string.Empty;
                if (careerList.Count() == 1)
                {
                    selected = careerList.ToArray()[0];
                }
                else if (careerList.Count() > 1)
                {
                    selected = await DisplayActionSheet(Properties.Resources.Prompt_Select_Career, null, null, careerList.ToArray());
                }
                else
                {
                    // No careers left to to do - exit
                    keepgoing = false;
                }
                if (!string.IsNullOrEmpty(selected))
                { 
                    var career = selectedCulture.GetBasicCareer(clist[selected]);
                    career.SkillOffered += SkillOffered;
                    if (career is Classic.Zhodani.Career)
                    {
                        var zc = career as Classic.Zhodani.Career;
                        zc.PsionicGamesOffered += DoPsionicGames;
                        zc.PsionicTrainingOffered += DoPsionicTraining;
                    }
                    career.Owner = character;
                    if (career.Enlist())
                    {
                        character.Careers.Add(career);
                        await ResolveBasicCareer(character, career);
                    }
                    else if (selectedCulture.Id == Constants.CultureType.Aslan)
                    {
                        // Aslan don't do draft - go straight to outcast
                        career = selectedCulture.GetBasicCareer(Career.CareerType.Aslan_Outcast);
                        career.Owner = character;
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
                            career.Owner = character;
                            career.Drafted = true;
                            character.Journal.Add(string.Format(Properties.Resources.Jrn_Drafted, career.Name));
                            await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Msg_Drafted, career.Name), Properties.Resources.Button_OK);
                            character.Careers.Add(career);
                            await ResolveBasicCareer(character, career);
                        }
                        else if (selectedCulture.Id == Constants.CultureType.Cepheus_Generic)
                        {
                            // In Cepheus if you don't submit to draft you end up as a drifter
                            career = selectedCulture.GetBasicCareer(Career.CareerType.Cepheus_Drifter);
                            career.Owner = character;
                            character.Careers.Add(career);
                            await ResolveBasicCareer(character, career);
                        }
                    }
                }
                if (selectedCulture.MultipleCareers && 
                    !character.Died && 
                    !(character.Careers.Sum(c => c.TermsServed) > 6) &&
                    keepgoing)
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
                else
                {
                    keepgoing = false;
                }
            } while (keepgoing);
            // check to see if the total of skill levels in non-psionic skills is greater than the sum of
            // int and edu, if it is they have to be reduced
            var total = character.Skills.Values.Where(a => a.Class != Skill.SkillClass.Psionic).Sum(a => a.Level);
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
            Campaigns.Clear();
            var list = Campaign.GetCampaignForRuleset(GenerationConfiguration.Ruleset);
            foreach (var item in list)
            {
                Campaigns.Add(item);
            }
            switch (GenerationConfiguration.Ruleset)
            {
                case "Classic":
                    generationStyle = Constants.GenerationStyle.Classic_Traveller;
                    break;
                case "Mega Traveller":
                    generationStyle = Constants.GenerationStyle.Mega_Traveller;
                    break;
                case "Cepheus Engine":
                    generationStyle = Constants.GenerationStyle.Cepheus_Engine;
                    break;
            }

            Citizens.IsVisible = (generationStyle == Constants.GenerationStyle.Classic_Traveller);
            Mishaps.IsVisible = (generationStyle == Constants.GenerationStyle.Cepheus_Engine);
        }

        private void Campaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cultures.Clear();
            var list = Culture.GetCulturesForCampaign(GenerationConfiguration.Campaign);
            foreach (var item in list)
            {
                Cultures.Add(item);
            }
        }

        private void Culture_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCulture = Culture.CreateCulture(GenerationConfiguration.Culture, GenerationConfiguration.UseCitizens, GenerationConfiguration.UseMishaps);
            if (selectedCulture is Classic.Zhodani.Culture)
            {
                // Psionic training and the like are handled at the "Culture" level
                ((Classic.Zhodani.Culture)selectedCulture).SkillOffered += SkillOffered;
            }
            if (selectedCulture != null)
            {
                Species.Clear();
                var list = selectedCulture.Species(generationStyle).Keys.ToList();
                foreach (var s in list)
                {
                    Species.Add(s);
                }
            }
        }

        private void Species_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sexes.Clear();
            if (selectedCulture != null)
            {
                species = selectedCulture.Species(generationStyle)[GenerationConfiguration.Species];
            }
            var list = CharacterGeneration.Species.SexList(species);
            foreach (var item in list)
            {
                Sexes.Add(item);
            }
        }

        private void DoPsionicGames(object sender, EventArgs e)
        {
            Classic.Zhodani.Career career = sender as Classic.Zhodani.Career;

            Device.BeginInvokeOnMainThread(async () => { 
                var result = await DisplayAlert(Properties.Resources.Title_App,
                    Properties.Resources.Prompt_PsionicGames,
                    Properties.Resources.Button_Yes,
                    Properties.Resources.Button_No);
                if (result)
                {
                    career.PsionicGames();
                }
            });

        }

        private async void DoPsionicTraining(object sender, EventArgs e)
        {
            var die = new Dice(6);
            var zc = sender as Classic.Zhodani.Career;
            zc.Owner.Journal.Add(Properties.Resources.Msg_Psionic_Training);
            var talents = zc.GetTalentsList();
            var top = talents.Count;
            for (int i = 0; i < top; i++)
            {
                var names = talents.Select(s => s.Item1.Name).ToArray();
                var result = await DisplayActionSheet(Properties.Resources.Prompt_Select_Psionic, null, null, names);
                var selected = talents.FirstOrDefault(s => s.Item1.Name == result);
                if (selected != null)
                {
                    if (die.roll(2) - i >= selected.Item2)
                    {
                        selected.Item1.Level = zc.Owner.Profile["PSI"].Value;
                        zc.Owner.AddSkill(selected.Item1);
                    }
                }
                talents = zc.GetTalentsList();
            }
        }

        private async Task ResolveBasicCareer(Character character, BasicCareer career)
        {
            var keepGoing = true;
            do
            {
                var survivalResult = career.Survival();
                if (survivalResult == BasicCareer.SurvivalResult.Survived)
                {
                    if (career.Commission())
                    {
                        career.Promotion();
                    }
                }
                else if (survivalResult == BasicCareer.SurvivalResult.Died)
                {
                    character.Died = true;
                    character.Journal.Add(string.Format(Properties.Resources.Jrn_Killed, character.Age));
                    await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Msg_Killed, character.Age), Properties.Resources.Button_OK);
                    keepGoing = false;
                    break;
                }
                else
                {
                    character.Journal.Add(string.Format("Discharged at age {0}", character.Age));
                    await DisplayAlert(Properties.Resources.Title_App, string.Format("Discharged at age {0}", character.Age), Properties.Resources.Button_OK);
                    break;
                }

                for (var i = 0; i < career.TermSkills; i++)
                {
                    career.CheckTableAvailablity();
                    var tables = new List<string>();
                    SkillTable table = null;
                    for (var j = 0; j < career.SkillTables.Length; j++)
                    {
                        if (career.SkillTables[j].Available)
                        {
                            tables.Add(career.SkillTables[j].Name);
                            table = career.SkillTables[j];
                        }
                    }

                    if (tables.Count > 1)
                    {
                        var result = string.Empty;
                        await semaphore.WaitAsync();
                        try
                        {
                            result = await DisplayActionSheet(Properties.Resources.Prompt_Select_Skill_Table, null, null, tables.ToArray());
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                        for (var j = 0; j < career.SkillTables.Length; j++)
                        {
                            if (career.SkillTables[j].Available && career.SkillTables[j].Name.Equals(result))
                            {
                                table = career.SkillTables[j];
                                break;
                            }
                        }
                    }

                    Skill offeredSkill;
                    var count = 0;
                    do
                    {
                        count++;
                        var die = new Dice(6);
                        var roll = die.roll() - 1 + selectedCulture.TableModifier(character, career, table);
                        roll = roll.Clamp(0, 5);
                        // Because culture rules can modify the skill "on the fly" we work with a clone
                        offeredSkill = table.Skills[roll].Clone();
                    } while (!selectedCulture.CheckSkill(character, offeredSkill, count));

                    var args = new Career.SkillOfferedEventArgs()
                    {
                        OfferedSkill = offeredSkill,
                        Owner = character
                    };
                    SkillOffered(this, args);
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
                    await semaphore.WaitAsync();
                    var result = false;
                    try
                    {
                        // Ask if they want to renlist
                        result = await DisplayAlert(Properties.Resources.Title_App,
                            string.Format(Properties.Resources.Prompt_Renlist, character.Age),
                            Properties.Resources.Button_Yes,
                            Properties.Resources.Button_No);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
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
            if (muster > 0)
            {
                var maxCashRolls = career.MaxCashRolls();
                var cashRolls = muster.Clamp(0, maxCashRolls);
                if (cashRolls > 0)
                {
                    await DisplayAlert(Properties.Resources.Title_App, string.Format(Properties.Resources.Prompt_Benefit_Rolls, muster, cashRolls), Properties.Resources.Button_OK);
                }
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
}