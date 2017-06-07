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

namespace LibMetier.GestionEnvironnements
{

    public class Fourmiliere : EnvironnementAbstrait
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

            ObjetList = new ObservableCollection<ObjetAbstrait>();
            ZoneList = new ObservableCollection<ZoneAbstrait>();
            InitZones();

        }
        

        public override void AjouterGuerriere()
        {
            PersonnagesList.Add(Fabrique.CreerGuerriere("Guerriere " + PersonnagesList.Count));
        }
        public override void AjouterReine()
        {
            PersonnagesList.Add(Fabrique.CreerReine("Reine " + PersonnagesList.Count));
        }
        public override void AjouterOuvriere()
        {
            PersonnagesList.Add(Fabrique.CreerOuvriere("Ouvriere " + PersonnagesList.Count));
        }
        public override void AjouterTermite()
        {
            PersonnagesList.Add(Fabrique.CreerTermite("Termite " + PersonnagesList.Count));
        }
        public void AjouteNourriture()
        {
            ObjetList.Add(new Nourriture(String.Format("Nourriture N {0}", ObjetList.Count), new Coordonnees(Hazard.Next(1, DimensionX), Hazard.Next(1, DimensionY))));
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
                boutDeTerrain.PersonnageList.AddRange(PersonnagesList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())/*&& x.GetType().Equals(typeof(Guerriere))*/));
                boutDeTerrain.ObjetList.AddRange(ObjetList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())));
                foreach(PersonnageAbstrait unPerso in PersonnagesList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())))
                {
                    unPerso.zone = boutDeTerrain;
                }

            }
        }

        public override void FournirAcces()
        {
            AccesAbstrait nouvelaccès;
            List<ZoneAbstrait> zoneAdjacentes = new List<ZoneAbstrait>();
            foreach(var unInsecte in PersonnagesList)
            {
                zoneAdjacentes.Clear();
                zoneAdjacentes.AddRange(ZoneList.Where(x => x.Position.X > (unInsecte.Position.X - 2) && x.Position.X < (unInsecte.Position.X - 2) && x.Position.Y > (unInsecte.Position.Y - 2) && x.Position.Y < (unInsecte.Position.Y - 2)));
                nouvelaccès = Fabrique.CreerAcces(zoneAdjacentes);
                unInsecte.ChoixZoneSuivante = nouvelaccès;
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
            Repositioner();
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
                //unInsecte.TransporteNourriture = true;
            }
            if (Hazard.Next(1, 11) == 1)
            {
                AjouteNourriture();
            }

            
            tourActuel++;
        }

        public override void InitZones()
        {
            for (int i = 0; i < DimensionX; i++)
            {
                for (int j = 0; j < DimensionY; j++)
                {
                    ZoneList.Add(Fabrique.CreerZone(String.Format("Zone en X={0}, Y={1}", i, j), Fabrique.CreerPosition(i, j)));
                }
            }
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
    }
}
