using org.DownesWard.Traveller.Shared;
using System;

namespace org.DownesWard.Traveller.AnimalEncounters.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            UPP upp = new UPP();
            upp.Atmosphere.Value = 7;
            upp.Hydro.Value = 7;
            upp.Size.Value = 7;
            int tsize = 2;
            TableGenerator table = new TableGenerator();

            foreach (string arg in args)
            {
                if (arg.Length > 0)
                {
                    if (arg[0].ToString().Equals("/"))
                    {
                        if (arg.Length > 1)
                        {
                            tsize = int.Parse(arg[1].ToString());
                            if (tsize != 1 && tsize != 2)
                            {
                                throw new ArgumentException("Invalid table size");
                            }
                        }
                    }
                    else
                    {
                        // it's a UPP
                        if (arg.Length > 1)
                        {
                            upp.Size.Value = int.Parse(arg[0].ToString());
                        }
                        if (arg.Length > 2)
                        {
                            upp.Atmosphere.Value = int.Parse(arg[1].ToString());
                        }
                        if (arg.Length > 3)
                        {
                            upp.Hydro.Value = int.Parse(arg[2].ToString());
                        }
                    }
                }
            }
            table.Generate(tsize, upp);
            table.WriteStreamAsText(Console.Out);
        }
    }
}
