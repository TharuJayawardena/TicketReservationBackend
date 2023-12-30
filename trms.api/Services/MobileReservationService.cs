/* Module: EAD
   Module Code: SE4040
   Student Name: Lanka P.A.C
   Student ID:IT20137014
  */
using MongoDB.Driver;
using trms.api.Common.Models;
using trms.api.Data;
using trms.api.Entities;
using trms.api.Entities.trms.api.Common.Models;
using System;


namespace trms.api.Common.Services
{
    
        //implementation of the reservation service in the mobile application
        public class MobileReservationService : IMobileReservationServicesInterface
        {
            private readonly IMongoCollection<MobileReservationEntity> _reservationCollection;
            private readonly IMongoCollection<TrainEntity> _trainCollection;

            public MobileReservationService(MongoContext dbContext)
            {
                _reservationCollection = dbContext.GetCollection<MobileReservationEntity>("mobileReservations");
                _trainCollection = dbContext.GetCollection<TrainEntity>("Trains");
            }

            //creating reservation
            public async Task<MobileReservationEntity> CreateReservationAsync(MobileReservation reservation)
            {


                // Check reference ID has exceeded the maximum limit of four reservations)
                var existingReservations = await _reservationCollection
                    .Find(r => r.NIC == reservation.NIC)
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


                // Create and insert the reservation
                var reservationEntity = new MobileReservationEntity
                {
                    NIC = reservation.NIC,
                    TrainName = reservation.TrainName,
                    TrainSelection = reservation.TrainSelection,
                    ReservationDate = reservation.ReservationDate,
                    NumberOfSeats = reservation.NumberOfSeats
                };

                await _reservationCollection.InsertOneAsync(reservationEntity);
                return reservationEntity;
            }


        //get traveler's booking history by NIC
        public async Task<List<MobileReservationEntity>> GetTravelerBookingsAsync(string NIC)
        {
            try
            {
                // Retrieve all reservations for the given NIC
                var travelerBookings = await _reservationCollection
                    .Find(r => r.NIC == NIC)
                    .ToListAsync();

                return travelerBookings;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<MobileReservationEntity>> GetTravelerBookingswithdateAsync(string NIC, DateTime filterDate)
        {
            try
            {
                // Retrieve bookings for the given NIC and matching the provided date
                var travelerBookings = await _reservationCollection
                    .Find(b => b.NIC == NIC && b.ReservationDate.Date == filterDate.Date)
                    .ToListAsync();

                return travelerBookings;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // Delete a booking by NIC
        public async Task DeleteBookingsByNICAsync(string NIC)
        {
            try
            {
                // Find and delete all reservations for the specified NIC
                await _reservationCollection.DeleteManyAsync(r => r.NIC == NIC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateBookingsByNICAsync(string NIC, MobileReservation reservation)
        {
            try
            {
                // Find and update all reservations for the specified NIC
                var filter = Builders<MobileReservationEntity>.Filter.Eq(r => r.NIC, NIC);

                // Create an update definition to set the new values
                var update = Builders<MobileReservationEntity>.Update
                    .Set(r => r.TrainName, reservation.TrainName)
                    .Set(r => r.TrainSelection, reservation.TrainSelection)
                    .Set(r => r.ReservationDate, reservation.ReservationDate)
                    .Set(r => r.NumberOfSeats, reservation.NumberOfSeats);

                // Update all matching reservations
                await _reservationCollection.UpdateManyAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }

}

