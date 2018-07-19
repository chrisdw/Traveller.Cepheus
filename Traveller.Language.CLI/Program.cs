using System;

namespace org.DownesWard.Traveller.Language.CLI
{
    class Program
    {
        /// <summary>
        /// First argument is language name
        /// Second argument is number of words (if not specified this is 1)
        /// Thrid argument is number of sylaballes per word (if not specified this is random)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var langs = new Languages();
            if (args.Length == 0)
            {
                Console.WriteLine(Properties.Resources.Msg_No_Language);
                return;
            }
            var lang = args[0].ToLowerInvariant();
            if (!langs.ContainsKey(lang))
            {
                Console.WriteLine(string.Format(Properties.Resources.Msg_Language_Not_Found, lang));
                return;
            }
            var wordCount = 1;
            var syl = -1;
            if (args.Length > 1)
            {
                wordCount = int.Parse(args[1]);
                
                if (args.Length > 2)
                {
                    syl = int.Parse(args[1]);
                } 
            }        
            for (var i = 0; i < wordCount; i++)
            {
                Console.WriteLine(langs[lang].GenerateWord(syl));
            }
        }
    }
}
