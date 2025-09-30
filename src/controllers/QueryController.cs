using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartStudentQueryAPI.Data;
using SmartStudentQueryAPI.Models;
using SmartStudentQueryAPI.Services;

namespace SmartStudentQueryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AIHelper _aiHelper;

        public QueryController(AppDbContext context, AIHelper aiHelper)
        {
            _context = context;
            _aiHelper = aiHelper;
        }

        // ---------- CREATE a Query ----------
        [HttpPost]
        public async Task<IActionResult> AddQuery([FromBody] CreateQueryDto dto)
        {
            // Check if the student exists
            var student = await _context.Students.FindAsync(dto.StudentId);
            if (student == null)
                return BadRequest("Student not found.");

            // Create and save the query
            var query = new Query
            {
                QueryText = dto.QueryText,
                StudentId = dto.StudentId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Queries.Add(query);
            await _context.SaveChangesAsync();

            return Ok(query);
        }

        // ---------- GET All Queries ----------
        [HttpGet]
        public async Task<IActionResult> GetAllQueries([FromQuery] string keyword = "")
        {
            var q = _context.Queries.Include(x => x.Student).AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                q = q.Where(x => x.QueryText.Contains(keyword));
            }

            var list = await q.ToListAsync();
            return Ok(list);
        }

        // ---------- GET a single Query ----------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuery(int id)
        {
            var query = await _context.Queries
                                      .Include(q => q.Student)
                                      .FirstOrDefaultAsync(q => q.Id == id);
            if (query == null) return NotFound();
            return Ok(query);
        }

        // ---------- Get AI Answer ----------
        [HttpGet("{id}/answer")]
        public async Task<IActionResult> GetAIAnswer(int id)
        {
            var query = await _context.Queries.FindAsync(id);
            if (query == null) return NotFound();

            var answer = await _aiHelper.GetAnswerAsync(query.QueryText);
            return Ok(new { Query = query.QueryText, AIAnswer = answer });
        }
    }
}
