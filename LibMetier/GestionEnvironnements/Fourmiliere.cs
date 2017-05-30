using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.Fabrique;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;

namespace LibMetier.GestionEnvironnements
{
    class Fourmiliere : EnvironnementAbstrait
    {

        public Fourmiliere()
        {

        }

        public override void AjouteChemin(FabriqueAbstraite fan, params AccesAbstrait[] accesArray)
        {
            throw new NotImplementedException();
        }

        public override void AjouteFourmis(PersonnageAbstrait unPersonnage)
        {
            throw new NotImplementedException();
        }

        public override void AjouteNourriture(ObjetAbstrait unObject)
        {
            throw new NotImplementedException();
        }

        public override void AjouteOeuf(ObjetAbstrait unObject)
        {
            throw new NotImplementedException();
        }

        public override void AjoutePheromone(ObjetAbstrait unObject)
        {
            throw new NotImplementedException();
        }

        public override void AjouteTermite(PersonnageAbstrait unPersonnage)
        {
            throw new NotImplementedException();
        }

        public override void AjouteZone(params ZoneAbstrait[] zoneArray)
        {
            throw new NotImplementedException();
        }

        public override void ChargerEnv(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void ChargerObjet(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void ChargerPersonnage(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void DeplacerPersonnage(PersonnageAbstrait unPersonnage, ZoneAbstrait zdebut, ZoneAbstrait zfin)
        {
            throw new NotImplementedException();
        }

        public override string Simuler()
        {
            throw new NotImplementedException();
        }

        public override string Statistiques()
        {
            throw new NotImplementedException();
        }
    }
}
