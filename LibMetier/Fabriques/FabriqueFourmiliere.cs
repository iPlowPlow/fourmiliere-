using LibMetier.GestionEnvironnements;
using LibMetier.GestionObjets;
using LibMetier.GestionPersonnages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibMetier
{
    public class FabriqueFourmiliere : FabriqueFourmiliereAbstraite
    {

        public String Titre { get; set; }


        public AccesAbstrait CreerAcces(ZoneAbstraite zDebut, ZoneAbstraite zFin)
        {
            return new Chemin(zDebut, zFin);
        }

        public EnvironmentAbstrait CreerEnvironment()
        {
            return new Fourmiliere();
        }

        public override ObjectAbstrait CreerOeuf(String nom)
        {
            return new Oeuf(nom);
        }

        public override ObjectAbstrait CreerNourriture(String nom)
        {
            return new Nourriture(nom);
        }
        public override ObjectAbstrait CreerPheromone(String nom)
        {
            return new Pheromone(nom);
        }

        public override PersonnageAbstrait CreerReine(String nom)
        {
            return new Reine(nom);
        }

        public override PersonnageAbstrait CreerOuvriere(String nom)
        {
            return new Ouvriere(nom);
        }

        public override PersonnageAbstrait CreerGuerriere(String nom)
        {
            return new Guerriere(nom);
        }

        public override PersonnageAbstrait CreerTermite(String nom)
        {
            return new Termite(nom);
        }

        public ZoneAbstraite CreerZone(String nom)
        {
            return new BoutDeTerrain(nom);
        }


    }
}
