using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Country;
using Open.Domain.Country;

namespace Open.Tests.Domain.Country {

    [TestClass] public class CountryObjectTests : ObjectTests<CountryObject> {
        protected override CountryObject getRandomTestObject() {
            return GetRandom.Object<CountryObject>();
        }

        [TestMethod] public void DbRecordTest() {
            var r = GetRandom.Object<CountryDbRecord>();
            obj = new CountryObject(r);
            Assert.AreSame(r, obj.DbRecord);

        }
        [TestMethod] public void CanCreateWithNullTest() {
            obj = new CountryObject(null);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.DbRecord);
        }
        
        [TestMethod] public void DbRecordIsReadOnlyTest() {
            var name = GetMember.Name<CountryObject>(x => x.DbRecord);
            Assert.IsTrue(IsReadOnly.Field<CountryObject>(name));
        }
    }
}
