using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibMetier.GestionEnvironnements;
using LibMetier.GestionStrategie;

namespace LibMetier.GestionPersonnages
{
  public  class Reine : PersonnageAbstrait 
    {
        public Reine()
        {
           
        }

        public Reine(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.PV = 500;
            this.Position = position;
            this.Maison = new Coordonnees();
            this.Maison.X = position.X;
            this.Maison.Y = position.Y;
            ListEtape = new ObservableCollection<Etape>();
            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Immobile("Immobile");
        }




        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }

        public override void AnalyseSituation()
        {
            foreach (PersonnageAbstrait unPerso in zone.PersonnageList)
            {

                if (unPerso.GetType().Equals(typeof(Guerriere)))
                {
                    /*Indiquer chemin nourriture*/
                }
                else if (unPerso.GetType().Equals(typeof(Termite)))
                {
                    AjouterEtape(0, "Une termite m'attaque ! ");
                }

            }
        }


        public override string ToString()
        {
            return "Ma Reine" + this.Nom;
        }
    }
}
