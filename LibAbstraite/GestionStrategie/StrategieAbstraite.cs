using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionStrategie
{
    public abstract class StrategieAbstraite
    {
        public string Nom { get; set; }
        public static Random Hazard = new Random();
        public abstract void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso);
    }
}
