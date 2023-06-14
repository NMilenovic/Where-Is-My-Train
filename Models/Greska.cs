using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Greska{

        [Key]
        public int ID { get; set; }

        public Voznja Voznja {get;set;}
        [Required]
        [MaxLength(300)]
        public string Opis { get; set; }
    }
}