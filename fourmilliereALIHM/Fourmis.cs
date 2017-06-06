using System;
using System.Collections.ObjectModel;

namespace fourmilliereALIHM
{
    public class Fourmis
    {
        /*static Random Hazard = new Random();
        public int X { get; set; }
        public int Y { get; set; }
        public string Nom { get; set; }
        public ObservableCollection<Etape> ListEtape { get; set; }
        public Fourmis(string v)
        {
            X = 10;
            Y = 10;
            this.Nom = v;
            ListEtape = new ObservableCollection<Etape>();
            int nbEtapes = Hazard.Next(10);
            for(int i = 0; i < nbEtapes; i++)
            {
                ListEtape.Add(new Etape());
            }
        }
        public override string ToString()
        {
            return "Ma fourmis" + this.Nom;
        }
        public void Avance1Tour(int dimX,int dimY)
        {
            AvanceAuHazard(dimX, dimY);
            ListEtape.Add(new Etape());
        }
        public void AvanceAuHazard(int dimX, int dimY)
        {
            int newX = X + Hazard.Next(3) - 1;
            int newY = Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX)) X = newX;
            if ((newY >= 0) && (newY < dimX)) Y = newY;
        }
        public void RameneNourriture()
        {

        }
    }
}