/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using Mapster;
using Microsoft.AspNetCore.Mvc;
using trms.api.Data;
using trms.api.Entities;

namespace trms.api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    //create login api
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var result = await _userService.Login(loginDto);
            return Ok(result.Adapt<UserDto>());
        }
        catch (Exception e)
        {
            return BadRequest(new {e.Message});
        }
    }

    //create user api
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        try
        {
            await _userService.Create(user);
            return Ok(user.Adapt<UserDto>());
        }
        catch (Exception e)
        {
            return BadRequest(new {e.Message});
        }
    }
    //create update api
    [HttpPut("{NIC}")]
    public async Task<IActionResult> Update(string NIC, [FromBody] UserDto userDto)
    {

        try
        {
            // check if userDto is null or not before proceeding.
            if (userDto == null)
            {
                return BadRequest("Invalid request data. UserDto is null.");
            }

            var updatedUser = await _userService.Update(NIC, userDto);

            if (updatedUser != null)
            {
                return Ok(updatedUser.Adapt<UserDto>());
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

    //create delete api
    [HttpDelete("{NIC}")]
    public async Task<IActionResult> Delete(string NIC)
    {
        try
        {
            await _userService.Delete(NIC);
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
        var users = await _userService.GetAll();
        return Ok(users.Adapt<List<UserDto>>());
    }
    //create traveler api
   /* [HttpGet("traveler")]
    public async Task<IActionResult> GetTravelers()
    {
        var users = await _userService.GetTravelers();
        return Ok(users.Adapt<List<UserDto>>());
    }*/
    //create getByNIC api to get unic user
    [HttpGet("{NIC}")]
    public async Task<IActionResult> GetByNIC(string NIC)
    {
        try
        {
            /* await _userService.GetByNIC(NIC);
             return Ok();*/

            var user = await _userService.GetByNIC(NIC);
            return Ok(user.Adapt<List<UserDto>>());
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}