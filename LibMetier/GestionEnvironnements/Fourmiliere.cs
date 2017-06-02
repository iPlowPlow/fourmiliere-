using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.Fabrique;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System.Collections.ObjectModel;
using System.Threading;

namespace LibMetier.GestionEnvironnements
{
    public class Fourmiliere : EnvironnementAbstrait
    {

      


        public Fourmiliere(int _dimensionX, int _dimensionY)
        {
            TitreApplication = "Application FourmilliereAL";
            vitesse = 500;

            this.DimensionX = _dimensionX;
            this.DimensionY = _dimensionY;

            
            Fabrique = new FabriqueFourmiliere();
            PersonnagesList = new ObservableCollection<PersonnageAbstrait>();
            PersonnagesMortList = new ObservableCollection<PersonnageAbstrait>();
            PersonnagesList.Add(Fabrique.CreerGuerriere("Alain"));
            PersonnagesList.Add(Fabrique.CreerOuvriere("Cecile"));
            PersonnagesList.Add(Fabrique.CreerTermite("Pierre"));

        }

        public override void AjouteChemin(FabriqueAbstraite fan, params AccesAbstrait[] accesArray)
        {
            throw new NotImplementedException();
        }

       

        public override void AjouteNourriture(ObjetAbstrait unObject)
        {
            this.ObjetList.Add(unObject);
        }

        public override void AjouteOeuf(ObjetAbstrait unObject)
        {
            this.ObjetList.Add(unObject);
        }

        public override void AjoutePheromone(ObjetAbstrait unObject)
        {
            throw new NotImplementedException();
        }

        public override void AjouteZone(params ZoneAbstrait[] zoneArray)
        {
            
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

        public override void Repositioner()
        {
            foreach(var boutDeTerrain in ZoneList)
            {
                boutDeTerrain.OuvriereList.AddRange(PersonnagesList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                boutDeTerrain.GuerrierList.AddRange(PersonnagesList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                boutDeTerrain.ReineList.AddRange(PersonnagesList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                boutDeTerrain.TermiteList.AddRange(PersonnagesList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                //boutDeTerrain.ObjetList.AddRange(ObjetList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
            }
        }

        public override string Simuler()
        {
            throw new NotImplementedException();
        }

        public override string Statistiques()
        {
            throw new NotImplementedException();
        }

        public void SupprimerPersonnage()
        {
            PersonnagesList.Remove(PersoSelect);
        }

        public void TourSuivant()
        {
            tourActuel++;
            for (int i = PersonnagesList.Count - 1; i >= 0; i--)
            {
                PersonnagesList[i].Avance1Tour(DimensionX, DimensionY, tourActuel);
                if (PersonnagesList[i].PV <= 0)
                {
                    PersonnagesMortList.Add(PersonnagesList[i]);
                    PersonnagesList.Remove(PersonnagesList[i]);
                }

            }
        }

        public void Avance()
        {
            encours = true;
            while (encours == true)
            {
                Thread.Sleep(vitesse);
                TourSuivant();

            }
        }
        public void Stop()
        {
            encours = false;
        }





    }
}
