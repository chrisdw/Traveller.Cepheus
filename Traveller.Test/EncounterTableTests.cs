using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Traveller.Test
{
    [TestClass]
    public class EncounterTableTests
    {
        [TestMethod]
        public void CepheusTableTest()
        {
            var tg = new org.DownesWard.Traveller.AnimalEncounters.Cepheus.TableGenerator();
            tg.Generate(2);
        }
    }
}
