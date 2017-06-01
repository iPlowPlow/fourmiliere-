using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;

namespace LibMetier.GestionEnvironnements
{
    class BoutDeTerrain : ZoneAbstrait
    {
    
        public BoutDeTerrain(string nom)
        {
            this.Nom = nom;
        }

        public override void AjouteAcces(AccesAbstrait acces)
        {
            AccesList.Add(acces);
        }

        public override void AjouteOeuf(ObjetAbstrait acces)
        {
            throw new NotImplementedException();
        }

        public override void AjouteNourriture(ObjetAbstrait acces)
        {
            throw new NotImplementedException();
        }

        public override void AjoutePheromone(ObjetAbstrait acces)
        {
            throw new NotImplementedException();
        }

        public override void AjouteFourmis(PersonnageAbstrait acces)
        {
            throw new NotImplementedException();
        }

        public override void AjouteTermite(PersonnageAbstrait acces)
        {
            throw new NotImplementedException();
        }

        public override void RetireTermite(PersonnageAbstrait acces)
        {
            throw new NotImplementedException();
        }

        public override void RetireFourmis(PersonnageAbstrait acces)
        {
            throw new NotImplementedException();
        }
    }
}
