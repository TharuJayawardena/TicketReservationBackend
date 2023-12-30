/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using System.ComponentModel.DataAnnotations;

//model class of travel agent reservation
//include only nessasary attributes which need to display
public class TravelAgentReservation
{
    public string Id { get; set; }

    [Required]
    public string TravelerNIC { get; set; }

    [Required]
    public string TrainName { get; set; }

    [Required]
    public string TrainSelection { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReservationDate { get; set; }

    [Required]
    public int NumberOfSeats { get; set; }

   
    public string TravelAgentId { get; set; }

   
    public string TravelAgentName { get; set; }
}
