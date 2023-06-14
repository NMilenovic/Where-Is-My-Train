namespace Models{
    public class RecenzijaStanice{
        public int ID { get; set; }

        public Stanica Stanica { get; set; }

        public string Sadrzaj { get; set; }

        public Korisnik Autor { get; set; }


    }
}