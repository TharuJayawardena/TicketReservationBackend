/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/


using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using trms.api.Common;
using MongoDB.Bson;

namespace trms.api.Entities
{
    public class BackOffice : BaseEntity

    {
        //create UserID as the primary key
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "The NIC is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "The UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Role is required")]
        public string Role { get; set; }

        //create validations
        public void Validate()
        {
            var validRoles = new List<string> {"BackOffice", "TravelAgent"};
            if (string.IsNullOrEmpty(UserId))
                throw new AggregateException("UserId name is required");
            if (string.IsNullOrEmpty(UserName))
                throw new AggregateException("UserName is required");
            if (string.IsNullOrEmpty(Email))
                throw new AggregateException("Email is required");

            if (string.IsNullOrEmpty(Role))
                throw new AggregateException("Role is required");

            if (validRoles.Contains(Role) == false)
                throw new AggregateException("Invalid role");
        }
    }
}
