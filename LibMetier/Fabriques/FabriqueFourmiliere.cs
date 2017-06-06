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
            return new Chemin(zoneaccessibles);
        }

        public override EnvironnementAbstrait CreerEnvironnement(int dimensionX, int dimensionY)
        {
            return new Fourmiliere(dimensionX, dimensionY);
        }

        public override ObjetAbstrait CreerOeuf(string nom)
        {
            return new Oeuf(nom);
        }

        public override ObjetAbstrait CreerNourriture(string nom, CoordonneesAbstrait position)
        {
            return new Nourriture(nom, position);
        }
        public override ObjetAbstrait CreerPheromone(string nom, CoordonneesAbstrait position)
        {
            return new Pheromone(nom, position);
        }

        public override PersonnageAbstrait CreerReine(string nom)
        {
            return new Reine(nom, new Coordonnees(10, 10));
        }

        public override PersonnageAbstrait CreerOuvriere(string nom)
        {
            return new Ouvriere(nom, new Coordonnees(10, 10));
        }

        public override PersonnageAbstrait CreerGuerriere(string nom)
        {
            return new Guerriere(nom, new Coordonnees(10, 10));
        }

        public override PersonnageAbstrait CreerTermite(string nom)
        {
            return new Termite(nom, new Coordonnees(0, 10));
        }

        public ZoneAbstrait CreerZone(String nom)
        {
            return new BoutDeTerrain(nom);
        }

        public override ZoneAbstrait CrerZone(string nom)
        {
            throw new NotImplementedException();
        }
    }
}
