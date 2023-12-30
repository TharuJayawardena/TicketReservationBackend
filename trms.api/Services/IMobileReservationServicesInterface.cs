/* Module: EAD
   Module Code: SE4040
   Student Name: Lanka P.A.C
   Student ID:IT20137014
  */

using trms.api.Common.Models;
using trms.api.Entities.trms.api.Common.Models;

namespace trms.api.Common.Services
{
    //interface class of mobile reservations
    public interface IMobileReservationServicesInterface
    {
        Task<MobileReservationEntity> CreateReservationAsync(MobileReservation reservation);

        Task UpdateBookingsByNICAsync(string NIC, MobileReservation reservation);

        Task DeleteBookingsByNICAsync(string NIC);


        //to get traveler's booking summary by using NIC
        Task<List<MobileReservationEntity>> GetTravelerBookingsAsync(string NIC);

        Task<List<MobileReservationEntity>> GetTravelerBookingswithdateAsync(string NIC, DateTime filterDate);



    }
}
