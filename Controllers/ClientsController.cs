using Microsoft.AspNetCore.Mvc;
using APBD12.Services;

namespace APBD12.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpDelete("{idClient}")]
        public async Task<ActionResult> DeleteClient(int idClient)
        {
            var result = await _clientService.DeleteClientAsync(idClient);

            if (result == "Client successfully deleted.")
            {
                return Ok(new { message = result });
            }

            if (result == "Client not found.")
            {
                return NotFound(new { message = result });
            }

            return BadRequest(new { message = result });
        }
    }
}