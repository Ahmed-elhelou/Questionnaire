using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireFirstTry.Models;
using QuestionnaireFirstTry.Services;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireFirstTry.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UsersController : Controller
    {

        private readonly IManageUsersService _manageUsersService;
        private readonly IManageRolesService _manageRolesService;

        public UsersController(IManageUsersService manageUsersService, IManageRolesService manageRolesService)
        {
            _manageUsersService = manageUsersService;
            _manageRolesService = manageRolesService;
        }
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var users = await _manageUsersService.GetUsers();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> AssignRoles(string id)
        {
            var user = await _manageUsersService.GetUser(id);
            var allRoles = (await _manageRolesService.GetRoles()).Select(x => x.Name).ToList();
             ViewBag.AllRoles = allRoles;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoles([Bind("Id,UserName,Roles")] UserViewModel userViewModel)
        {
            await _manageUsersService.AssignRoles(userViewModel);
            return RedirectToAction(nameof(Index));
        }




    }
}
