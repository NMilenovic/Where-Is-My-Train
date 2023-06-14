namespace Models{
    
    public class RecenzijaPrevoznika{
        public int ID { get; set; }
        public Prevoznik Prevoznik { get; set; }

        public string Sadrzaj {get;set;}
        public Korisnik Autor {get;set;}
    }

}