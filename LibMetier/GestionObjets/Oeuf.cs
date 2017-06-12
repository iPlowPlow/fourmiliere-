using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMetier.GestionEnvironnements;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;
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
        public override void TourPasse()
        {
            Age++;
            if(Age == DUREE_AVANT_ECLOSION)
            {
                Eclore();
            }
        }
        public void Eclore()
        {
            int res = Fourmiliere.Hazard.Next(1, 6);
            if (res < 5)
            {
                fourmiARetourner = Fourmiliere.Fabrique.CreerOuvriere("Ouvriere N°", Position);
            }
            else
            {
                fourmiARetourner = Fourmiliere.Fabrique.CreerGuerriere("Guerrière N°", Position);
            }
        }
    }
}
