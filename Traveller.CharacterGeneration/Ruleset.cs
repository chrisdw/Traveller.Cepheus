using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Ruleset
    {
        public static List<string> GetSupportRulesets()
        {
            var list = new List<string>();
            list.Add("Classic");
            list.Add("Mega Traveller");
            list.Add("Mongoose");
            list.Add("Cepheus Engine");
            return list;
        }
    }
}
