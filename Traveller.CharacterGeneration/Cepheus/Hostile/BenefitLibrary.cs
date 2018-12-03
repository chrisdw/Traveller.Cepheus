using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public static class BenefitLibrary
    {
        // Tickets
        public static Benefit StandardTicket = new Benefit("Standard Ticket", 1, Benefit.BenefitType.Material);
        public static Benefit EliteTicket = new Benefit("Elite Ticket", 1, Benefit.BenefitType.Material);

        public static Benefit StarEnvoyClubMember = new Benefit("Star Envoy Club Member", 1, Benefit.BenefitType.Material);
    }
}
