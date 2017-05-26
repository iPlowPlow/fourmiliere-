using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionEnvironnement
{
   public abstract class ZoneAbstrait
    {
      public  List<AccesAbstrait> AccesAbstraitList { get; set; }
        public string Nom { get; set; }
        public List<ObjetAbstrait> ObjetList { get; set; }
        public List<PersonnageAbstrait> PersonnageList { get; set; }
        public abstract void AjouteAcces(AccesAbstrait acces);
        public abstract void AjouteOeuf(ObjetAbstrait acces);
        public abstract void AjouteNourriture(ObjetAbstrait acces);
        public abstract void AjoutePheromone(ObjetAbstrait acces);
        public abstract void AjouteFourmis(PersonnageAbstrait acces);
        public abstract void AjouteTermite(PersonnageAbstrait acces);
        public abstract void RetireTermite(PersonnageAbstrait acces);
        public abstract void RetireFourmis(PersonnageAbstrait acces);
        public ZoneAbstrait( string Nom)
        {
            this.Nom = Nom;
        }

    }
}
