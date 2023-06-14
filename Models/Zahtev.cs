
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{
    public class Zahtev{
        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public Prevoznik Prevoznik { get; set; }
        public Voznja ListaVoznji { get; set; }
        public string UsernamePrevoznik {get;set;}
    }
}