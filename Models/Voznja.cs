using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models{
    public class Voznja{
        public int ID { get; set; }
        public Stanica PocetnaStanica { get; set; }
        public Stanica KrajnaStanica { get; set; }
        public DateTime Termin { get; set; }
        
        public DateTime Stize {get;set;}
        public Prevoznik Prevoznik { get; set; }
        }
}