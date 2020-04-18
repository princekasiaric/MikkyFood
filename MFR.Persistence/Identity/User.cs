using Microsoft.AspNetCore.Identity;

namespace MFR.DomainModels.Identity
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
