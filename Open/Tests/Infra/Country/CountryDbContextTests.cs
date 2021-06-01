using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Infra.Country;

namespace Open.Tests.Infra.Country {
    [TestClass] public class CountryDbContextTests : CountryDbTests<CountryDbContext> {

        [TestMethod] public void CountriesTest() {
            Assert.IsNotNull(db.Countries);
            
        }

    }
}