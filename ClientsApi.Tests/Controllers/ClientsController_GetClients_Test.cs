using ClientsApi.Controllers;
using ClientsApi.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;

namespace ClientsApi.Tests
{
    [TestClass]
    public class ClientsController_GetClients_Test
    {
        [TestMethod]
        public void GetClients_RepositoryMethodExecuted()
        {
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();
            HttpRequestMessage reqFake = new HttpRequestMessage();

            ClientsController api = new ClientsController(repo.Object);
            api.Request = reqFake;

            api.GetClients();

            repo.Verify(x => x.GetClients());
        }

        [TestMethod]
        public void GetClientsByCpf_RepositoryMethodExecuted()
        {
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();

            HttpRequestMessage reqFake = new HttpRequestMessage();

            ClientsController api = new ClientsController(repo.Object);
            api.Request = reqFake;

            api.GetByCpf(1);

            repo.Verify(x => x.GetByCpf(1));
        }


        //private IQueryable<Client> GetListOfClients()
        //{
        //    var clients = new List<Client>();

        //    clients.Add(new Client
        //    {
        //        CPF = 12345678901,
        //        Name = "Leonardo de Lima Cruz",
        //        Email = "llimacruz@gmail.com",
        //        MaritalStatus = MaritalStatus.Married,
        //        PhoneNumbers = "31998980803|3121225208",
        //        Street = "Alameda dos Flamingos, 132",
        //        City = "Contagem",
        //        State = "MG",
        //        Country = "Brazil",
        //        Zip = "32146036"
        //    });

        //    clients.Add(new Client
        //    {
        //        CPF = 23456789012,
        //        Name = "Segundo Leonardo de Lima Cruz",
        //        Email = "2llimacruz@gmail.com",
        //        MaritalStatus = MaritalStatus.Single,
        //        PhoneNumbers = "3121225208",
        //        Street = "Rua do Limoreiro, 132",
        //        City = "Santo André",
        //        State = "SP",
        //        Country = "USA",
        //        Zip = "11556633"
        //    });
        //    return clients.AsQueryable();
        //}
    }
}
