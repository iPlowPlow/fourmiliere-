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
        public  List<AccesAbstrait> AccesList { get; set; }
        public string Nom { get; set; }
        public CoordonneesAbstrait Position { get; set; }
        public List<ObjetAbstrait> ObjetList { get; set; }
        public List<PersonnageAbstrait> PersonnageList { get; set; }
        public abstract void AjouteAcces(AccesAbstrait acces);
        public abstract void AjouteOeuf(ObjetAbstrait oeuf);
        public abstract void AjouteNourriture(ObjetAbstrait nourriture);
        public abstract void AjoutePheromone(ObjetAbstrait phermonoe);
        public abstract void AjouteOuvriere(PersonnageAbstrait personnage);
        public abstract void AjouteGuerriere(PersonnageAbstrait personnage);
        public abstract void AjouteReine(PersonnageAbstrait personnage);
        public abstract void AjouteTermite(PersonnageAbstrait personnage);
        public abstract void RetirerOuvriere(PersonnageAbstrait personnage);
        public abstract void RetirerGuerriere(PersonnageAbstrait personnage);
        public abstract void RetirerReine(PersonnageAbstrait personnage);
        public abstract void RetirerTermite(PersonnageAbstrait personnage);


    }
}
