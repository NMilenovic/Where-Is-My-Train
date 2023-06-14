using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Karta{

        [Key]
        public int ID { get; set; }

        [Range(1,10000)]
        public double Cena { get; set; }

        [Required]
        public Voznja Voznja { get; set; }


    }
}