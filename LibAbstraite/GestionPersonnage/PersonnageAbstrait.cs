using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibAbstraite.GestionPersonnage
{
    public abstract class PersonnageAbstrait
    {
        public bool TransporteNourriture = false;
        public static Random Hazard = new Random();
        public AccesAbstrait ChoixZoneSuivante { get; set; }
        public int Pointsdevie { get; set; }
        public string Nom { get; set; }
        public CoordonneesAbstrait Position
        {
            get; set;
        }
        public abstract void Avance1Tour(int dimX, int dimY);

        public abstract void AvanceAuHazard(int dimX, int dimY);

        public abstract ZoneAbstrait ChoisirZoneSuivante();
    }
}
