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
        public const int DUREE_AVANT_ECLOSION = 15;
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
            if(Age == DUREE_AVANT_ECLOSION)
            {
                Eclore();
            }
        }
        public void Eclore()
        {
            int res = Fourmiliere.Hazard.Next(1, 21);
            if (res > 0 && res <= 16)
            {
                fourmiARetourner = Fourmiliere.Fabrique.CreerOuvriere("Ouvriere N°", Fourmiliere.Fabrique.CreerPosition(Fourmiliere.coordMaison.X, Fourmiliere.coordMaison.Y), Fourmiliere.coordMaison); 
            }
            else if (res > 16 && res < 20)
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
