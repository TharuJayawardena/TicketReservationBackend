/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using MongoDB.Driver;
using trms.api.Entities;
using trms.api.Data;
using trms.api.Common.Models;

namespace trms.api.Common.Services
{
    public class TrainService
    {
        private readonly IMongoCollection<TrainEntity> _trainCollection;
        private readonly IMongoCollection<TravelAgentReservationEntity> _reservationCollection;

        public TrainService(MongoContext dbContext)
        {
            _trainCollection = dbContext.GetCollection<TrainEntity>("Trains");
            _reservationCollection = dbContext.GetCollection<TravelAgentReservationEntity>("travelAgentReservations");
        }

        // Get a train by ID
        public async Task<TrainEntity> GetTrainByIdAsync(string id)
        {
            var objectId = new string(id);
            var train = await _trainCollection.Find(t => t.Id == objectId).FirstOrDefaultAsync();

            if (train == null)
            {
                throw new Exception("Train not found.");
            }

            return train;
        }

        // Get all trains
        public async Task<List<TrainEntity>> GetAllTrainsAsync()
        {
            var trains = await _trainCollection.Find(_ => true).ToListAsync();
            return trains;
        }

        // Create a new train with schedules. Ensure it's active and published before allowing reservations.
        public async Task<TrainEntity> CreateTrainAsync(TrainEntity train)
        {
            // Check if the train is active and published
            if (!train.IsActive || !train.IsPublished)
            {
                throw new Exception("Train must be active and published to accept reservations.");
            }

            // Create and insert the train entity into the MongoDB collection
            await _trainCollection.InsertOneAsync(train);
            return train;
        }

       
        // Update train details, including name, number, schedules, and stops
        public async Task UpdateTrainDetailsAsync(string id, TrainEntity updatedTrain)
        {
            // Check if the train exists
            var objectId = new string(id);
            var existingTrain = await _trainCollection.Find(t => t.Id == objectId).FirstOrDefaultAsync();

            if (existingTrain == null)
            {
                throw new Exception("Train not found.");
            }

            // Update the train properties with the new values
            existingTrain.TrainNumber = updatedTrain.TrainNumber;
            existingTrain.TrainName = updatedTrain.TrainName;
            existingTrain.Schedules = updatedTrain.Schedules;
            existingTrain.to= updatedTrain.to;
            existingTrain.from = updatedTrain.from;
            existingTrain.Stops = updatedTrain.Stops;

            // Update the train in the MongoDB collection
            await _trainCollection.ReplaceOneAsync(t => t.Id == objectId, existingTrain);
        }


        // Cancel a train for reservations. Check for existing reservations before canceling.
        public async Task CancelTrainAsync(string id)
        {
            // Check if the train exists
            var objectId = new string(id);
            var existingTrain = await _trainCollection.Find(t => t.Id == objectId).FirstOrDefaultAsync();

            if (existingTrain == null)
            {
                throw new Exception("Train not found.");
            }

            // Check if there are existing reservations for this train
            bool hasReservations = await CheckForExistingReservations(existingTrain);

            if (hasReservations)
            {
                throw new Exception("Cannot cancel a train with existing reservations.");
            }
            else
            {
                // Update the train to set isActive and isPublished to false
                existingTrain.IsActive = false;
                existingTrain.IsPublished = false;

                // Update the train in the MongoDB collection
                await _trainCollection.ReplaceOneAsync(t => t.Id == objectId, existingTrain);
            }
        }


        //check for exsting reservations for selected train by user
        private async Task<bool> CheckForExistingReservations(TrainEntity train)
        {
            // Check if there are reservations for the specified train name
            var reservationsForTrain = await _reservationCollection
                .Find(r => r.TrainName == train.TrainName)
                .ToListAsync();

            if (reservationsForTrain.Count > 0)
            {
                // Calculate the total number of seats reserved for this train
                int totalReservedSeats = reservationsForTrain.Sum(r => r.NumberOfSeats);

                // Check if there are reserved seats for this train
                if (totalReservedSeats > 0)
                {
                    return true; // There are existing reservations with seats
                }
            }

            return false; // No existing reservations with seats
        }

    }
}
