using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionObjets
{
   public abstract class ObjetAbstrait
    {
        public string Nom
        {
            get;
            set;
        }
        public int position {
            get;
            set;
       }
        public ObjetAbstrait(string Nom)
        {
            this.Nom = Nom;
        }
    }
}
