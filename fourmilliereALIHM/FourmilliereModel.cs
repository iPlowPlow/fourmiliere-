using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.Fabrique;
using LibMetier;
using LibAbstraite;

namespace fourmilliereALIHM
{
    public class FourmilliereModel : ViewModelBase
    {
        private Boolean encours;
        private string titre;
        private PersonnageAbstrait unPerso;
        public int tourActuel = 0;
        public int DimensionX;
        public int DimensionY;
        public int vitesse;
        public string TitreApplication {
            get { return titre;}
            set {
                titre = value;
                OnPropertyChanged("TitreApplication");
            }
        }
        public ObservableCollection<PersonnageAbstrait> PersonnagesList { get; set; }
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
            DimensionX = 20;
            DimensionY = 30;
            vitesse = 500;
            Fabrique = new FabriqueFourmiliere();
            PersonnagesList = new ObservableCollection<PersonnageAbstrait>();
            PersonnagesMortList = new ObservableCollection<PersonnageAbstrait>();
            PersonnagesList.Add(Fabrique.CreerGuerriere("Alain"));
            PersonnagesList.Add(Fabrique.CreerOuvriere("Cecile"));
            PersonnagesList.Add(Fabrique.CreerTermite("Pierre"));
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
            PersonnagesList.Remove(unPerso);
        }

        public void TourSuivant()
        {
            tourActuel++;
            for (int i = PersonnagesList.Count -1; i>=0; i--)
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
