using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.DownesWard.Traveller.Language;

namespace Traveller.Test
{
    [TestClass]
    public class LanguageTests
    {
        [TestMethod]
        public void LoadLanguages()
        {
            var languages = new Languages();
            Assert.IsTrue(languages.Count > 0);
        }

        [TestMethod]
        public void GenerateWord()
        {
            var languages = new Languages();
            var language = languages["vilanii"];
            var word = language.GenerateWord();

            Assert.IsNotNull(word);
        }
    }
}
