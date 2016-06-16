using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BrowserTrack.Data.Repositories
{
    public interface IAuthRepository
    {
        void Dispose();
        Task<IdentityUser> FindUser(string userName, string password);
        Task<IdentityResult> RegisterUser(UserModel userModel);
    }
}