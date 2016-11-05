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
    public class ClientsController_PostClient_Test
    {
        [TestMethod]
        public void PostClient_RepositoryMethodExecuted()
        {
            //arrange
            //mock of the repository
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();

            //creating the api
            ClientsController api = new ClientsController(repo.Object);

            //configuring api's request 
            api.Request = new HttpRequestMessage();
            //api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //fake client
            var client = new Client();


            //act
            api.PostClient(client);

            //assert
            repo.Verify(x => x.Update(client));
        }

        [TestMethod]
        public void PostClient_WithException_ReturnsBadRequest()
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

            var res = api.PostClient(client);

            //assert status code
            Assert.AreEqual(expectedRes.StatusCode, res.StatusCode);
        }

        [TestMethod]
        public void PostClient_WithoutException_ReturnsCreated()
        {
            var client = new Client();

            //fake request
            HttpRequestMessage reqFake = new HttpRequestMessage();
            reqFake.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var expectedRes = reqFake.CreateResponse(HttpStatusCode.Created, client);

            //mock repository
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();

            //api request
            ClientsController api = new ClientsController(repo.Object);
            api.Request = new HttpRequestMessage();
            api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var res = api.PostClient(client);

            //assert status code
            Assert.AreEqual(expectedRes.StatusCode, res.StatusCode);
        }

    }
}
