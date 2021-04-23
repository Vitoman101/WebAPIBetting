using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBetting.Models;
using System.Text.Json;

namespace WebAPIBetting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly BetbaumContext _context;
        private List<Fixture> listOfFixtures;

        public FixturesController(BetbaumContext context)
        {
            _context = context;
        }

        // GET: api/Fixtures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fixture>>> GetFixture()
        {
            var fixture = await _context.Fixture.Include(fix => fix.League).ToListAsync();
            return fixture;
        }

        // GET: api/Fixtures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fixture>> GetFixture(int id)
        {
            var fixture = await _context.Fixture.FindAsync(id);

            if (fixture == null)
            {
                return NotFound();
            }

            return fixture;
        }

        // PUT: api/Fixtures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFixture(int id, Fixture fixture)
        {
            if (id != fixture.Id)
            {
                return BadRequest();
            }

            _context.Entry(fixture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixtureExists(id))
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

        // POST: api/Fixtures/getfixtures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("GetFixtures")]
        public async Task<ActionResult<IEnumerable<Fixture>>> PostFixtureGetList(Fixture fixture)
        {
            listOfFixtures = new List<Fixture>();
            try
            {
                listOfFixtures.AddRange(await _context.Fixture.Include(fix => fix.League).ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

            return listOfFixtures.FindAll(x => 
                                            x.LeagueId == fixture.LeagueId && 
                                            x.Date.ToString("yyyy-MM-dd") == fixture.Date.ToString("yyyy-MM-dd"));
        }

        // POST: api/Fixtures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fixture>> PostFixture(Fixture fixture)
        {
            _context.Fixture.Add(fixture);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FixtureExists(fixture.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFixture", new { id = fixture.Id }, fixture);
        }

        // DELETE: api/Fixtures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fixture>> DeleteFixture(int id)
        {
            var fixture = await _context.Fixture.FindAsync(id);
            if (fixture == null)
            {
                return NotFound();
            }

            _context.Fixture.Remove(fixture);
            await _context.SaveChangesAsync();

            return fixture;
        }

        private bool FixtureExists(int id)
        {
            return _context.Fixture.Any(e => e.Id == id);
        }
    }
}
