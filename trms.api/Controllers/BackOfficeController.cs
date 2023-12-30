/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using Microsoft.AspNetCore.Mvc;
using trms.api.Entities;
using Mapster;
using trms.api.Common.Models;
using trms.api.Services;
using trms.api.Data;

namespace trms.api.Controllers
{
    [ApiController]
    [Route("api/backOffice")]
    public class BackOfficeController : ControllerBase
    {
        private readonly BackOfficeService _backOfficeService;
       

        public BackOfficeController(BackOfficeService backOfficeService)
        {
            _backOfficeService = backOfficeService;
        }



        //create BackOffice api
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BackOffice backOffice)
        {
            try
            {
                await _backOfficeService.Create(backOffice);
                return Ok(backOffice.Adapt<BackOfficeDto>());
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetByUserId(string UserId)
        {
            try
            {
                /* await _userService.GetByNIC(NIC);
                 return Ok();*/

                var backOffice = await _backOfficeService.GetByUserId(UserId);
                return Ok(backOffice.Adapt<List<BackOfficeDto>>());
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }


        //create delete api
        [HttpDelete("{UserId}")]
        public async Task<IActionResult> Delete(string UserId)
        {
            try
            {
                await _backOfficeService.Delete(UserId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        //create getALl api 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var backOfficers = await _backOfficeService.GetAll();
            return Ok(backOfficers.Adapt<List<BackOfficeDto>>());
        }

        //create update api
        [HttpPut("{UserId}")]
        public async Task<IActionResult> Update(string UserId, [FromBody] BackOfficeDto backOfficeDto)
        {

            try
            {
                // check if userDto is null or not before proceeding.
                if (backOfficeDto == null)
                {
                    return BadRequest("Invalid request data.");
                }

                var updatedBackOffice = await _backOfficeService.Update(UserId, backOfficeDto);

                if (updatedBackOffice != null)
                {
                    return Ok(updatedBackOffice.Adapt<BackOfficeDto>());
                }
                else
                {
                    return NotFound(); // User with the given NIC not found
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }


}
