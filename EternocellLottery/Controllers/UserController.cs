using EternocellLottery.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EternocellLottery.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand cmd)
        {
          
            var entity = new User(cmd.FirstName, cmd.LastName, cmd.PhoneNumber);
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Ok("کاربر با موفقیت ثبت شد");
        }
        [HttpGet]
        public async Task<int> GetAllUserCount()
        {
            var count = _context.Users.Count();
            return count;
        }
    }
}
