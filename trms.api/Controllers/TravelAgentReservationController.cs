/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using Microsoft.AspNetCore.Mvc;
using trms.api.Common.Services;

[Route("api/travel-agent-reservations")]
[ApiController]
public class TravelAgentReservationController : ControllerBase
{
    private readonly ITravelAgentReservationService _reservationService;

    public TravelAgentReservationController(ITravelAgentReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    // GET api/travel-agent-reservations
    [HttpGet]
    public async Task<IActionResult> GetAllReservations()
    {
        try
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // GET api/travel-agent-reservations/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservationById(string id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            return Ok(reservation);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // POST api/travel-agent-reservations
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] TravelAgentReservation reservation)
    {
        try
        {
            var result = await _reservationService.CreateReservationAsync(reservation);
            return CreatedAtRoute(new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // PUT api/travel-agent-reservations/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(string id, [FromBody] TravelAgentReservation reservation)
    {
        try
        {
            await _reservationService.UpdateReservationAsync(id, reservation);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/travel-agent-reservations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelReservation(string id)
    {
        try
        {
            await _reservationService.CancelReservationAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
