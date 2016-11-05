using ClientsApi.Controllers;
using ClientsApi.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ClientsApi.Tests
{
    [TestClass]
    public class ClientsController_DeleteClient_Test
    {
        [TestMethod]
        public void DeleteClient_RepositoryMethodExecuted()
        {
            //arrange
            //mock of the repository
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();

            //creating the api
            ClientsController api = new ClientsController(repo.Object);
            api.Request = new HttpRequestMessage();

            //act
            api.DeleteClient(1);

            //assert
            repo.Verify(x => x.Delete(1));
        }

        [TestMethod]
        public void DeleteClients_WithException_ReturnsNotFound()
        {
            //var client = new Client();

            //fake request
            HttpRequestMessage reqFake = new HttpRequestMessage();
            reqFake.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var expectedRes = reqFake.CreateResponse(HttpStatusCode.NotFound, 1);

            //mock repository
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();
            repo.Setup(m => m.Delete(It.IsAny<long>())).Throws(new InvalidOperationException());

            //api request
            ClientsController api = new ClientsController(repo.Object);
            api.Request = new HttpRequestMessage();
            api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var res = api.DeleteClient(1);

            //assert status code
            Assert.AreEqual(expectedRes.StatusCode, res.StatusCode);
        }

        [TestMethod]
        public void DeleteClient_WithoutException_ReturnsOk()
        {
            //fake request
            HttpRequestMessage reqFake = new HttpRequestMessage();
            reqFake.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var expectedRes = reqFake.CreateResponse(HttpStatusCode.OK, 1);

            //mock repository
            Mock<IClientsRepository> repo = new Mock<IClientsRepository>();

            //api request
            ClientsController api = new ClientsController(repo.Object);
            api.Request = new HttpRequestMessage();
            api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var res = api.DeleteClient(1);

            //assert status code
            Assert.AreEqual(expectedRes.StatusCode, res.StatusCode);
        }

    }
}
