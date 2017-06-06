using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using LibMetier.GestionObjets;

namespace LibMetier.GestionEnvironnements
{
    public class BoutDeTerrain : ZoneAbstrait
    {

        public BoutDeTerrain(string nom)
        {
            this.Nom = nom;
        }

        public override void AjouteAcces(AccesAbstrait acces)
        {
            AccesList.Add(acces);
        }

        public override void AjouteGuerriere(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }

        public override void AjouteNourriture(ObjetAbstrait nourriture)
        {
            throw new NotImplementedException();
        }

        public override void AjouteOeuf(ObjetAbstrait oeuf)
        {
            throw new NotImplementedException();
        }

        public override void AjouteOuvriere(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }


        public override void AjouteReine(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }

        public override void AjouteTermite(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }
        public override void AjoutePheromone(ObjetAbstrait pheromone)
        {
            ObjetList.Add(pheromone);
        }
        public void RetirerPheromone()
        {
            List<Pheromone> pheromones = ObjetList.OfType<Pheromone>().ToList();
            ObjetList.Remove(pheromones[0]);
        }

        public override void RetirerGuerriere(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }

        public override void RetirerOuvriere(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }

        public override void RetirerReine(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }

        public override void RetirerTermite(PersonnageAbstrait personnage)
        {
            throw new NotImplementedException();
        }

       
    }
}
