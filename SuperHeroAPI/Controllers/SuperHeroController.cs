using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _contect;

        public SuperHeroController(DataContext context)
        {
            _contect = context;
        }



        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _contect.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _contect.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _contect.SuperHeroes.Add(hero);
            await _contect.SaveChangesAsync();
            return Ok(await _contect.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await _contect.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
                return BadRequest("Hero not found.");

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await _contect.SaveChangesAsync();

            return Ok(await _contect.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var dbhero = await _contect.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero not found.");

            _contect.SuperHeroes.Remove(dbhero);
            await _contect.SaveChangesAsync();

            return Ok(await _contect.SuperHeroes.ToListAsync());
        }
    }
}
