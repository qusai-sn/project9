using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project9_cohort4.Server.DTOs;
using project9_cohort4.Server.Models;

namespace project9_cohort4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheltersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SheltersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Shelters
        [HttpGet]
        public IActionResult GetShelters()
        {
            return  Ok( _context.Shelters.ToList());
        }

        // GET: api/Shelters/5
        [HttpGet("{id}")]
        public IActionResult GetShelter(int id)
        {
            var shelter =  _context.Shelters.Find(id);

            if (shelter == null)
            {
                return NotFound();
            }

            return Ok(shelter);
        }

        // PUT: api/Shelters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutShelter(int id, updateShelderDTO shelter)
        {
            var existShelder = _context.Shelters.Find(id);

            if (existShelder == null)
            {
                return BadRequest();
            }
            existShelder.ShelterName = shelter.ShelterName;
            existShelder.Description = shelter.Description;
            existShelder.ContactEmail = shelter.ContactEmail;
            existShelder.Phone = shelter.Phone;
            existShelder.Address = shelter.Address;
           
            _context.Shelters.Update(existShelder);
            _context.SaveChanges();
            return Ok(existShelder);
        }

        // POST: api/Shelters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostShelter(addShelderDTO shelter)
        {

            var newShelter = new Shelter
            {
                ShelterName = shelter.ShelterName,

                Description = shelter.Description,
                ContactEmail = shelter.ContactEmail,
                Phone = shelter.Phone,
                Address = shelter.Address
               
            };
            _context.Shelters.Add(newShelter);
            _context.SaveChanges();

            return Ok(newShelter);

        }

        // DELETE: api/Shelters/5
        [HttpDelete("{id}")]
        public IActionResult DeleteShelter(int id)
        {
            var shelter = _context.Shelters.Find(id);
            if (shelter == null)
            {
                return NotFound();
            }

            _context.Shelters.Remove(shelter);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ShelterExists(int id)
        {
            return _context.Shelters.Any(e => e.ShelterId == id);
        }
    }
}
