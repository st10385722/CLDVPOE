using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE.Models
{
    //for the roles class
    public class UserViewModel
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> AllRoles { get; set; }
        public string SelectedRole { get; set; }
    }
}
