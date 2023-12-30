/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

namespace trms.api.Data;

//include only nessasary attributes which need to display
public class UserDto
{
    public string NIC { get; set; }
    public string? UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
   // public string Role { get; set; }
    public bool Active { get; set; }
}