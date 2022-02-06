using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FIADbContext.Model.DTO;

namespace FIAWebApi.BL
{
    public partial class UsersService
    {
        private readonly UserManager<UserDbDTO> userManager;

        public UsersService(UserManager<UserDbDTO> userManager)
        {
            this.userManager = userManager;
        }

        async Task<Exception> ApplyToUser(string userName, Func<UserDbDTO, Task<Exception>> method)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new KeyNotFoundException($"Пользователь {userName} не найден.");
            }
            return await method(user);
        }

        async Task<bool> UserExists(string userName)
        {
            return await userManager.FindByNameAsync(userName) != null;
        }
    }
}
