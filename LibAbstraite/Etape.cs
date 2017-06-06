namespace LibAbstraite
{
    public class Etape
    {
        public int tour { get; set; }
        public string lieu { get; set; }
        public Etape(int tour, string lieu) {
            this.lieu = lieu;
            this.tour = tour;
        }
    }
}