using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionEnvironnement
{
   public abstract class ZoneAbstrait
    {
        public string Nom { get; set; }
        public CoordonneesAbstrait Position { get; set; }
        public List<ObjetAbstrait> ObjetList { get; set; }
        public List<PersonnageAbstrait> PersonnageList { get; set; }
  


    }
}
