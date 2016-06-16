using Microsoft.AspNet.Identity.EntityFramework;

public class AuthDBContext : IdentityDbContext<IdentityUser>
{
    public AuthDBContext()
        : base("Name = AuthDBContext")
    {

    }
}