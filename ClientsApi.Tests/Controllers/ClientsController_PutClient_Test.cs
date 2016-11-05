using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientsApi.Persistence;
using Moq;
using ClientsApi.Controllers;
using ClientsApi.Models;
using System.Net.Http;
using System.Net;
using System.Web.Http.Hosting;
using System.Web.Http;

namespace ClientsApi.Tests
{
    [TestClass]
    public class ClientsController_PutClient_Test
    {
        [TestMethod]
        public void PutClient_RepositoryMethodExecuted()
        {
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();

            ClientsController api = new ClientsController(repo.Object);

            //configuring api's request 
            api.Request = new HttpRequestMessage();
            //api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var client = new Client();

            api.PutClient(client);

            repo.Verify(x => x.Update(client));
        }

        [TestMethod]
        public void PutClient_WithException_ReturnsNotFound()
        {
            var client = new Client();

            //fake request
            HttpRequestMessage reqFake = new HttpRequestMessage();
            reqFake.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var expectedRes = reqFake.CreateResponse(HttpStatusCode.BadRequest, client);

            //mock repository
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();
            repo.Setup(m => m.Update(It.IsAny<Client>())).Throws(new InvalidOperationException());

            //api request
            ClientsController api = new ClientsController(repo.Object);
            api.Request = new HttpRequestMessage();
            api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var res = api.PutClient(client);

            //assert status code
            Assert.AreEqual(expectedRes.StatusCode, res.StatusCode);
        }

        [TestMethod]
        public void PutClient_WithoutException_ReturnsOk()
        {
            var client = new Client();

            //fake request
            HttpRequestMessage reqFake = new HttpRequestMessage();
            reqFake.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var expectedRes = reqFake.CreateResponse(HttpStatusCode.OK, client);

            //mock repository
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();

            //api request
            ClientsController api = new ClientsController(repo.Object);
            api.Request = new HttpRequestMessage();
            api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var res = api.PutClient(client);

            //assert status code
            Assert.AreEqual(expectedRes.StatusCode, res.StatusCode);
        }

        //Client GetClient()
        //{
        //    return new Client
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
        //    };
        //}
    }
}
