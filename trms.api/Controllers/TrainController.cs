/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using Microsoft.AspNetCore.Mvc;
using trms.api.Common.Models;
using trms.api.Common.Services;
using trms.api.Entities;

[Route("api/trains")]
[ApiController]
public class TrainController : ControllerBase
{
    private readonly TrainService _trainService;

    public TrainController(TrainService trainService)
    {
        _trainService = trainService;
    }



    // GET api/trains/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrainById(string id)
    {
        try
        {
            var train = await _trainService.GetTrainByIdAsync(id);
            return Ok(train);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/trains
    [HttpGet]
    public async Task<IActionResult> GetAllTrains()
    {
        try
        {
            var trains = await _trainService.GetAllTrainsAsync();
            return Ok(trains);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // POST api/trains
    [HttpPost]
    public async Task<IActionResult> CreateTrain([FromBody] Train train)
    {
        try
        {
            // Convert the Train object to a TrainEntity object
            var trainEntity = new TrainEntity
            {
                TrainNumber = train.TrainNumber,
                TrainName = train.TrainName,
                to = train.to,
                from = train.from,
                IsActive = train.IsActive,
                IsPublished = train.IsPublished,
                TrainSections = train.TrainSections,
                Schedules = train.Schedules,
                Stops = train.Stops,
                // Map other properties from Train to TrainEntity as needed
            };

            // Implement logic to create a new train with schedules here.
            // Ensure the train is active and published before allowing reservations.
            var createdTrain = await _trainService.CreateTrainAsync(trainEntity);
            return CreatedAtRoute(new { id = createdTrain.Id }, createdTrain);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT api/trains/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrain(string id, [FromBody] Train train)
    {
        try
        {
            // Convert the Train object to a TrainEntity object
            var updatedTrain = new TrainEntity
            {
                TrainNumber = train.TrainNumber,
                TrainName = train.TrainName,
                Schedules = train.Schedules,
                to=train.to,
                from=train.from,
                Stops = train.Stops
            };

            // Implement logic to update existing train details here.
            await _trainService.UpdateTrainDetailsAsync(id, updatedTrain);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/trains/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelTrain(string id)
    {
        try
        {
            // Implement logic to cancel a train and check for existing reservations.
            await _trainService.CancelTrainAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
