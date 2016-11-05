using ClientsApi.Business;
using ClientsApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ClientsApi.Tests
{
    [TestClass]
    public class ClientValidatorTest
    {
        [TestMethod]
        public void IsValid()
        {
            var client = GetClient();

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Invalid_CPF()
        {
            var client = GetClient();
            client.CPF = 12345678901;

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_Name()
        {
            var client = GetClient();
            client.Name = "";

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_Email()
        {
            var client = GetClient();
            client.Email = "";

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_PhoneNumbers()
        {
            var client = GetClient();
            client.PhoneNumbersList = new List<string>();

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_Street()
        {
            var client = GetClient();
            client.Street = "";

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_City()
        {
            var client = GetClient();
            client.City = "";

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_State()
        {
            var client = GetClient();
            client.State = "";

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_Country()
        {
            var client = GetClient();
            client.Country = "";

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Invalid_Zip()
        {
            var client = GetClient();
            client.Zip = "";

            bool isValid = ClientValidator.IsValid(client);

            Assert.IsFalse(isValid);
        }

        Client GetClient()
        {
            var client = new Client
            {
                CPF = 45837254188,
                Name = "Leonardo de Lima Cruz",
                Email = "llimacruz@gmail.com",
                MaritalStatus = MaritalStatus.Married,
                PhoneNumbersList = new List<string> { "31998980803", "3121225208" },
                Street = "Alameda dos Flamingos, 132",
                City = "Contagem",
                State = "MG",
                Country = "Brazil",
                Zip = "32146036"
            };
            return client;
        }
    }
}
