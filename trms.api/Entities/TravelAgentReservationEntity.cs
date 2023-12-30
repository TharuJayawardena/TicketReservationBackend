/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace trms.api.Common.Models
{
    //entity of travel agent reservation
    public class TravelAgentReservationEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
}
