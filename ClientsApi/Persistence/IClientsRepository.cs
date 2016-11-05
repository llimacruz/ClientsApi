using ClientsApi.Models;
using System.Linq;

namespace ClientsApi.Persistence
{
    public interface IClientsRepository
    {
        IQueryable<Client> GetClients();
        Client GetByCpf(long cpf);
        void Update(Client client);
        void Delete(long cpf);
    }
}
