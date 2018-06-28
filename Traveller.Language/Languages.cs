using System.Collections.Generic;
using System.IO;

namespace org.DownesWard.Traveller.Language
{
    public class Languages : Dictionary<string, Language>
    {
        public Languages()
        {
            var root = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(root, "*.ini", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var lang = new Language(file);
                Add(lang.Name.ToLowerInvariant(), lang);
            }
        }
    }
}
