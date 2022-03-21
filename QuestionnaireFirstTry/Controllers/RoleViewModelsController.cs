using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionnaireFirstTry.Data;
using QuestionnaireFirstTry.Models;
using QuestionnaireFirstTry.Services;

namespace QuestionnaireFirstTry.Controllers
{
    [Authorize(Roles =  "Admin")]

    public class RoleViewModelsController : Controller
    {
        private readonly IManageRolesService _manageRolesService;
        private readonly ApplicationDbContext _context;

        public RoleViewModelsController(IManageRolesService manageRolesService, ApplicationDbContext context)
        {
            _manageRolesService = manageRolesService;
            _context = context;
        }

        // GET: RoleViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _manageRolesService.GetRoles());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Claims = await _context.Claims.ToListAsync();
            return View();
        }

        // POST: RoleViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Claims")] RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {

                await _manageRolesService.CreateRole(roleViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }
        // GET: RoleViewModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleViewModel = await _manageRolesService.GetRole(id);
            if (roleViewModel == null)
            {
                return NotFound();
            }

            return View(roleViewModel);
        }

        // POST: RoleViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _manageRolesService.DeleteRole(id);

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: RoleViewModels/Details/5
        /*public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var roleViewModel = await _manageRolesService.GetRole(id);
                return View(roleViewModel);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            //var roleViewModel = await _manageRolesService.GetRole(id);
           
        }
        */
        // GET: RoleViewModels/Create

        // GET: RoleViewModels/Edit/5
        /*public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleViewModel = await _manageRolesService.GetRole(id);
            if (roleViewModel == null)
            {
                return NotFound();
            }
            return View(roleViewModel);
        }

        // POST: RoleViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id")] RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _manageRolesService.UpdateRole(id,roleViewModel);
                   
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }*/



    }
}
