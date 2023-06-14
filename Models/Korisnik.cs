using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{
    public class Korisnik{
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        public string Password {get;set;}
        [Required]
        public bool Ban { get; set; }

        public DateTime DatumBanovanja {get;set;}
        public List<Karta> kupljeneKarte { get; set; }
        [JsonIgnore]
        public List<RecenzijaPrevoznika> recenzijePrevoznika {get;set;}
        [JsonIgnore]
        public List<RecenzijaStanice> recenzijeStanice{get;set;}


    }
}