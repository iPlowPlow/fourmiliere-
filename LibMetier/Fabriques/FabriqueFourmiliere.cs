using LibMetier.GestionEnvironnements;
using LibMetier.GestionObjets;
using LibMetier.GestionPersonnages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibAbstraite.Fabrique;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
namespace LibMetier
{
    public class FabriqueFourmiliere : FabriqueAbstraite
    {

        public override String Titre { get; }

        public override AccesAbstrait CreerAcces(List<ZoneAbstrait> zoneaccessibles)
        {
            return new Acces(zoneaccessibles);
        }

        public override EnvironnementAbstrait CreerEnvironnement(int dimensionX, int dimensionY)
        {
            return new Fourmiliere(dimensionX, dimensionY);
        }

        public override ObjetAbstrait CreerOeuf(string nom, CoordonneesAbstrait position)
        {
            return new Oeuf(nom, position);
        }

        public override ObjetAbstrait CreerNourriture(string nom, CoordonneesAbstrait position)
        {
            return new Nourriture(nom, position);
        }
        public override ObjetAbstrait CreerPheromone(string nom, CoordonneesAbstrait position)
        {
            return new Pheromone(nom, position);
        }

        public override PersonnageAbstrait CreerReine(string nom, CoordonneesAbstrait position)
        {
            return Reine.Instance(nom, position);
        }

        public override PersonnageAbstrait CreerOuvriere(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine)
        {
            return new Ouvriere(nom, position, positionReine);
        }

        public override PersonnageAbstrait CreerPrincesse(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine)
        {
            return new Princesse(nom, position, positionReine);
        }

        public override PersonnageAbstrait CreerGuerriere(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine)
        {
            return new Guerriere(nom, position, positionReine);
        }

        public override PersonnageAbstrait CreerTermite(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionFourmilliere)
        {
            return new Termite(nom, position, positionFourmilliere);
        }


        public override ZoneAbstrait CreerZone(string nom, CoordonneesAbstrait position)
        {
            return new BoutDeTerrain(nom, position);
        }
        public override CoordonneesAbstrait CreerPosition(int x, int y)
        {
            return new Coordonnees(x, y);
        }
    }
}
