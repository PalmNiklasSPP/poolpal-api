using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.DirectoryServices.AccountManagement;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poolpal_api.Database;
using System.Security.Principal;
using poolpal_api.Database.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace poolpal_api.Controllers
{
    [Route("api/[controller]")]
    public class UserController(PoolTournamentContext context, IMemoryCache memoryCache) : ControllerBase
    {
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<Player>> GetCurrentUser()
        {
            if (HttpContext.User.Identity is not WindowsIdentity windowsIdentity) return Unauthorized();




            var userName = windowsIdentity.Name;

            // Check if user is in database
            var user = await context.Players.FirstOrDefaultAsync(p => p.LoginId == userName);
            if (user != null) return user;


            // User not found in database, fetch from Active Directory
            using var context1 = new PrincipalContext(ContextType.Domain, "STB.local");
            var userPrincipal = UserPrincipal.FindByIdentity(context1, userName);
            if (userPrincipal != null)
            {
                // Create new user from Active Directory info
                user = new Player
                {
                    PlayerName = userPrincipal.DisplayName,
                    LoginId = userName,
                    ELO = 1500 // Default value, adjust as needed
                };

                // Add to database and save
                context.Players.Add(user);
                await context.SaveChangesAsync();
                return user;

            }

            return NotFound("User not found in Active Directory.");
        }


        [HttpGet("AllUsers")]
        public async Task<ActionResult<Player>> GetUsers()
        {
            var players = await context.Players.ToListAsync();
            return Ok(players);
        }


        [HttpGet("GetUserBySearch")]
        public IEnumerable<Player> GetUserBySearch(string search)
        {
            const string cacheKey = "PlayersCache";
            List<Player> players;

            if (!memoryCache.TryGetValue(cacheKey, out players))
            {
                // Cache is empty, load data from database
                players = context.Players.ToList();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Cache expiration time

                memoryCache.Set(cacheKey, players, cacheEntryOptions);
            }

            if (!string.IsNullOrEmpty(search))
            {
                var result= players.Where(p => p.PlayerName.Contains(search, StringComparison.OrdinalIgnoreCase)).Take(5);
                return result;
            }

            return players;
        }
    }

}
