/* Module: EAD
   Module Code: SE4040
   Student Name: Lanka P.A.C
   Student ID:IT20137014
  */
using Microsoft.AspNetCore.Mvc;
using trms.api.Common.Models;
using trms.api.Common.Services;
using trms.api.Entities.trms.api.Common.Models;
using trms.api.Services;

namespace trms.api.Controllers
{
    //after creating reservations, traveler can view the reservation summary, modify summary details and cancel or remove them

    //route path
    [Route("api/mobile-reservations")]

    [ApiController]
    public class MobileReservationController : ControllerBase
    {
        private readonly IMobileReservationServicesInterface _reservationService;

        public MobileReservationController(IMobileReservationServicesInterface reservationService)
        {
            _reservationService = reservationService;
        }

        //to add reservations through mobile
        // POST api/mobile-reservations
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] MobileReservation reservation)
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


        //to get traveler's booking summary by NIC
        // GET api/mobile-reservations/{NIC}/bookings
        [HttpGet("{NIC}/bookings")]
        public async Task<IActionResult> GetTravelerBookings(string NIC)
        {
            try
            {
                var travelerBookings = await _reservationService.GetTravelerBookingsAsync(NIC);
                return Ok(travelerBookings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //to get traveler's booking summary by NIC and datetime
        [HttpGet("{NIC}/bookings_by_date")]
        public async Task<IActionResult> GetTravelerBookings(string NIC, [FromQuery] DateTime? filterDate = null)
        {
            try
            {
                List<MobileReservationEntity> travelerBookings;

                if (filterDate.HasValue)
                {
                    // Filter by date if a date parameter is provided
                    travelerBookings = await _reservationService.GetTravelerBookingswithdateAsync(NIC, filterDate.Value);
                }
                else
                {
                    // If no date parameter provided, retrieve all bookings
                    travelerBookings = await _reservationService.GetTravelerBookingsAsync(NIC);
                }

                if (travelerBookings.Count == 0)
                {
                    return NotFound("This data has no existing bookings or reservations.");
                }

                return Ok(travelerBookings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //to delete or cancel reservation summary
        // DELETE api/mobile-reservations/{NIC}/bookings
        [HttpDelete("{NIC}/bookings")]
        public async Task<IActionResult> DeleteBookingsByNIC(string NIC)
        {
            try
            {
                // Call the service method to delete all bookings for the specified NIC
                await _reservationService.DeleteBookingsByNICAsync(NIC);

                return NoContent(); // Return a successful response
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //to update booking summary
        // PUT api/mobile-reservations/{NIC}/bookings
        [HttpPut("{NIC}/bookings")]
        public async Task<IActionResult> UpdateBookingsByNIC(string NIC, [FromBody] MobileReservation reservation)
        {
            try
            {
                // Call the service method to update bookings for the specified NIC
                await _reservationService.UpdateBookingsByNICAsync(NIC, reservation);

                return NoContent(); // Return a successful response
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}