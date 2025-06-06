using Microsoft.EntityFrameworkCore;
using APBD12.Data;

namespace APBD12.Services
{
    public class ClientService : IClientService
    {
        private readonly TripDbContext _context;

        public ClientService(TripDbContext context)
        {
            _context = context;
        }

        public async Task<string> DeleteClientAsync(int idClient)
        {
            var client = await _context.Clients
                .Include(c => c.ClientTrips)
                .FirstOrDefaultAsync(c => c.IdClient == idClient);

            if (client == null)
            {
                return "Client not found.";
            }

            if (client.ClientTrips.Any())
            {
                return "Cannot delete client who has assigned trips.";
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return "Client successfully deleted.";
        }
    }
}