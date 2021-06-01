using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;

namespace Open.Tests.Aids
{
    [TestClass]

    public class SystemCultureInfoTests : BaseTests {
        [TestInitialize]

        public override void TestInitialize()
        {
            base.TestInitialize();
            type = typeof(SystemCultureInfo);
        }

        [TestMethod]
        public void GetSpecificCulturesTest()
        {
            var expected = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var actual = SystemCultureInfo.GetSpecificCultures();
            Assert.AreEqual(expected.Length, actual.Length);
            foreach (var a in actual) {
                if (expected.Contains(a)) continue;
                Assert.Fail("{0} is not in list", a);
            }
        }

        //Teine test
        [TestMethod]
        public void GetCulturesTest()
        {
            var allCultures = CultureTypes.AllCultures;
            var expected = CultureInfo.GetCultures(allCultures);
            var actual = SystemCultureInfo.GetCultures(allCultures);
            Assert.AreEqual(expected.Length, actual.Length);
            foreach (var a in actual) {
                if (expected.Contains(a)) continue;
                Assert.Fail("{0} is not in list", a);
            }
        }


    
    //Viimane test mille lisasin

        [TestMethod]
        public void ToRegionInfoTest()
        {
            var cultures = new CultureInfo("et-EE");
            var regions = SystemCultureInfo.ToRegionInfo(cultures);
            Assert.AreEqual("EST", regions.ThreeLetterISORegionName);
            Assert.AreEqual("EE", regions.TwoLetterISORegionName);
            Assert.AreEqual("Estonia", regions.EnglishName);
        }
        //selle testi lõpp

        [TestMethod]
        public void ToRegionInfoWithNullArgumentTest()
        {
            var r = SystemCultureInfo.ToRegionInfo(null);
            Assert.IsNull(r);
        }

        [TestMethod]
        public void GetCulturesWithWrongArgumentTest()
        {
            var types = CultureTypes.UserCustomCulture & CultureTypes.AllCultures;
            var c = SystemCultureInfo.GetCultures(types);
            Assert.IsNotNull(c);
            Assert.AreEqual(0, c.Length);
        }
    }
}










