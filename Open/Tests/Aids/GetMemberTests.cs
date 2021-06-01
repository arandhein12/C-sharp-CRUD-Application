using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Country;
using Open.Domain.Country;
using Open.Facade.Country;
namespace Open.Tests.Aids {

    [TestClass] public class GetMemberTests : BaseTests {
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            type = typeof(GetMember);
        }
        [TestMethod] public void NameTest() {
            Assert.AreEqual("DbRecord", GetMember.Name<CountryObject>(o => o.DbRecord));
            Assert.AreEqual("Name", GetMember.Name<CountryDbRecord>(o => o.Name));
            Assert.AreEqual("NameTest", GetMember.Name<GetMemberTests>(o => o.NameTest()));
            Assert.AreEqual(string.Empty, GetMember.Name((Expression<Func<CountryDbRecord, object>>) null));
            Assert.AreEqual(string.Empty,  GetMember.Name((Expression<Action<CountryDbRecord>>)null));
        }
        [TestMethod] public void DisplayNameTest() {
            Assert.AreEqual("DbRecord", GetMember.DisplayName<CountryObject>(o => o.DbRecord));
            Assert.AreEqual("Valid From",
                GetMember.DisplayName<CountryViewModel>(o => o.ValidFrom));
            Assert.AreEqual("Name", GetMember.DisplayName<CountryViewModel>(o => o.Name));
            Assert.AreEqual("Valid To", GetMember.DisplayName<CountryViewModel>(o => o.ValidTo));
            Assert.AreEqual(string.Empty, GetMember.DisplayName<CountryViewModel>(null));
            //Impossible to use for methods
            //Assert.AreEqual(string.Empty, GetMember.DisplayName<GetMemberTests>(o => o.NameTest()));
        }
    }
}