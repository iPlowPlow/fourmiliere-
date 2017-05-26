using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fourmilliereALIHM
{
    public abstract class FabriqueAbstraite
    {
        public abstract string Titre { get; }
        public abstract EnvironnementAbstrait CreerEnvironnement();
        public abstract ZoneAbstrait CrerZone(string nom);
        public abstract AccesAbstrait CreerAcces(ZoneAbstrait zdebut, ZoneAbstrait zfin);
        public abstract PersonnageAbstrait CreerPersonnage(string nom);
        public abstract ObjetAbstrait CreerObjet(string nom);

    }
}
