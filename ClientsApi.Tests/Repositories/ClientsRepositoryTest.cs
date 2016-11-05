using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientsApi.Persistence;
using Moq;
using ClientsApi.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ClientsApi.Tests
{
    [TestClass]
    public class ClientsRepositoryTest
    {
        [TestMethod]
        public void GetAllClients()
        {
            var clientsInMemory = new List<Client> { GetClient(), GetClient() };
            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            var allClients = repo.GetClients().ToList();

            Assert.AreEqual(clientsInMemory.Count, allClients.Count);
        }

        [TestMethod]
        public void GetByCpf_Existing()
        {
            var clientsInMemory = new List<Client> { GetClient(), GetClient() };

            int searchCpf = 123;
            clientsInMemory[0].CPF = searchCpf;

            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            var client = repo.GetByCpf(searchCpf);

            Assert.AreEqual(clientsInMemory[0], client);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetByCpf_Inexisting()
        {
            var clientsInMemory = new List<Client> { GetClient() };
            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            var client = repo.GetByCpf(1234); //wrong cpf
        }


        [TestMethod]
        public void Update_NewClient_Valid()
        {
            var clientsInMemory = new List<Client>();
            var client = GetClient();

            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            //mocking add method
            mockDbSet.Setup(m => m.Add(It.IsAny<Client>()))
                .Callback<Client>(clientsInMemory.Add);

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            repo.Update(client);

            Assert.IsTrue(clientsInMemory.Contains(client));
        }

        [TestMethod]
        public void Update_ExistingClient_Valid()
        {
            var clientsInMemory = new List<Client> { GetClient() };

            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            //mocking add method
            mockDbSet.Setup(m => m.Add(It.IsAny<Client>()))
                .Callback<Client>(clientsInMemory.Add);

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            var client = GetClient();
            client.City = "Belo Horizonte";
            repo.Update(client);

            Assert.IsTrue(clientsInMemory.Count == 1);
            Assert.AreEqual("Belo Horizonte", clientsInMemory[0].City);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Update_NewClient_Invalid()
        {
            var clientsInMemory = new List<Client>();
            var client = GetClient();
            client.CPF = 123;

            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            //mocking add method
            mockDbSet.Setup(m => m.Add(It.IsAny<Client>()))
                .Callback<Client>(clientsInMemory.Add);

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            repo.Update(client);
        }

        [TestMethod]
        public void Delete_ExistingClient()
        {
            var client = GetClient();
            var clientsInMemory = new List<Client> { client };

            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            //mocking add method
            mockDbSet.Setup(m => m.Remove(It.IsAny<Client>()))
                .Returns(() => client)
                .Callback(() => clientsInMemory.Remove(client));

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            repo.Delete(client.CPF);

            Assert.IsTrue(clientsInMemory.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_InexistingClient()
        {
            var client = GetClient();
            var clientsInMemory = new List<Client> { client };

            var mockDbSet = new Mock<DbSet<Client>>();
            PrepareQueryableMock(clientsInMemory, mockDbSet);

            //mocking remove method
            mockDbSet.Setup(m => m.Remove(It.IsAny<Client>()))
                .Returns(() => client)
                .Callback(() => clientsInMemory.Remove(client));

            var dbContext = new ClientsApiDbContext { Clients = mockDbSet.Object };
            var repo = new ClientsRepository(dbContext);

            repo.Delete(123);
        }


        void PrepareQueryableMock(List<Client> clientsInMemory, Mock<DbSet<Client>> mockDbSet)
        {
            //mock of FirstOrDefault (Extension method)
            var queryableData = clientsInMemory.AsQueryable();
            mockDbSet.As<IQueryable<Client>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockDbSet.As<IQueryable<Client>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockDbSet.As<IQueryable<Client>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockDbSet.As<IQueryable<Client>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
        }

        Client GetClient()
        {
            var client = new Client
            {
                Id = 1,
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
