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


        public override AccesAbstrait CreerAcces(ZoneAbstrait zDebut, ZoneAbstrait zFin)
        {
            return new Chemin(zFin, zDebut);
        }

        public override EnvironnementAbstrait CreerEnvironnement(int dimensionX, int dimensionY)
        {
            return new Fourmiliere(dimensionX, dimensionY);
        }

        public override ObjetAbstrait CreerOeuf(string nom)
        {
            return new Oeuf(nom);
        }

        public override ObjetAbstrait CreerNourriture(string nom)
        {
            return new Nourriture(nom);
        }
        public override ObjetAbstrait CreerPheromone(string nom)
        {
            return new Pheromone(nom);
        }

        public override PersonnageAbstrait CreerReine(string nom)
        {
            return new Reine(nom);
        }

        public override PersonnageAbstrait CreerOuvriere(string nom)
        {
            return new Ouvriere(nom);
        }

        public override PersonnageAbstrait CreerGuerriere(string nom)
        {
            return new Guerriere(nom);
        }

        public override PersonnageAbstrait CreerTermite(string nom)
        {
            return new Termite(nom);
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
