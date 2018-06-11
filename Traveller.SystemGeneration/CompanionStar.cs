namespace org.DownesWard.Traveller.SystemGeneration
{
    public class CompanionStar : Star
    {
        public int OrbitNum { get; set; }
        public double Range { get; set; }
        public StarSystem.SystemType SysNat { get; set; }

        public CompanionStar() : base()
        {
            Build();
        }

        public CompanionStar(StellarType stellarType, char stellarClass, char decimalClass) : base(stellarType, stellarClass, decimalClass)
        {
            Build();
        }

        private int ComOrbit()
        {
            var dieroll = Common.d6() + Common.d6();

            switch (dieroll)
            {
                case 2:
                case 3:
                    return 0;
                case 4:
                    return 1;
                case 5:
                    return 2;
                case 6:
                    return 3;
                case 7:
                    return 4 + Common.d6();
                case 8:
                    return 5 + Common.d6();
                case 9:
                    return 6 + Common.d6();
                case 10:
                    return 7 + Common.d6();
                case 11:
                    return 8 + Common.d6();
                case 12:
                    return FAR_ORBIT;
                default:
                    return -1;
            }
        }

        private void Build()
        {
            OrbitNum = ComOrbit();

            if (NumOrbits > (OrbitNum / 2))
            {
                NumOrbits = OrbitNum / 2;
            }

            if (OrbitNum == 0)
            {
                Orbits[0].OrbitalType = Orbit.OrbitType.UNAVAILABLE;
            }

            if (OrbitNum == FAR_ORBIT)
            {
                Range = Common.d6() * 1000;
                SysNat = StarSystem.Nature(true);
                if (SysNat == StarSystem.SystemType.BINARY)
                {
                    NumCompanions = 1;
                    Companions[0] = new CompanionStar();
                    AvaialbleOribits(0);
                }
            }
        }
    }

}
