using ClientsApi.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientsApi.Tests
{
    [TestClass]
    public class CPFValidatorTest
    {
        [TestMethod]
        public void CPFValidator_Test_Valid()
        {
            Assert.IsTrue(CPFValidator.IsValid(11144477735));
            Assert.IsTrue(CPFValidator.IsValid(4832841220));
        }

        [TestMethod]
        public void CPFValidator_Test_Invalid()
        {
            Assert.IsFalse(CPFValidator.IsValid(1114447778836));
            Assert.IsFalse(CPFValidator.IsValid(11144477736));
            Assert.IsFalse(CPFValidator.IsValid(48407362585));
            Assert.IsFalse(CPFValidator.IsValid(11469966662));
        }
    }
}
