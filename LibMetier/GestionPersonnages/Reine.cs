﻿using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibAbstraite.GestionObjets;
using LibMetier.GestionEnvironnements;
using LibMetier.GestionObjets;
using LibMetier.GestionStrategie;

namespace LibMetier.GestionPersonnages
{
    public class Reine : PersonnageAbstrait 
    {
        private static Reine instance;
        private Oeuf oeufPondu;

        public Reine()
        {
           
        }

        public static Reine Instance()
        {

            if (instance == null)
            {
                instance = new Reine();
            }
            return instance;
        }
        public static Reine Instance(string nom, CoordonneesAbstrait position)
        {
            if (instance == null)
            {
                instance = new Reine();
            }
            instance.Nom = nom;
            instance.PV = 500;
            instance.Position = position;
            instance.Maison = new Coordonnees();
            instance.Maison.X = position.X;
            instance.Maison.Y = position.Y;
            instance.ListEtape = new ObservableCollection<Etape>();
            instance.zone = new BoutDeTerrain("default", position);
            instance.StategieCourante = new Immobile("Immobile");
            return instance;
        }
        public Oeuf OeufPondu { 
            get
            {
                if (instance == null)
                {
                    return null;
                }
                return instance.oeufPondu;
            }
            set
            {
                if (instance != null)
                {
                    instance.oeufPondu = value;
                }
            }

        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }

        public override void AnalyseSituation()
        {

            List<ObjetAbstrait> morceaux = zone.ObjetList.Where(x => x.GetType().Equals(typeof(MorceauNourriture))).ToList();
            if (morceaux.Count > 0)
            {
                PondreOeuf();
            }
            foreach (PersonnageAbstrait unPerso in zone.PersonnageList)
            {


                if (unPerso.GetType().Equals(typeof(Guerriere)))
                {
                    /*Indiquer chemin nourriture*/
                }
                else if (unPerso.GetType().Equals(typeof(Termite)))
                {
                    AjouterEtape(0, "Une termite m'attaque ! ");
                }
            }
        }

        public override string ToString()
        {
            return "Ma Reine" + instance.Nom;
        }
        public void PondreOeuf()
        {
            FabriqueFourmiliere fabrique = new FabriqueFourmiliere();
            CoordonneesAbstrait pos = new Coordonnees();
            pos.X = Position.X + Fourmiliere.Hazard.Next(-1, 2);
            pos.Y = Position.Y + Fourmiliere.Hazard.Next(-1, 2);
            oeufPondu =(Oeuf)fabrique.CreerOeuf(String.Format("Oeuf N° {0}", zone.ObjetList.Where(x=>x.GetType().Equals(typeof(Oeuf))).Count()), pos);
        }

        public override void maj(string etat)
        {
         
        }
    }
}
