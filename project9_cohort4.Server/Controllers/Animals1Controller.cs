using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project9_cohort4.Server.DTOs;
using project9_cohort4.Server.Models;

namespace project9_cohort4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Animals1Controller : ControllerBase
    {
        private readonly MyDbContext _context;

        public Animals1Controller(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Animals1
        [HttpGet]
        public IActionResult GetAnimals()
        {
            return Ok(_context.Animals.ToList());
        }

        // GET: api/Animals1/5
        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = _context.Animals.Find(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        // PUT: api/Animals1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAnimal(int id,[FromForm] updateAnimalDTO animal)
        {
            var existAnimal = _context.Animals.Find(id);

            if (existAnimal == null)
            {
                return BadRequest();
            }
            existAnimal.Name = animal.Name;
            existAnimal.Age = animal.Age;
            existAnimal.Species = animal.Species;
            existAnimal.Breed = animal.Breed;
            existAnimal.Size = animal.Size;
            existAnimal.Temperament = animal.Temperament;
            existAnimal.SpecialNeeds = animal.SpecialNeeds;
            existAnimal.Description = animal.Description;
            existAnimal.AdoptionStatus = animal.AdoptionStatus;
            existAnimal.PhotoUrl = animal.PhotoUrl;

            _context.Animals.Update(existAnimal);
            _context.SaveChanges();
            return Ok(existAnimal);
        }

        // POST: api/Animals1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal([FromForm] addAnimalDTO animal)
        {
            var newAnimal = new Animal
            {
                Name = animal.Name,

                Age = animal.Age,
                Species = animal.Species,
                Breed = animal.Breed,
                Size = animal.Size,
                Temperament = animal.Temperament,
                SpecialNeeds = animal.SpecialNeeds,
                Description = animal.Description,
                AdoptionStatus = animal.AdoptionStatus,
                PhotoUrl = animal.PhotoUrl,
            };
            _context.Animals.Add(newAnimal);
             _context.SaveChanges();

            return Ok( newAnimal);
        }

        // DELETE: api/Animals1/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            _context.SaveChanges();

            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.AnimalId == id);
        }
    }
}
