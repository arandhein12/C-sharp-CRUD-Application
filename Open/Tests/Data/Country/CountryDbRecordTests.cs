﻿using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Country;
using Open.Tests.Aids;
namespace Open.Tests.Data.Country {

    [TestClass] public class CountryDbRecordTests : ObjectTests<CountryDbRecord> {

        [TestMethod] public void CanCreateTest() {
            Assert.IsNotNull(obj);
        }
        protected override CountryDbRecord getRandomTestObject() {
           return GetRandom.Object<CountryDbRecord>();
        }
        [TestMethod] public void IDTest() {
            testReadWriteProperty(() => obj.ID, x => obj.ID = x);
        }
        [TestMethod] public void NameTest()
        {
            testReadWriteProperty(() => obj.Name, x => obj.Name = x);
        }
        [TestMethod] public void CodeTest()
        {
            testReadWriteProperty(() => obj.Code, x => obj.Code = x);
        }
        [TestMethod] public void ValidFromTest() {
            DateTime rnd() => GetRandom.DateTime(null, obj.ValidTo.AddYears(-1));
            testReadWriteProperty(() => obj.ValidFrom, x => obj.ValidFrom = x, rnd);
        }
        [TestMethod] public void ValidToTest(){
            DateTime rnd() => GetRandom.DateTime(obj.ValidFrom.AddYears(-1));
            testReadWriteProperty(() => obj.ValidTo, x => obj.ValidTo = x, rnd);
        }
    }
}











