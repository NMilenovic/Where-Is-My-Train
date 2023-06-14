using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Stanica{
        [Key]
        public int ID { get; set; }

        [Required]
        public string Mesto { get; set; }
        

    }
}