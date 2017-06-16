using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite;
using LibAbstraite.GestionPersonnage;
namespace LibMetier.GestionEnvironnements
{
    public class Meteo : SujetAbstrait
    {
        public List<PersonnageAbstrait> ListObservateur { get; set; }
        
        public string Etat { get; set; }
        public  void Attach(PersonnageAbstrait observer)
        {
            ListObservateur.Add(observer);
        }

        public  void Detach(PersonnageAbstrait observer)
        {
            ListObservateur.Remove(observer);
        }

        public  void Notify()
        {
            foreach(PersonnageAbstrait unInsecte in ListObservateur)
            {
                unInsecte.maj(this.Etat);
            }
        }
    }
}
