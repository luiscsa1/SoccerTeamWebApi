using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSoccerTeams.Models;

namespace WebApiSoccerTeams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public TeamController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Team> GetAllTeams()
        {
            return context.Teams.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamById(int id)
        {
            Team team = context.Teams.Include(x => x.Players).FirstOrDefault(x => x.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost]
        public IActionResult AddNewTeam([FromBody] Team team)
        {
            if (ModelState.IsValid)
            {
                context.Teams.Add(team);
                context.SaveChanges();

                return Ok(team);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam([FromBody] Team team, int id)
        {
            if (team.Id != id)
            {
                return BadRequest();
            }

            context.Entry(team).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            Team team = context.Teams.FirstOrDefault(x => x.Id == id);
            
            if(team == null)
            {
                return NotFound();
            }

            context.Teams.Remove(team);
            context.SaveChanges();

            return Ok(team);
        }
    }
}