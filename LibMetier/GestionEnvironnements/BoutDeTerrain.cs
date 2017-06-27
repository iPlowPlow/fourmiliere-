using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using LibMetier.GestionObjets;

namespace LibMetier.GestionEnvironnements
{
    public class BoutDeTerrain : ZoneAbstrait
    {

        public BoutDeTerrain(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            PersonnageList = new List<PersonnageAbstrait>();
            ObjetList = new List<ObjetAbstrait>();
            this.Position = position;
        }


    }
}
