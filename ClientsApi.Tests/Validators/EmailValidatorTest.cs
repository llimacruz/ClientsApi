using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientsApi.Business;

namespace ClientsApi.Tests
{
    [TestClass]
    public class EmailValidatorTest
    {
        [TestMethod]
        public void EmailValidator_Test_Valid()
        {
            Assert.IsTrue(EmailValidator.IsValid("leo@leo"));
            Assert.IsTrue(EmailValidator.IsValid("131@3131"));
            Assert.IsTrue(EmailValidator.IsValid("llimacruz@gmail.com"));
            Assert.IsTrue(EmailValidator.IsValid("djfakçsjfdçaskjf@jfdçsjf.çsafljf"));
        }

        [TestMethod]
        public void EmailValidator_Test_Invalid()
        {
            Assert.IsFalse(EmailValidator.IsValid(""));
            Assert.IsFalse(EmailValidator.IsValid("leo@"));
            Assert.IsFalse(EmailValidator.IsValid("@3131"));
            Assert.IsFalse(EmailValidator.IsValid("jfdçsjf.çsafljf"));
        }
    }
}
