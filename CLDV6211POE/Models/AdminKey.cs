using Microsoft.AspNetCore.Identity;

namespace CLDV6211POE.Models
{
    //this model stores the admin key in the database (yes, very unsafe and dangerous), but otherwise
    //i couldnt figure out how not to have it
    public class AdminKey: IdentityUser
    {
        public string? Adminkey { get; set; }
    }
}
