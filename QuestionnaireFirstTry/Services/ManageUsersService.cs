using Microsoft.AspNetCore.Identity;
using QuestionnaireFirstTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireFirstTry.Services
{
    public interface IManageUsersService
    {
        Task<UserViewModel> GetUser(string userId);
        Task<List<UserViewModel>> GetUsers();
        Task AssignRoles(UserViewModel user);
    }
    public class ManageUsersService : IManageUsersService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IManageRolesService _manageRolesService;
        public ManageUsersService(UserManager<IdentityUser> userManager,IManageRolesService manageRolesService)
        {
            _manageRolesService = manageRolesService;
            _userManager = userManager;
        }
        public async Task AssignRoles(UserViewModel user)
        {
            var currUser = await _userManager.FindByIdAsync(user.Id);
            if (currUser == null)
                throw new Exception("User does not exist!");

            var currUserRoles = await _userManager.GetRolesAsync(currUser);
            if (user.Roles == null || user.Roles.Count == 0)
            {
                await _userManager.RemoveFromRolesAsync(currUser, currUserRoles);
            }
            else
            {
                // roles in user but not in currUser
                var rolesToAdd = user.Roles.Where(x => !currUserRoles.Any(currRole => currRole == x));
                // roles in currUser but not in the new user
                var rolesToRemove = currUserRoles.Where(currRole => !user.Roles.Any(x => currRole == x));

                await _userManager.AddToRolesAsync(currUser, rolesToAdd);
                await _userManager.RemoveFromRolesAsync(currUser, rolesToRemove);
            }
        }

        public async Task<UserViewModel> GetUser(string userId)
        {
            var currUser = await _userManager.FindByIdAsync(userId);
            if (currUser == null)
                throw new Exception("User does not exits!");
            var userRoles = await _userManager.GetRolesAsync(currUser);

            var userViewModel = new UserViewModel()
            {
                Id = currUser.Id,
                UserName = currUser.UserName,
                Roles = userRoles.ToList()
            };
            return userViewModel;
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            var usersViewModelList = new List<UserViewModel>();
            var users =  _userManager.Users.AsEnumerable();
            foreach (var item in users)
            {
                usersViewModelList.Add(new UserViewModel()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Roles = (await _userManager.GetRolesAsync(item)).ToList(),
                });
            }
            return usersViewModelList;
        }
    }
}
