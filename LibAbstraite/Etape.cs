using LibAbstraite.GestionEnvironnement;


namespace LibAbstraite
{
    public abstract class EtapeAbstraite
    {
        public int tour { get; set; }
        public string action { get; set; }
        public CoordonneesAbstrait position;
        
    }
}