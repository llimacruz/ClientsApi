using ClientsApi.Models;
using System.Linq;
using System;
using ClientsApi.Business;

namespace ClientsApi.Persistence
{
    public class ClientsRepository : IClientsRepository
    {
        private ClientsApiDbContext _ctx;
        public ClientsRepository(ClientsApiDbContext ctx)
        {
            _ctx = ctx;
        }
                

        public IQueryable<Client> GetClients()
        {
            return _ctx.Clients;
        }


        public Client GetByCpf(long cpf)
        {
            var client = _ctx.Clients.FirstOrDefault(c => c.CPF == cpf);
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new InvalidOperationException("There's no client with the passed CPF.");
            }
        }
        

        public void Update(Client client)
        {
            try
            {
                if (!ClientValidator.IsValid(client))
                    throw new InvalidOperationException();

                var existing = _ctx.Clients.FirstOrDefault(c => c.CPF == client.CPF);

                if (existing == null)
                {
                    _ctx.Clients.Add(client);
                }
                else
                {
                    existing.Name = client.Name;
                    existing.Email = client.Email;
                    existing.MaritalStatus = client.MaritalStatus;
                    existing.PhoneNumbers = client.PhoneNumbers;
                    existing.Street = client.Street;
                    existing.City = client.City;
                    existing.State = client.State;
                    existing.Country = client.Country;
                    existing.Zip = client.Zip;
                }

                _ctx.SaveChanges();

            }
            catch(InvalidOperationException)
            {
                throw new InvalidOperationException("Invalid client");
            }
            catch (Exception)
            {
                throw new ApplicationException("The data couldn't be saved.");
            }
        }


        public void Delete(long cpf)
        {
            try
            {
                var client = _ctx.Clients.FirstOrDefault(c => c.CPF == cpf);
                if (client != null)
                {
                    _ctx.Clients.Remove(client);
                    _ctx.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("There's no client with the passed CPF.");
            }
            catch
            {
                throw new ApplicationException("It was not possible delete the client.");
            }
        }

    }
}