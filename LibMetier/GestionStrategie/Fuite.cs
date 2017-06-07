using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;

namespace LibMetier.GestionStrategie
{
    public class Fuite : StrategieAbstraite
    {
        public Fuite(string nom)
        {
            this.Nom = nom;
        }

        public override void Operation()
        {
            Console.WriteLine("barre toi enculé");
        }
    }
}
