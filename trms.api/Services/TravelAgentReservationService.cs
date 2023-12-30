/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using MongoDB.Driver;
using trms.api.Common.Models;
using trms.api.Data;
using trms.api.Entities;


namespace trms.api.Common.Services
{
    //implementation of the reservation service
    public class TravelAgentReservationService : ITravelAgentReservationService
    {
        private readonly IMongoCollection<TravelAgentReservationEntity> _reservationCollection;
        private readonly IMongoCollection<TrainEntity> _trainCollection;

        public TravelAgentReservationService(MongoContext dbContext)
        {
            _reservationCollection = dbContext.GetCollection<TravelAgentReservationEntity>("travelAgentReservations");
            _trainCollection = dbContext.GetCollection<TrainEntity>("Trains");
        }

        //creating a new reservation by travel agent
        public async Task<TravelAgentReservationEntity> CreateReservationAsync(TravelAgentReservation reservation)
        {
            

            // Check if the reference ID has already exceeded the maximum limit (4 reservations)
            var existingReservations = await _reservationCollection
                .Find(r => r.TravelerNIC == reservation.TravelerNIC)
                .ToListAsync();

            if (existingReservations.Count >= 4)
            {
                throw new Exception("Maximum 4 reservations allowed per reference ID.");
            }

            // Check if the train is active
            var train = await _trainCollection.Find(t => t.TrainName == reservation.TrainName && t.IsActive && t.IsPublished).FirstOrDefaultAsync();
            if (train == null)
            {
                throw new Exception("The selected train is not active or published(or both).");
            }


            // Create and insert the reservation entity into the MongoDB collection
            var reservationEntity = new TravelAgentReservationEntity
            {
                TravelerNIC = reservation.TravelerNIC,
                TrainName = reservation.TrainName,
                TrainSelection = reservation.TrainSelection,
                ReservationDate = reservation.ReservationDate,
                NumberOfSeats = reservation.NumberOfSeats,
                TravelAgentId = reservation.TravelAgentId,
                TravelAgentName = reservation.TravelAgentName
            };

            await _reservationCollection.InsertOneAsync(reservationEntity);
            return reservationEntity;
        }


        //update the reservations
        public async Task UpdateReservationAsync(string id, TravelAgentReservation reservation)
        {
            // Check if the reservation exists
            var existingReservation = await _reservationCollection.Find(r => r.Id == new string(id)).FirstOrDefaultAsync();

            if (existingReservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            // Check if the train is active
            var train = await _trainCollection.Find(t => t.TrainName == reservation.TrainName && t.IsActive).FirstOrDefaultAsync();
            if (train == null)
            {
                throw new Exception("The selected train is not active.");
            }

            // Update the reservation entity with the new values
            existingReservation.TrainName = reservation.TrainName;
            existingReservation.TrainSelection = reservation.TrainSelection;
            existingReservation.ReservationDate = reservation.ReservationDate;
            existingReservation.NumberOfSeats = reservation.NumberOfSeats;
            existingReservation.TravelAgentId = reservation.TravelAgentId;
            existingReservation.TravelAgentName = reservation.TravelAgentName;

            // Update the reservation in the MongoDB collection
            await _reservationCollection.ReplaceOneAsync(r => r.Id == new string(id), existingReservation);
        }


        //cancel reservations
        public async Task CancelReservationAsync(string id)
        {
            // Check if the reservation exists
            var existingReservation = await _reservationCollection.Find(r => r.Id == new string(id)).FirstOrDefaultAsync();

            if (existingReservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            // Delete the reservation from the MongoDB collection
            await _reservationCollection.DeleteOneAsync(r => r.Id == new string(id));
        }


        //get a list of reservations
        public async Task<List<TravelAgentReservationEntity>> GetAllReservationsAsync()
        {
            // Retrieve all reservations from the MongoDB collection
            return await _reservationCollection.Find(_ => true).ToListAsync();
        }

        //get a reservation by id
        public async Task<TravelAgentReservationEntity> GetReservationByIdAsync(string id)
        {
            // Check if the reservation exists
            var objectId = new string(id);
            var existingReservation = await _reservationCollection.Find(r => r.Id == objectId).FirstOrDefaultAsync();

            if (existingReservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            return existingReservation;
        }
    }
}
