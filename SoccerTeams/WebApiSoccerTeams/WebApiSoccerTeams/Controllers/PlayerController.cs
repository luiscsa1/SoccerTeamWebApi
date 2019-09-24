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
    [Route("api/Team/{TeamId}/Player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PlayerController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Player> GetAllPlayers(int teamId)
        {
            return context.Players.Where(x => x.TeamId == teamId).ToList();
        }

        [HttpPost]
        public IActionResult AddNewPlayer([FromBody] Player player, int teamId)
        {
            player.TeamId = teamId;

            if (ModelState.IsValid)
            {
                context.Players.Add(player);
                context.SaveChanges();

                return Ok(player);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id, int teamId)
        {
            Player player = context.Players.FirstOrDefault(x => x.Id == id && x.TeamId == teamId);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer([FromBody] Player player, int id)
        {
            if (player.Id != id)
            {
                return BadRequest();
            }

            context.Entry(player).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            Player player = context.Players.FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            context.Players.Remove(player);
            context.SaveChanges();

            return Ok(player);
        }

    }
}