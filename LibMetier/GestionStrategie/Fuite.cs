using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;

namespace LibMetier.GestionStrategie
{
    public class Fuite : StrategieAbstraite
    {
        public Fuite(string nom)
        {
            this.Nom = nom;
        }

        public override void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso)
        {
            int newX = unPerso.Position.X + Hazard.Next(3) - 1;
            int newY = unPerso.Position.Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX)) unPerso.Position.X = newX;
            if ((newY >= 0) && (newY < dimX)) unPerso.Position.Y = newY;


        }
    }
}
