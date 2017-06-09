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
using LibAbstraite.Fabrique;
using LibMetier;
using LibAbstraite;

namespace fourmilliereALIHM
{
    public class FourmilliereModel : ViewModelBase 
    {
        public static Random Hazard = new Random();
        private Boolean encours;
        private string titre;
        private PersonnageAbstrait unPerso;
        public int tourActuel = 0;
        public int DimensionX;
        public Fourmiliere fourmilliere { get; set; }
        public int DimensionY;
        public int vitesse;
        public string TitreApplication {
            get { return titre;}
            set {
                titre = value;
                OnPropertyChanged("TitreApplication");
            } 
        }
        public ObservableCollection<Fourmis> FourmisList { get; set; }
        public ObservableCollection<Pheromone> PheromoneList { get; set; }

        public PersonnageAbstrait FourmisSelect { 
          get { return unPerso; } 
          set {
                unPerso = value;
                OnPropertyChanged("FourmisSelect");
              }
        }
        public ObservableCollection<PersonnageAbstrait> PersonnagesMortList { get; set; }

        public FabriqueAbstraite Fabrique;

        public PersonnageAbstrait PersoSelect {
            get { return unPerso; }
            set {
                unPerso = value;
                OnPropertyChanged("PersoSelect");
            }
        }

        public  FourmilliereModel()
        {
            TitreApplication = "Application FourmilliereAL";
            DimensionX = 60;
            DimensionY = 60;
            vitesse = 500;
            fourmilliere = new Fourmiliere(DimensionX, DimensionY);
            fourmilliere.PersonnageAbstraitList = new ObservableCollection<PersonnageAbstrait>();
            fourmilliere.ObjetAbstraitList = new ObservableCollection<ObjetAbstrait>();
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
        public void AjouteNourriture()
        {
            fourmilliere.ObjetAbstraitList.Add(new Nourriture(String.Concat("Nourriture N {0}", fourmilliere.ObjetAbstraitList.Count), new Coordonnees(Hazard.Next(1, DimensionX), Hazard.Next(1, DimensionY))));
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
        public void AjouterFourmis(PersonnageAbstrait f)
        {
            fourmilliere.PersonnageAbstraitList.Add(f);
        }
        public void AjouterFourmis()
        {
            fourmilliere.PersonnageAbstraitList.Add(new Ouvriere("Fourmis N°"+ fourmilliere.PersonnageAbstraitList.Count, new Coordonnees(30, 30)));
            Fabrique = new FabriqueFourmiliere();
        }

        
        public void AjouterGuerriere()
        {
            PersonnagesList.Add(Fabrique.CreerGuerriere("Guerriere " + PersonnagesList.Count));
        }

        public void AjouterReine()
        {
            PersonnagesList.Add(Fabrique.CreerReine("Reine " + PersonnagesList.Count));
        }

        public void AjouterOuvriere()
        {
            PersonnagesList.Add(Fabrique.CreerOuvriere("Ouvriere " + PersonnagesList.Count));
        }

        public void AjouterTermite()
        {
            PersonnagesList.Add(Fabrique.CreerGuerriere("Termite " + PersonnagesList.Count));
        }
        /*Suppression via L'IHM*/
        public void SupprimerPersonnage()
        {
            fourmilliere.PersonnageAbstraitList.Remove(FourmisSelect);
            //PersonnagesList.Remove(unPerso);
        }

        public void TourSuivant()
        {
            foreach (Pheromone unePheromone in fourmilliere.ObjetAbstraitList.Where(x => x.GetType().Equals(typeof(Pheromone))).ToList())
            {
                if (unePheromone.Dureevie < 1)
                {
                    fourmilliere.ObjetAbstraitList.Remove(unePheromone);
                }
            }
            foreach (ObjetAbstrait unObjet in fourmilliere.ObjetAbstraitList)
            {
                unObjet.TourPasse();
            }
            foreach(PersonnageAbstrait unInsecte in fourmilliere.PersonnageAbstraitList)
            {
                if (unInsecte.GetType().Equals(typeof(Ouvriere)) && unInsecte.TransporteNourriture == true)
                {
                    Coordonnees coordonnees = new Coordonnees(unInsecte.Position.X, unInsecte.Position.Y);
                    Pheromone unPheromone = new Pheromone("pheromone", coordonnees);
                    fourmilliere.ObjetAbstraitList.Add(unPheromone);
                }
                unInsecte.Avance1Tour(DimensionX, DimensionY);
                if (unInsecte.Pointsdevie <= 0)
                {
                    PersonnagesMortList.Add(unInsecte);
                    PersonnagesList.Remove(unInsecte);
                }
                //décommentes si tu veux que tes fourmis souillent la map avec leurs feromones
                //unInsecte.TransporteNourriture = true;
            }
            if(Hazard.Next(1,11) == 1)
            {
                AjouteNourriture();
            }
            tourActuel++;
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
