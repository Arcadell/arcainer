using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public List<IdentityUser> GetUsers(string? id = null)
        {
            var users = new List<IdentityUser>();

            if(id != null)
                users = context.Users.Where(x => x.Id.Contains(id)).ToList();
            else
                users = context.Users.ToList();

            return users;
        }
    }
}
