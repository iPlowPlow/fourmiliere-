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

using System.Xml.Serialization;
using System.Threading;
using LibMetier.GestionPersonnages;
using LibMetier.GestionObjets;
using LibAbstraite;
namespace LibMetier.GestionEnvironnements
{

    public class Fourmiliere : EnvironnementAbstrait,SujetAbstrait
    {
        public Fourmiliere()
        {

        }

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
            meteo = new Meteo();
            ObjetList = new ObservableCollection<ObjetAbstrait>();

        }
        //après le déplacement du personnage, ajoute les 4 zones adjacentes à la zone du personnage dans les zones accessibles du
        //personnage
        public override void AjouteChemin(PersonnageAbstrait unpersonnage)
        {
            unpersonnage.ChoixZoneSuivante.Zonesaccessibles.Clear();
            CoordonneesAbstrait positionPersonnage = unpersonnage.Position;
            for (int i = -1; i<=1; i+=2)
            {
                for(int j = -1; j <= 1; j += 2)
                {
                    try
                    {
                        unpersonnage.ChoixZoneSuivante.Zonesaccessibles.Add(ZoneList
                            .Single(z => z.Position.X == positionPersonnage.X + i && z.Position.Y == positionPersonnage.X + j));
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }


        public void AjouteNourriture()
        {
            ObjetList.Add(new Nourriture(String.Concat("Nourriture N {0}", ObjetList.Count), new Coordonnees(Hazard.Next(1, DimensionX), Hazard.Next(1, DimensionY))));
        }

        public override void AjouteOeuf(ObjetAbstrait unOeuf)
        {
            this.ObjetList.Add(unOeuf);
        }

        public override void AjoutePheromone(ObjetAbstrait unPheromone)
        {
            this.ObjetList.Add(unPheromone);
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

                boutDeTerrain.PersonnageList.Clear();
                boutDeTerrain.ObjetList.Clear();
                boutDeTerrain.PersonnageList.AddRange(PersonnagesList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())));
                boutDeTerrain.ObjetList.AddRange(ObjetList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())));

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

        public void AjouterReine(Reine reine)
        {
            throw new NotImplementedException();
        }

        public override void TourSuivant()
        {
            foreach (Pheromone unePheromone in ObjetList.Where(x => x.GetType().Equals(typeof(Pheromone))).ToList())
            {
                if (unePheromone.Dureevie < 1)
                {
                    ObjetList.Remove(unePheromone);
                }
            }
            foreach (ObjetAbstrait unObjet in ObjetList)
            {
                unObjet.TourPasse();
            }
            foreach (PersonnageAbstrait unInsecte in PersonnagesList.ToList())
            {
                if (unInsecte.GetType().Equals(typeof(Ouvriere)) && unInsecte.TransporteNourriture == true)
                {
                    Coordonnees coordonnees = new Coordonnees(unInsecte.Position.X, unInsecte.Position.Y);
                    Pheromone unPheromone = new Pheromone("pheromone", coordonnees);
                    ObjetList.Add(unPheromone);
                }
                unInsecte.Avance1Tour(DimensionX, DimensionY, tourActuel);
                if (unInsecte.PV <= 0)
                {
                    PersonnagesMortList.Add(unInsecte);
                    PersonnagesList.Remove(unInsecte);
                }
                //décommentes si tu veux que tes fourmis souillent la map avec leurs feromones
                unInsecte.TransporteNourriture = true;
            }
            if (Hazard.Next(1, 11) == 1)
            {
                AjouteNourriture();
            }
            tourActuel++;
        }




        public override void Avance()
        {
            encours = true;
            while (encours == true)
            {
                Thread.Sleep(vitesse);
                TourSuivant();

            }
        }
        public override void Stop()
        {
            encours = false;
        }

        public override void AjouteNourriture(ObjetAbstrait unObject)
        {
            throw new NotImplementedException();
        }

        public  void Attach(PersonnageAbstrait observer)
        {
            ListObservateur.Add(observer);
        }

        public  void Detach(PersonnageAbstrait observer)
        {
            ListObservateur.Remove(observer);
        }

        public  void Notify()
        {
            foreach (PersonnageAbstrait unInsecte in ListObservateur)
            {
                unInsecte.maj();
            }
        }
    }
}
