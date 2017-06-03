using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionObjets;
using LibMetier.GestionPersonnages;
using LibMetier.GestionEnvironnements;
using LibMetier.GestionObjets;

namespace fourmilliereALIHM
{
    public class FourmilliereModel : ViewModelBase 
    {
        private Boolean encours;
        private string titre;
        private Fourmis fourmisse;
        public int DimensionX;
        public Fourmiliere fourmilliere { get; set; }
        public int DimensionY;
        public int vitesse;
        public string TitreApplication { get { return titre;} set {
                titre = value;
                OnPropertyChanged("TitreApplication");
            } }
        public ObservableCollection<Fourmis> FourmisList { get; set; }
        public ObservableCollection<Pheromone> PheromoneList { get; set; }

        public Fourmis FourmisSelect { get { return fourmisse; } set {
                fourmisse = value;
                OnPropertyChanged("FourmisSelect");
            } }
        public  FourmilliereModel()
        {
            TitreApplication = "Application FourmilliereAL";
            DimensionX = 60;
            DimensionY = 60;
            vitesse = 500;
            fourmilliere = new Fourmiliere(DimensionX, DimensionY);
            fourmilliere.PersonnageAbstraitList = new ObservableCollection<PersonnageAbstrait>();

            fourmilliere.ObjetAbstraitList = new List<ObjetAbstrait>();
            FourmisList = new ObservableCollection<Fourmis>();
            FourmisList.Add(new Fourmis("bob"));
        }
        public void AjouteOeuf(List<Oeuf> o)
        {
            foreach (var oeuf in o)
            {
                fourmilliere.ObjetAbstraitList.Add(oeuf);
            }
        }
        public void AjouteNourriture(List<Nourriture> o)
        {
            foreach (var n in o)
            {
                fourmilliere.ObjetAbstraitList.Add(n);
            }
        }
        public void AjoutePheromone(List<Pheromone> o)
        {
            foreach (var n in o)
            {
                fourmilliere.ObjetAbstraitList.Add(n);
            }
        }
        public void AjouteOuvriere(List<Ouvriere> f)
        {
           foreach(var fourmis in f)
            {
                    fourmilliere.PersonnageAbstraitList.Add(fourmis);
            }
        }
        public void AjouteGuerriere(List<Guerriere> f)
        {
            foreach (var fourmis in f)
            {
                fourmilliere.PersonnageAbstraitList.Add(fourmis);
            }
        }
        public void AjouteReine(Reine f)
        {


            fourmilliere.PersonnageAbstraitList.Add(f);
            
        }
        public void AjouterFourmis(Fourmis f)
        {
            FourmisList.Add(f);
        }
        public void AjouterFourmis()
        {
            FourmisList.Add(new Fourmis("Fourmis N°"+ FourmisList.Count));
        }
        public void SupprimerFourmis()
        {
            FourmisList.Remove(FourmisSelect);
        }
        public void TourSuivant()
        {
            foreach (Pheromone unePheromone in PheromoneList.ToList())
            {
                if (unePheromone.dureevie < 1)
                {
                    PheromoneList.Remove(unePheromone);
                }
            }
            foreach (Pheromone unePheromone in PheromoneList)
            {
                unePheromone.TourPasse();
            }
            foreach(PersonnageAbstrait uneFourmi in fourmilliere.PersonnageAbstraitList)
            {
                Coordonnees coordonnees = new Coordonnees(uneFourmi.X, uneFourmi.Y);
                Pheromone unPheromone = new Pheromone("pheromone de fourmi", coordonnees);
                App.fourmilliereVM.PheromoneList.Add(unPheromone);
                uneFourmi.Avance1Tour(DimensionX, DimensionY);
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
