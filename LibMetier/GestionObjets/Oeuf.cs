using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMetier.GestionEnvironnements;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;
using LibAbstraite;

namespace LibMetier.GestionObjets
{
  public  class Oeuf : ObjetAbstrait
    {
        public static int DureeAvantEclosion = 15;
        public PersonnageAbstrait fourmiARetourner;
        //Incrémenté à chaque tour
        public int Age { get; set; }
        public Oeuf()
        {
          
        }
        public Oeuf(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.Age = 0;
            this.Position = position;
        }
        public override void TourPasse(SujetAbstrait meteo)
        {
            Age++;
            if(Age == DureeAvantEclosion)
            {
                Eclore();
            }
        }
        public void Eclore()
        {
            int res = Fourmiliere.Hazard.Next(1, 100);
            if (res > 0 && res <= Config.probaOuvriere)
            {
                fourmiARetourner = Fourmiliere.Fabrique.CreerOuvriere("Ouvriere N°", Fourmiliere.Fabrique.CreerPosition(Fourmiliere.coordMaison.X, Fourmiliere.coordMaison.Y), Fourmiliere.coordMaison); 
            }
            else if (res > Config.probaOuvriere && res < (Config.probaOuvriere+Config.probaGuerriere))
            {
                fourmiARetourner = Fourmiliere.Fabrique.CreerGuerriere("Guerriere N°", Fourmiliere.Fabrique.CreerPosition(Fourmiliere.coordMaison.X, Fourmiliere.coordMaison.Y), Fourmiliere.coordMaison);

            }
            else
            {
                fourmiARetourner = Fourmiliere.Fabrique.CreerPrincesse("Princesse N°", Fourmiliere.Fabrique.CreerPosition(Fourmiliere.coordMaison.X, Fourmiliere.coordMaison.Y), Fourmiliere.coordMaison);

            }
        }
    }
}
