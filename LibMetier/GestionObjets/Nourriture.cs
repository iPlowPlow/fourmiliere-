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
        public const int MORCEAU_NOURRITURE = 20;
        public List<MorceauNourriture> ListMorceaux { get; set; }
        public Nourriture()
        {
         
        }
        public Nourriture(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.Position = position;
            this.Morceaux = Enumerable.Repeat(new MorceauNourriture(String.Concat("Un morceau de {0}", Nom), Position), MORCEAU_NOURRITURE).ToList();
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
