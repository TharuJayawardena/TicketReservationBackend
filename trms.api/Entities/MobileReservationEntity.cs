/* Module: EAD
   Module Code: SE4040
   Student Name: Lanka P.A.C
   Student ID:IT20137014
  */
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace trms.api.Entities
{
    namespace trms.api.Common.Models
    {
        //entity class - for mobile reservation
        public class MobileReservationEntity
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
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

}
