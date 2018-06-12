using org.DownesWard.Traveller.SystemGeneration.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration.Campaigns
{
    public class Classic : ICampaign
    {
        public virtual int GenerateSubordinateTechLevel(TravInfo travInfo, TravInfo mainworld)
        {
            var techLevel = mainworld.TechLevel.Value - 1;
            if (travInfo.Remarks.Contains(Languages.Subbase_Scout) ||
                travInfo.Remarks.Contains(Languages.Subbase_Naval) ||
                travInfo.Remarks.Contains(Languages.TradeCode_ResearchColony))
            {
                techLevel += 1;
            }
            return techLevel;
        }

        public virtual int GenerateTechLevel(TravInfo travInfo)
        {
            var techLevel = Common.d6();

            switch (travInfo.Starport)
            {
                case 'A':
                    techLevel += 6;
                    break;
                case 'B':
                    techLevel += 4;
                    break;
                case 'C':
                    techLevel += 2;
                    break;
                case 'X':
                    techLevel -= 4;
                    break;
            }

            switch (travInfo.Size.Value)
            {
                case 0:
                case 1:
                    techLevel += 2;
                    break;
                case 2:
                case 3:
                case 4:
                case 11:
                case 12:
                    techLevel += 1;
                    break;
            }

            switch (travInfo.Atmosphere.Value)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 10:
                    techLevel += 1;
                    break;
                case 13:
                    techLevel -= 2;
                    break;
            }

            switch (travInfo.Hydro.Value)
            {
                case 9:
                    techLevel += 1;
                    break;
                case 10:
                    techLevel += 2;
                    break;
            }

            switch (travInfo.Pop.Value)
            {
                case 9:
                    techLevel += 2;
                    break;
                case 10:
                    techLevel += 4;
                    break;
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    techLevel += 1;
                    break;
            }

            switch (travInfo.Government.Value)
            {
                case 0:
                case 5:
                    techLevel += 1;
                    break;
            }

            if ((travInfo.Hydro.Value == 0 || travInfo.Hydro.Value == 10) && travInfo.Pop.Value > 5)
            {
                if (techLevel < 4)
                {
                    techLevel = 4;
                }
            }

            switch (travInfo.Atmosphere.Value)
            {
                case 0:
                case 2:
                case 3:
                case 10:
                case 11:
                case 12:
                    if (techLevel < 7)
                    {
                        techLevel = 7;
                    }
                    break;
                case 4:
                case 7:
                case 9:
                    if (techLevel < 5)
                    {
                        techLevel = 5;
                    }
                    break;
                case 13:
                case 14:
                    if (travInfo.Hydro.Value == 10)
                    {
                        if (techLevel < 7)
                        {
                            techLevel = 7;
                        }
                    }
                    break;
            }
            return techLevel;
        }

        public string GenerateTradeCodes(TravInfo travInfo)
        {
            var builder = new StringBuilder();

            if ((travInfo.Atmosphere.Value >= 4 && travInfo.Atmosphere.Value <= 9) &&
                (travInfo.Hydro.Value >= 4 && travInfo.Hydro.Value <= 8) && (travInfo.Pop.Value >= 5 && travInfo.Pop.Value <= 7))
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Agricultural);
            }
            if (travInfo.Size.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Asteroid);
            }
            if (travInfo.Pop.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Barren);
            }
            if (travInfo.Atmosphere.Value >= 2 && travInfo.Hydro.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Desert);
            }
            if (travInfo.Atmosphere.Value >= 10 && travInfo.Hydro.Value > 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_FluidOceans);
            }
            if (travInfo.Pop.Value >= 9)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_HighPopulation);
            }
            if (travInfo.Atmosphere.Value <= 1 && travInfo.Hydro.Value > 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_IceCapped);
            }
            if (((travInfo.Atmosphere.Value >= 2 && travInfo.Atmosphere.Value <= 4) || travInfo.Atmosphere.Value == 7 || travInfo.Atmosphere.Value == 9) && travInfo.Pop.Value >= 9)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Industrial);
            }
            if (travInfo.Pop.Value > 0 && travInfo.Hydro.Value <= 4)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_LowPopulation);
            }
            if (travInfo.Atmosphere.Value <= 3 && travInfo.Hydro.Value <= 4 && travInfo.Pop.Value >= 6)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_NonAgricultural);
            }
            if (travInfo.Pop.Value > 0 && travInfo.Pop.Value <= 6)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_NonIndustrial);
            }
            if ((travInfo.Atmosphere.Value >= 2 && travInfo.Atmosphere.Value <= 5) && travInfo.Hydro.Value <= 3 && travInfo.Pop.Value > 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Poor);
            }
            if ((travInfo.Atmosphere.Value == 6 || travInfo.Atmosphere.Value == 8) && (travInfo.Pop.Value >= 6 && travInfo.Pop.Value <= 8) && (travInfo.Government.Value >= 4 && travInfo.Government.Value <= 9))
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Rich);
            }
            if (travInfo.Size.Value > 0 && travInfo.Atmosphere.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_VaccumWorld);
            }
            if (travInfo.Hydro.Value == 10)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_WaterWorld);
            }

            return builder.ToString();
        }
    }
}
