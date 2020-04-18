using System;
using System.ComponentModel.DataAnnotations;

namespace WhoIsWho.Models
{
    public class Assignation
    {
        public int ID { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
        public int AssignerID { get; set; }
        public int AssignedID { get; set; }
        public int GameID { get; set; }
    }

}