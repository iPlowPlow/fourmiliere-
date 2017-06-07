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
        public abstract void Operation();
    }
}
