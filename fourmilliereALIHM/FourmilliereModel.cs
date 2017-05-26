using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
namespace fourmilliereALIHM
{
    public class FourmilliereModel : ViewModelBase
    {
        private Boolean encours;
        private string titre;
        private Fourmis fourmisse;
        public int DimensionX;
        public int DimensionY;
        public int vitesse;
        public string TitreApplication { get { return titre;} set {
                titre = value;
                OnPropertyChanged("TitreApplication");
            } }
        public ObservableCollection<Fourmis> FourmisList { get; set; }
        public Fourmis FourmisSelect { get { return fourmisse; } set {
                fourmisse = value;
                OnPropertyChanged("FourmisSelect");
            } }
        public  FourmilliereModel()
        {
            TitreApplication = "Application FourmilliereAL";
            DimensionX = 20;
            DimensionY = 30;
            vitesse = 500;
            FourmisList = new ObservableCollection<Fourmis>();
            FourmisList.Add(new Fourmis("Alain"));
            FourmisList.Add(new Fourmis("Cecile"));
            FourmisList.Add(new Fourmis("Pierre"));
            FourmisList.Add(new Fourmis("Denis"));
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
            foreach( Fourmis uneFourmi in FourmisList)
            {
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
