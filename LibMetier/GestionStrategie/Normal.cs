using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;

namespace LibMetier.GestionStrategie
{
    public class Normal : StrategieAbstraite
    {
        public Normal(string nom)
        {
            this.Nom = nom;
        }

        public override void Operation()
        {
            Console.WriteLine("il fait beau B474RD");
        }
    }
}
