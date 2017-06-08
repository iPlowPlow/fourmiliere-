using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionObjets
{
    public class Nourriture : ObjetAbstrait
    {
        public int morceauNourriture = 20;
        public List<MorceauNourriture> ListMorceaux { get; set; }
        public Nourriture()
        {
         
        }
        public Nourriture(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.Position = position;
            this.ListMorceaux = Enumerable.Repeat(new MorceauNourriture(String.Concat("Un morceau de {0}", Nom)), morceauNourriture).ToList();
        }
        public MorceauNourriture Recolter()
        {
            var morceau = ListMorceaux.First();
            ListMorceaux.Remove(morceau);
            return morceau;
        }
        public override void TourPasse()
        {
            
        }
    }
}
