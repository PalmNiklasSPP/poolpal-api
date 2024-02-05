using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.DirectoryServices.AccountManagement;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poolpal_api.Database;
using poolpal_api.Models;
using System.Security.Principal;

namespace poolpal_api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly PoolTournamentContext _context;

        public UserController(PoolTournamentContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<Player>> GetCurrentUser()
        {
            if (HttpContext.User.Identity is not WindowsIdentity windowsIdentity) return Unauthorized();




            var userName = windowsIdentity.Name;

            // Check if user is in database
            var user = await _context.Players.FirstOrDefaultAsync(p => p.LoginId == userName);
            if (user != null) return user;


            // User not found in database, fetch from Active Directory
            using var context = new PrincipalContext(ContextType.Domain, "STB.local");
            var userPrincipal = UserPrincipal.FindByIdentity(context, userName);
            if (userPrincipal != null)
            {
                // Create new user from Active Directory info
                user = new Player
                {
                    PlayerName = userPrincipal.DisplayName,
                    LoginId = userName,
                    RankingPoints = 0 // Default value, adjust as needed
                };

                // Add to database and save
                _context.Players.Add(user);
                await _context.SaveChangesAsync();
                return user;

            }

            return NotFound("User not found in Active Directory.");
        }
    }

}
