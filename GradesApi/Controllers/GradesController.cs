using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GradesApi.Models;


namespace GradesApi.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/GradeItems")]
    [ApiController]
    public class GradeItemsController : ControllerBase
    {
        private readonly GradeContext _context;

        public GradeItemsController(GradeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeItemDTO>>> GetGradeItems()
        {
          if (_context.GradeItems == null)
          {
              return NotFound();
          }
            return await _context.GradeItems.Select(x => ItemToDTO(x)).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GradeItemDTO>> GetGradeItem(long id)
        {
          if (_context.GradeItems == null)
          {
              return NotFound();
          }
            var gradeItem = await _context.GradeItems.FindAsync(id);

            if (gradeItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(gradeItem);
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGradeItem(long id, GradeItemDTO gradeDTO)
        {
            if (id != gradeDTO.Id)
            {   
                return BadRequest("");
            }

            var gradeItem = await _context.GradeItems.FindAsync(id);

            if (gradeItem == null) return NotFound();


            _context.Entry(gradeItem).State = EntityState.Modified;
            gradeItem.Name = gradeDTO.Name;
            gradeItem.Grade = gradeDTO.Grade;
            gradeItem.IsComplete = gradeDTO.IsComplete;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GradeItemDTO>> PostGradeItem(GradeItemDTO gradeDTO)
        {
            var gradeItem = new GradeItem {
                Name = gradeDTO.Name,
                Grade = gradeDTO.Grade,
                IsComplete = gradeDTO.IsComplete
            };

            _context.GradeItems.Add(gradeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGradeItem), new { id = gradeItem.Id }, ItemToDTO(gradeItem));
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> GradeItem(long id)
        {
            if (_context.GradeItems == null)
            {
                return NotFound();
            }
            var gradeItem = await _context.GradeItems.FindAsync(id);
            if (gradeItem == null)
            {
                return NotFound();
            }

            _context.GradeItems.Remove(gradeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeItemExists(long id)
        {
            return (_context.GradeItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        private static GradeItemDTO ItemToDTO(GradeItem gradeItem) => 
            new GradeItemDTO {
                Id = gradeItem.Id,
                Name = gradeItem.Name,
                Grade = gradeItem.Grade,
                IsComplete = gradeItem.IsComplete
            };

    }
}
