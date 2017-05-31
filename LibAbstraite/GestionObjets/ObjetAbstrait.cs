using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;

namespace LibAbstraite.GestionObjets
{
   public abstract class ObjetAbstrait
    {
        public string Nom
        {
            get;
            set;
        }
        public CoordonneesAbstrait position {
            get; set;
        }
       
    }
}
