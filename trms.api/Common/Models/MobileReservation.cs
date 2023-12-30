/* Module: EAD
   Module Code: SE4040
   Student Name: LankaP.A.C.
   Student ID:IT20137014
  */
using System.ComponentModel.DataAnnotations;
namespace trms.api.Common.Models {

    //model class of mobile reservations by traveler 
    public class MobileReservation
    {
        //attributes which are displayed 
        public string Id { get; set; }

        [Required]
        public string NIC { get; set; }

        [Required]
        public string TrainName { get; set; }

        [Required]
        public string TrainSelection { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }


    }

}