using org.DownesWard.Traveller.Shared;
using System;

namespace org.DownesWard.Traveller.AnimalEncounters.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            UWP uwp = new UWP();
            uwp.Atmosphere.Value = 7;
            uwp.Hydro.Value = 7;
            uwp.Size.Value = 7;
            int tsize = 2;
            bool cepheus = true;

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
                        if (arg.Length > 0)
                        {
                            uwp.Size.Value = int.Parse(arg[0].ToString(), System.Globalization.NumberStyles.HexNumber);
                        }
                        if (arg.Length > 1)
                        {
                            uwp.Atmosphere.Value = int.Parse(arg[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                        }
                        if (arg.Length > 2)
                        {
                            uwp.Hydro.Value = int.Parse(arg[2].ToString(), System.Globalization.NumberStyles.HexNumber);
                        }
                    }
                }
            }

            if (!cepheus)
            {
                TableGenerator table = new TableGenerator();
                table.Generate(tsize, uwp);
                table.WriteStreamAsText(Console.Out);
            }
            else
            {
                var tg = new Cepheus.TableGenerator();
                var tables = tg.Generate(tsize, uwp);
                foreach (var t in tables)
                {
                    t.WriteStreamAsText(Console.Out);
                }
            }
        }
    }
}
