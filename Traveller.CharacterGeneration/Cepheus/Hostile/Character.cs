﻿using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Character : Cepheus.Character
    {
        
        protected override Skill GetBackgroundSkillList()
        {
            var res = dice.roll(2);
            if (res <= 9)
            {
                Journal.Add(Resources.Msg_BornOnEarth);
                return new Skill()
                {
                    Name = Resources.Skill_Background,
                    Level = 0,
                    Cascade =
                        {
                            CharacterGeneration.SkillLibrary.Admin,
                            SkillLibrary.Agriculture,
                            CharacterGeneration.SkillLibrary.Carousing,
                            CharacterGeneration.SkillLibrary.Communications,
                            CharacterGeneration.SkillLibrary.Computer,
                            CharacterGeneration.SkillLibrary.Electronics,
                            CharacterGeneration.SkillLibrary.Engineering,
                            CharacterGeneration.SkillLibrary.Gambling,
                            SkillLibrary.Investigate,
                            CharacterGeneration.SkillLibrary.Liason,
                            CharacterGeneration.SkillLibrary.Mechanical,
                            CharacterGeneration.SkillLibrary.Medic,
                            CharacterGeneration.SkillLibrary.Steward,
                            CharacterGeneration.SkillLibrary.Streetwise,
                            CharacterGeneration.SkillLibrary.Survival
                        }
                };
            }
            else
            {
                Journal.Add(Resources.Msg_BornOffWorld);
                return new Skill()
                {
                    Name = Resources.Skill_Background,
                    Level = 0,
                    Cascade =
                        {
                            CharacterGeneration.SkillLibrary.Admin,
                            SkillLibrary.Agriculture,
                            SkillLibrary.BladeCombat,
                            CharacterGeneration.SkillLibrary.Carousing,
                            CharacterGeneration.SkillLibrary.Communications,
                            CharacterGeneration.SkillLibrary.Computer,
                            CharacterGeneration.SkillLibrary.Electronics,
                            CharacterGeneration.SkillLibrary.Engineering,
                            CharacterGeneration.SkillLibrary.Gambling,
                            SkillLibrary.Investigate,
                            CharacterGeneration.SkillLibrary.Liason,
                            CharacterGeneration.SkillLibrary.Mechanical,
                            CharacterGeneration.SkillLibrary.Medic,
                            CharacterGeneration.SkillLibrary.Steward,
                            CharacterGeneration.SkillLibrary.Survival,
                            CharacterGeneration.SkillLibrary.VaccSuit,
                            SkillLibrary.Vehicle
                        }
                };
            }
        }
    }
}
