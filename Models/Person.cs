using System;
using System.ComponentModel.DataAnnotations;

namespace WhoIsWho.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Display(Name = "Nombre")]
        [DataType(DataType.Text)]
        public String Name { get; set; }

        [Display(Name = "Foto")]
        [DataType(DataType.ImageUrl)]
        public String Picture { get; set; }
        public Boolean Ready { get; set; }
    }
}