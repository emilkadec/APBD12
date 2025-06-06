using Microsoft.EntityFrameworkCore;
using APBD12.Data;
using APBD12.DTOs;
using APBD12.Models;

namespace APBD12.Services
{
    public class TripService : ITripService
    {
        private readonly TripDbContext _context;

        public TripService(TripDbContext context)
        {
            _context = context;
        }

        public async Task<TripsResponseDto> GetTripsAsync(int page, int pageSize)
        {
            var totalTrips = await _context.Trips.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalTrips / pageSize);

            var trips = await _context.Trips
                .Include(t => t.CountryTrips)
                    .ThenInclude(ct => ct.Country)
                .Include(t => t.ClientTrips)
                    .ThenInclude(ct => ct.Client)
                .OrderByDescending(t => t.DateFrom)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TripDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new CountryDto
                    {
                        Name = ct.Country.Name
                    }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientDto
                    {
                        FirstName = ct.Client.FirstName,
                        LastName = ct.Client.LastName
                    }).ToList()
                })
                .ToListAsync();

            return new TripsResponseDto
            {
                PageNum = page,
                PageSize = pageSize,
                AllPages = totalPages,
                Trips = trips
            };
        }

        public async Task<string> AssignClientToTripAsync(int idTrip, AssignClientDto assignClientDto)
        {
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.Pesel == assignClientDto.Pesel);

            if (existingClient != null)
            {
                return "A client with the given PESEL number already exists.";
            }

            var existingClientTrip = await _context.ClientTrips
                .AnyAsync(ct => ct.IdTrip == idTrip && 
                                _context.Clients.Any(c => c.IdClient == ct.IdClient && c.Pesel == assignClientDto.Pesel));

            if (existingClientTrip)
            {
                return "A client with the given PESEL number is already registered for this trip.";
            }

            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null)
            {
                return "Trip does not exist.";
            }

            if (trip.DateFrom <= DateTime.Now)
            {
                return "Cannot register for a trip that has already occurred or is starting today.";
            }

            var newClient = new Client
            {
                FirstName = assignClientDto.FirstName,
                LastName = assignClientDto.LastName,
                Email = assignClientDto.Email,
                Telephone = assignClientDto.Telephone,
                Pesel = assignClientDto.Pesel
            };

            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();

            var clientTrip = new ClientTrip
            {
                IdClient = newClient.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = assignClientDto.PaymentDate
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();

            return "Client successfully assigned to trip.";
        }
    }
}