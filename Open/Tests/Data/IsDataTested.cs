using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;

namespace Open.Tests.Data
{
    [TestClass]public class IsDataTested : AssemblyTests {

        private const string assembly = "Open.Data";

        protected override string Namespace(string name)
        {return $"{assembly}.{name}"; }

    [TestMethod]

     public void IsCountryTest()
            {
                Assert.IsFalse (SystemRegionInfo.IsCountry (null));
                isAllClassesTested(assembly, Namespace("Country"));
            }
        }
    }
           