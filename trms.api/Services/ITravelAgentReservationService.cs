/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using trms.api.Common.Models;


namespace trms.api.Common.Services 
{
    //interface of service implementation of reservation
    public interface ITravelAgentReservationService
    {
        Task<TravelAgentReservationEntity> CreateReservationAsync(TravelAgentReservation reservation);

        Task UpdateReservationAsync(string id, TravelAgentReservation reservation);

        Task CancelReservationAsync(string id);

        Task<List<TravelAgentReservationEntity>> GetAllReservationsAsync();

        Task<TravelAgentReservationEntity> GetReservationByIdAsync(string id);
    }
}
