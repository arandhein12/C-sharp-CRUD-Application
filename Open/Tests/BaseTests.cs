﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;

namespace Open.Tests {
    public class BaseTests {
        private const string notTested = "<{0} is not tested";
        private const string notSpecified = "Class is not specified";

        private List<string> members { get; set; }
        protected Type type;

        [TestInitialize]
        public virtual void TestInitialize()
        { }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            type = null;
        }

        [TestMethod]
        public virtual void IsTested()
        {
            if (type == null) Assert.Inconclusive(notSpecified);
            var m = GetClass.Members(type, PublicBindingFlagsFor.DeclaredMembers);
            members = m.Select(e => e.Name).ToList();
            removeTested();
            if (members.Count == 0) return;
            Assert.Fail(notTested, members[0]);
        }
         
        //Viimane lisatud test

      

        private void removeTested()
        {
            var tests = GetType().GetMembers().Select(e => e.Name).ToList();
            for (var i = members.Count; i > 0; i--) {
                var m = members[i - 1] + "Test";
                var isTested = tests.Find(o => o == m);
                if (string.IsNullOrEmpty(isTested)) continue;
                members.RemoveAt(i - 1);
            }
        }
    }
}

     