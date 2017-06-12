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
using LibMetier.GestionStrategie;

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
            this.Maison = new Coordonnees();
            this.Maison.X = position.X;
            this.Maison.Y = position.Y;
            ListEtape = new ObservableCollection<Etape>();
            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Defense("Defense");

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
            /*Si termite sur sa case l'attaquer*/
            if (zone.PersonnageList.Count > 0)
            {
                foreach(PersonnageAbstrait unPerso in zone.PersonnageList)
                {
                    if (unPerso.GetType().Equals(typeof(Termite)))
                    {
                        unPerso.PV -= 10;
                        AjouterEtape(0, "J'attaque une termite ! ");
                    }
                    else if (unPerso.GetType().Equals(typeof(Reine)))
                    {

                    }
                    else if (unPerso.GetType().Equals(typeof(Ouvriere))){
                        /*Indiquer chemin nourriture*/
                    }
                    

                }
                
            }
            
        }

    }
}
