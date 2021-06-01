using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Country;
using Open.Infra.Country;

namespace Open.Tests.Infra.Country{
    [TestClass] public class CountryDbTests<T> : ClassTests<T> {
        protected readonly CountryDbContext db;
        protected readonly CountryObjectsRepository repository;
        protected const int count = 10;

        public CountryDbTests() {
            var options = new DbContextOptionsBuilder<CountryDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            db = new CountryDbContext(options);
            repository = new CountryObjectsRepository(db);
        }
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            Assert.AreEqual(0, db.Countries.Count());
            for (var i = 0; i < count; i++) {
                db.Countries.Add(GetRandom.Object<CountryDbRecord>());
                db.SaveChanges();
            }
        }
        [TestCleanup] public override void TestCleanup() {
            base.TestCleanup();
            foreach (var c in db.Countries) {
                db.Remove(c);
                db.SaveChanges();
            }
            Assert.AreEqual(0, db.Countries.Count());
        }
    }
    
}
