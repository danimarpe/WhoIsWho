using System;
using System.ComponentModel.DataAnnotations;

namespace WhoIsWho.Models
{
    public class Game
    {
        public int ID { get; set; }

        public Boolean Ready { get; set; }
        public Boolean Started { get; set; }
        public int PlayersReady { get; set; }
        public string Name { get; set; }
    }
}