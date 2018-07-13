﻿using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.SwordWorlds
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.SwordWorlds;

        public bool MultipleCareers => false;

        private Dice dice = new Dice(6);

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            return true;
        }

        public Dictionary<string, Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    careers.Add("Army", Career.CareerType.Imperial_Army);
                    careers.Add("Marines", Career.CareerType.Imperial_Marines);
                    careers.Add("Navy", Career.CareerType.Imperial_Navy);
                    careers.Add("Merchants", Career.CareerType.Imperial_Merchants);
                    careers.Add("Patrol", Career.CareerType.SwordWorlds_Patrol);
                    careers.Add("Other", Career.CareerType.Imperial_Other);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            return true;
        }

        public BasicCareer Drafted(Character character)
        {
            switch (dice.roll(1))
            {
                case 1:
                    return new Imperial.BasicNavy() { Culture = this, Drafted = true };
                case 2:
                    return new Imperial.BasicMarines() { Culture = this, Drafted = true };
                case 3:
                    return new Imperial.BasicArmy() { Culture = this, Drafted = true };
                case 4:
                    return new Imperial.BasicMerchants() { Culture = this, Drafted = true };
                case 5:
                    return new Patrol() { Culture = this, Drafted = true };
                case 6:
                    return new Imperial.BasicOther() { Culture = this, Drafted = true };
            }
            // Should never reach here
            return new Imperial.BasicArmy() { Culture = this, Drafted = true };
        }

        public BasicCareer GetBasicCareer(Career.CareerType career)
        {
            switch (career)
            {
                case Career.CareerType.Imperial_Marines:
                    return new Imperial.BasicMarines() { Culture = this };
                case Career.CareerType.Imperial_Army:
                    return new Imperial.BasicArmy() { Culture = this };
                case Career.CareerType.Imperial_Navy:
                    return new Imperial.BasicNavy() { Culture = this };
                case Career.CareerType.Imperial_Merchants:
                    return new Imperial.BasicMerchants() { Culture = this };
                case Career.CareerType.SwordWorlds_Patrol:
                    return new Patrol() { Culture = this };
                case Career.CareerType.Imperial_Other:
                    return new Imperial.BasicOther() { Culture = this };
                default:
                    return new Imperial.BasicArmy() { Culture = this };
            }
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    list.Add("Human (Sword Worlds)", Character.Species.Human_SwordWorlds);
                    break;
            }

            return list;
        }
    }
}