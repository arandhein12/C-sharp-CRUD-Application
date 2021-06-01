using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;

namespace Open.Tests.Aids
{
    [TestClass]
    public class SystemStringTests
    {
        [TestMethod]
        public void StartsWithLetterTest()
        
        {
            Assert.IsTrue(SystemString.StartsWithLetter("abc"));
            Assert.IsFalse(SystemString.StartsWithLetter("1abc"));

            Assert.IsFalse(SystemString.StartsWithLetter(""));
            Assert.IsFalse(SystemString.StartsWithLetter(null));
        }

        

        [TestMethod]
            public void ToBackwardsTest()
            {
                Assert.AreEqual("sba", SystemString.ToBackwards("abs"));
                Assert.AreEqual("  ", SystemString.ToBackwards("  "));
                Assert.AreEqual("", SystemString.ToBackwards(""));
                Assert.AreEqual(string.Empty, SystemString.ToBackwards(null));


        }
    }
}


/*[TestMethod]
        public void ToBackwardsUsingForTest()
        {
            Assert.AreEqual("cba", SystemString.ToBackwardsUsingFor("abs"));
            Assert.AreEqual("   ", SystemString.ToBackwardsUsingFor("   "));
            Assert.AreEqual("", SystemString.ToBackwardsUsingFor(""));
            Assert.AreEqual("", SystemString.ToBackwardsUsingFor(null));

            }

        [TestMethod]
        public void FasterToBackwardsAlgorithmTest()
        {
            var d1 = DateTime.Now.Ticks;
            for (var i = 0; i < 1000000; i++) SystemString.ToBackwards("abc");
            var d2 = DateTime.Now.Ticks;

            for (var i = 0; i < 1000000; i++) SystemString.ToBackwardsUsingFor("abc");
            var d3 = DateTime.Now.Ticks;
            var r1 = d2 - d1;
            var r2 = d3 - d2;
            Assert.AreEqual(r1, r2);
        }

    }

  } */
