using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public List<IdentityUser> GetUsers(string? id = null)
        {
            var users = userRepository.GetUsers(id);
            return users;
        }
    }
}
