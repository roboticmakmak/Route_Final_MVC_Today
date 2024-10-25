using Company.Data.Entites;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        // Method for listing users with search functionality
        public async Task<IActionResult> Index(string searchInp)
        {
            List<ApplicationUser> users;

            if (string.IsNullOrEmpty(searchInp))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.Users
                    .Where(u => u.NormalizedEmail.Trim().Contains(searchInp.Trim().ToUpper()))
                    .ToListAsync();
            }

            return View(users);
        }

        // Method to show user details
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("User ID cannot be null");

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (viewName == "Update")
            {
                var userViewModel = new UserUpdateViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };
                return View(viewName, userViewModel);
            }
            return View(viewName, user);
        }
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id , "Update");
        }

        [HttpPost]
        
        public async Task<IActionResult> Update(string id, UserUpdateViewModel applicationUser)
        {
            if (id != applicationUser.Id)
                return NotFound();


            if (ModelState.IsValid)
            {

                try
                {
                    var user = await _userManager.FindByIdAsync(id);

                    if (user is null)
                        return NotFound();

                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Successfully");
                        return RedirectToAction("Index");
                    }

                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }




            }
            return View(applicationUser);
        }


      
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("User ID cannot be null");

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                    return NotFound();

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User with ID {id} deleted successfully.");
                    return RedirectToAction("Index");
                }

                // Log errors if deletion failed
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error deleting user {id}: {error.Description}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while deleting user with ID {id}: {ex.Message}");
            }

            return RedirectToAction("Index");
        }
    }
}
