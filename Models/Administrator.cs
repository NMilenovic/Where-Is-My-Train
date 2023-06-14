using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Administrator{

        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}