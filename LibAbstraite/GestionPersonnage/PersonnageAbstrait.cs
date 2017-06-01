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
        public AccesAbstrait choixZoneSuivante { get; set; }
        public int pointsdevie { get; set; }
        static Random Hazard = new Random();
        public string Nom { get; set; }
        public abstract ZoneAbstrait ChoixZoneSuivante();
        public CoordonneesAbstrait position
        {
            get; set;
        }

    }
}
