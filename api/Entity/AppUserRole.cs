using Microsoft.AspNetCore.Identity;

namespace api.Entity
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public ApiUser User { get; set; }
        public AppRole Role { get; set; }
    }
}