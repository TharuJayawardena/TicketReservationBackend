/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/


namespace trms.api.Common.Models
{
    //include only nessasary attributes which need to display
    public class BackOfficeDto
    {

        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
