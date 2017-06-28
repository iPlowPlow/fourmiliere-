using LibMetier.GestionPersonnages;
using LibMetier.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier
{
    public class Config
    {
        public static int probaPluie = 20;
        public static int probaBrouillard = 10;
        public static int probaClair = 70;
        public static int probaOuvriere = 80;
        public static int probaGuerriere = 15;
        public static int probaPrincesse = 5;
        public static int nbrTermites = 15;
        public int HPReine
        {
            get { return Reine.HPNaissance; }
            set
            {
                Reine.HPNaissance = value;
            }
        }
        public int HPOuvriere
        {
            get { return Ouvriere.HPNaissance; }
            set
            {
                Ouvriere.HPNaissance = value;
            }
        }
        public int HPGuerriere
        {
            get { return Guerriere.HPNaissance; }
            set
            {
                Guerriere.HPNaissance = value;
            }
        }
        public int HPPrincesse
        {
            get { return Princesse.HPNaissance; }
            set
            {
                Princesse.HPNaissance = value;
            }
        }
        public int HPTermite
        {
            get { return Termite.HPNaissance; }
            set
            {
                Termite.HPNaissance = value;
            }
        }
        public int DureeVieNourriture
        {
            get { return Nourriture.DureeVieOriginale; }
            set
            {
                Nourriture.DureeVieOriginale = value;
            }
        }
        public int DureeEclosionOeuf
        {
            get { return Oeuf.DureeAvantEclosion; }
            set
            {
                Oeuf.DureeAvantEclosion = value;
            }
        }
        public int DureeViePheromone
        {
            get { return Pheromone.DureeVieOriginale; }
            set
            {
                Pheromone.DureeVieOriginale = value;
            }
        }
        public int ProbaPluie
        {
            get { return probaPluie; }
            set
            {
                probaPluie = value;
            }
        }
        public int ProbaBrouillard
        {
            get { return probaBrouillard; }
            set
            {
                probaBrouillard = value;
            }
        }
        public int ProbaClair
        {
            get { return probaClair; }
            set
            {
                probaClair = value;
            }
        }
        public int ProbaOuvriere
        {
            get { return probaOuvriere; }
            set
            {
                probaOuvriere = value;
            }
        }
        public int ProbaGuerriere
        {
            get { return probaGuerriere; }
            set
            {
                probaGuerriere = value;
            }
        }
        public int ProbaPrincesse
        {
            get { return probaPrincesse; }
            set
            {
                probaPrincesse = value;
            }
        }
        public int NombreTermites
        {
            get { return nbrTermites; }
            set
            {
                nbrTermites = value;
            }
        }
    }
}
