using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly LibraryManagementContext _context;

        public UsersController(LibraryManagementContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> ListOfUsersPage()
        {
            // Retrieve the "Member" role ID
            var memberRoleId = await _context.Roles
                .Where(r => r.Name == "Member")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(memberRoleId))
            {
                return NotFound("Role 'Member' not found.");
            }

            // Retrieve users assigned to the "Member" role
            var usersInMemberRole = await _context.Users
                .Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == memberRoleId))
                .ToListAsync();

            return View(usersInMemberRole);
        }
    }
}
