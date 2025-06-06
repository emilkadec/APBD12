using Microsoft.AspNetCore.Mvc;
using APBD12.DTOs;
using APBD12.Services;

namespace APBD12.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<ActionResult<TripsResponseDto>> GetTrips(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var result = await _tripService.GetTripsAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<ActionResult> AssignClientToTrip(
            int idTrip, 
            [FromBody] AssignClientDto assignClientDto)
        {
            var result = await _tripService.AssignClientToTripAsync(idTrip, assignClientDto);

            if (result == "Client successfully assigned to trip.")
            {
                return Ok(new { message = result });
            }

            return BadRequest(new { message = result });
        }
    }
}