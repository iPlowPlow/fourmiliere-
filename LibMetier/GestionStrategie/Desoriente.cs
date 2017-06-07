using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;

namespace LibMetier.GestionStrategie
{
    public class Desoriente : StrategieAbstraite
    {
        public Desoriente(string nom)
        {
            this.Nom = nom;
        }

        public override void Operation()
        {
            Console.WriteLine("on voit quedal pt1");
        }
    }
}
