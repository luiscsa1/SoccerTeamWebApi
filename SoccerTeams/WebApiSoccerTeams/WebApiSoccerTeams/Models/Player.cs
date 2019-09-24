using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSoccerTeams.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
    }
}
