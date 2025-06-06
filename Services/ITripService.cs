using APBD12.DTOs;

namespace APBD12.Services
{
    public interface ITripService
    {
        Task<TripsResponseDto> GetTripsAsync(int page, int pageSize);
        Task<string> AssignClientToTripAsync(int idTrip, AssignClientDto assignClientDto);
    }
}