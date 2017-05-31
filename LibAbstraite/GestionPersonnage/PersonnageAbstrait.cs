using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionPersonnage
{
   public abstract class PersonnageAbstrait
    {
        static Random Hazard = new Random();
        public string Nom { get; set; }
        public abstract ZoneAbstrait ChoixZoneSuivante(List<AccesAbstrait> accesList);
        public CoordonneesAbstrait position
        {
            get; set;
        }

    }
}
