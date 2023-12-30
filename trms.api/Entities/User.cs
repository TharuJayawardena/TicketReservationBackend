/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using trms.api.Common;

namespace trms.api.Entities;

//user attributes inherit from the baseEntity
public class User : BaseEntity
{
    //create NIC as the primary key
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "The NIC is required")]
    public string NIC { get; set; }

    [Required(ErrorMessage = "The UserName is required")]
    public string UserName { get; set; }

    
    [Required(ErrorMessage = "The FirstName is required")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "The LastName is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "The Password is required")]
    public string Password { get; set; }
  /*  
    [Required(ErrorMessage = "The Role is required")]
    public string Role { get; set; }*/

    public bool Active { get; set; }

    public User()
    {
        Active = true;
        UserName = NIC;
    }

    //create validations
    public void Validate()
    {
       // var validRoles = new List<string> {"BackOffice", "TravelAgent", "Traveler"};
        if (string.IsNullOrEmpty(UserName))
            throw new AggregateException("UserName name is required");
        if (string.IsNullOrEmpty(FirstName))
            throw new AggregateException("First name is required");
        if (string.IsNullOrEmpty(LastName))
            throw new AggregateException("Last name is required");
        if (string.IsNullOrEmpty(Password))
            throw new AggregateException("Password is required");

      /*  if (string.IsNullOrEmpty(Role))
            throw new AggregateException("Role is required");

        if(validRoles.Contains(Role) == false)
            throw new AggregateException("Invalid role");*/
    }

}