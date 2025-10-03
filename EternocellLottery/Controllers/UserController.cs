using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using EternocellLottery.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var entity = new User(cmd.FullName, cmd.InstagramId, cmd.PhoneNumber);
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
        [HttpGet]
        public async Task Delete()
        {
            _context.Users.ExecuteDelete();

        }



        [HttpGet]
        public async Task<IActionResult> ExportUsersToExcel()
        {
            var users = await _context.Users
                .OrderBy(u => u.CreatedAt)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");

                // Header row
                worksheet.Cell(1, 1).Value = "ایدی";
                worksheet.Cell(1, 2).Value = "نام و نام خانوادگی";
                worksheet.Cell(1, 3).Value = "ایدی اینستاگرام";
                worksheet.Cell(1, 4).Value = "شماره موبایل";
                worksheet.Cell(1, 5).Value = "تاریخ شرکت";

                // Data rows
                for (int i = 0; i < users.Count; i++)
                {
                    var u = users[i];
                    worksheet.Cell(i + 2, 1).Value = u.Id;
                    worksheet.Cell(i + 2, 2).Value = u.FullName;
                    worksheet.Cell(i + 2, 3).Value = u.InstagramId;
                    worksheet.Cell(i + 2, 4).Value = u.PhoneNumber ?? "";
                    worksheet.Cell(i + 2, 5).Value = u.CreatedAt.ToString("yyyy-MM-dd HH:mm");
                }

                // Auto-size columns
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Users.xlsx");
                }
            }
        }


    }
}
