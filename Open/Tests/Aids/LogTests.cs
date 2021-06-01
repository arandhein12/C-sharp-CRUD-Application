using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;

namespace Open.Tests.Aids {

    [TestClass]
    public class LogTests : BaseTests {
        internal class logTests : ILogBook {
            public string LoggedMessage { get; private set; }
            public Exception LoggedException { get; private set; }
            public List<Exception> LoggedExceptions { get; } = new List<Exception>();

            public void WriteEntry(string message) {
                LoggedMessage = message;
            }

            public void WriteEntry(Exception e) {
                LoggedException = e;
                LoggedExceptions.Add (e);
            }
        }

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            type = typeof(Log);
        }

        [TestMethod]
        public void MessageTest() {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ExceptionTest() {
            Assert.Inconclusive();

        }
    }
}


