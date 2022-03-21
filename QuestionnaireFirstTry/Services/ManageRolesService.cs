using Microsoft.AspNetCore.Identity;
using QuestionnaireFirstTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuestionnaireFirstTry.Services
{
    public interface IManageRolesService
    {
        Task<RoleViewModel> GetRole(string roleId);
        Task<List<RoleViewModel>> GetRoles();
        Task<RoleViewModel> CreateRole(RoleViewModel role);
        Task<RoleViewModel> UpdateRole(string roleId,RoleViewModel role);
        Task DeleteRole(string roleId);

    }
    public class ManageRolesService : IManageRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRolesService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
       
        public async Task<RoleViewModel> CreateRole(RoleViewModel role)
        {
            var currRole = new IdentityRole(role.Name);
            if(await _roleManager.RoleExistsAsync(currRole.Name))
                throw new Exception("Role already exist");
            await _roleManager.CreateAsync(currRole);
            foreach (var claimType in role.Claims)
            {
                await _roleManager.AddClaimAsync(currRole, new Claim(claimType,""));
            }
            return role;
        }

        public async Task DeleteRole(string roleId)
        {
           var currRole = await _roleManager.FindByIdAsync(roleId);
            if (currRole == null)
                throw new Exception("Role does not exist");
            await _roleManager.DeleteAsync(currRole);
        }

        public async Task<RoleViewModel> GetRole(string roleId)
        {
            var currRole = await _roleManager.FindByIdAsync(roleId);
            if (currRole == null)
                throw new Exception("Role does not exist");
            var claims = await _roleManager.GetClaimsAsync(currRole);
            var currRoleViewModel = new RoleViewModel()
            {
                Id = roleId,
                Name = currRole.Name,
                Claims = claims.Select(x => x.Type).ToList()
            };
            return currRoleViewModel;
        }

        public async Task<List<RoleViewModel>> GetRoles()
        {
            var rolesList = new List<RoleViewModel>();
            var roles =  _roleManager.Roles.AsEnumerable();
            foreach (var role in roles)
            {
                rolesList.Add(new RoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Claims = (await _roleManager.GetClaimsAsync(role)).Select(x => x.Type).ToList(),
                });
            }

           return rolesList;
        }

        public async Task<RoleViewModel> UpdateRole(string roleId, RoleViewModel role)
        {
            var currRole = await _roleManager.FindByIdAsync(roleId);
            if (currRole == null)
                throw new Exception("This role does not exist!");
            currRole.Name = role.Name;

            var currRoleClaims = await _roleManager.GetClaimsAsync(currRole);
            //roles in the new role but not in currRole
            var claimsToAdd = role.Claims.Where(x => !currRoleClaims.Any(currClaim => currClaim.Type == x));
            //roles in the currRole but not in the new one
            var claimsToRemove = currRoleClaims.Where(currClaim => !role.Claims.Any(x => currClaim.Type == x));
            foreach (var claimType in claimsToAdd)
            {
                await _roleManager.AddClaimAsync(currRole, new Claim(claimType, ""));
            }
            foreach (var claim in claimsToRemove)
            {
                await _roleManager.RemoveClaimAsync(currRole, claim);
            }

            return role;
        }
    }
}
