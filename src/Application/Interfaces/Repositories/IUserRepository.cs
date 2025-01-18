using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        List<IdentityUser> GetUsers(string? id = null);
    }
}
