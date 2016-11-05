using ClientsApi.Models;
using ClientsApi.Persistence;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ClientsApi.Controllers
{
    [RoutePrefix("api/clients")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientsController : ApiController
    {
        private IClientsRepository _repo;
        public ClientsController(IClientsRepository repo)
        {
            _repo = repo;
        }


        public HttpResponseMessage GetClients()
        {
            try
            {
                var clients = _repo.GetClients().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, clients);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        [Route("{cpf:long}")]
        public HttpResponseMessage GetByCpf(long cpf)
        {
            try
            {
                var client = _repo.GetByCpf(cpf);
                return Request.CreateResponse(HttpStatusCode.OK, client);
            }
            catch (InvalidOperationException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        public HttpResponseMessage PostClient(Client client)
        {
            try
            {
                _repo.Update(client);
                return Request.CreateResponse(HttpStatusCode.Created, client);
            }
            catch(InvalidOperationException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        public HttpResponseMessage PutClient(Client client)
        {
            try
            {
                _repo.Update(client);
                return Request.CreateResponse(HttpStatusCode.OK, client);
            }
            catch (InvalidOperationException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        [Route("{cpf:long}")]
        public HttpResponseMessage DeleteClient(long cpf)
        {
            try
            {
                _repo.Delete(cpf);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (InvalidOperationException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}