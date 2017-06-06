using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibMetier.GestionEnvironnements;

namespace LibMetier.GestionPersonnages
{
    public class Guerriere : PersonnageAbstrait
    {

        public Guerriere()
        {
         
        }
        public Guerriere(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.PV = 75;
            this.Position = position;
            ListEtape = new ObservableCollection<Etape>();
            zone = new BoutDeTerrain("default");
        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Ma Guerriere" + this.Nom;
        }

        public override void AnalyseSituation()
        {

            //Console.WriteLine("ma zone : " + zone.Position.X);

            /*Si termite sur sa case l'attaquer*/
           /* if (zone.TermiteList.Count > 0)
            {

            }*/
            
        }

    }
}
