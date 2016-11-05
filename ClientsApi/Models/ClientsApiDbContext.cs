using System.Data.Entity;

namespace ClientsApi.Models
{
    public class ClientsApiDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
    }
}