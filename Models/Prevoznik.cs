using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{
    public class Prevoznik{
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Username { get; set;}
        public string Password { get; set; }
        public List<Zahtev> Zahtevi {get;set;}
    }
}