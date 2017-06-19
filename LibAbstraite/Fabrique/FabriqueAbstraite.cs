using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.Fabrique
{
    public abstract class FabriqueAbstraite
    {
        public abstract string Titre { get; }
        public abstract EnvironnementAbstrait CreerEnvironnement(int dimensionX, int dimensionY);
        public abstract ZoneAbstrait CreerZone(string nom, CoordonneesAbstrait position);
        public abstract AccesAbstrait CreerAcces(List<ZoneAbstrait> zoneaccessibles);
        public abstract CoordonneesAbstrait CreerPosition(int x, int y);
        public abstract PersonnageAbstrait CreerGuerriere(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine);
        public abstract PersonnageAbstrait CreerOuvriere(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine);
        public abstract PersonnageAbstrait CreerPrincesse(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine);
        public abstract PersonnageAbstrait CreerReine(string nom, CoordonneesAbstrait position);
        public abstract PersonnageAbstrait CreerTermite(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionFourmilliere);
        public abstract ObjetAbstrait CreerOeuf(string nom, CoordonneesAbstrait position);
        public abstract ObjetAbstrait CreerPheromone(string nom, CoordonneesAbstrait position);
        public abstract ObjetAbstrait CreerNourriture(string nom, CoordonneesAbstrait position);

    }
}
