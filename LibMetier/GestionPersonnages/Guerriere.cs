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
using System.Windows.Threading;

namespace LibMetier.GestionPersonnages
{
    public class Guerriere : PersonnageAbstrait
    {

        public Guerriere()
        {
         
        }
        public Guerriere(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine)
        {
            this.Nom = nom;
            this.PV = 75;
            this.Position = position;

            this.Maison = positionReine;
            ListEtape = new ObservableCollection<EtapeAbstraite>();

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
                        AjouterEtape(0, "J'attaque une termite ! ", this.Position.X, this.Position.Y);
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
        public override void maj(string etat)
        {

            if (etat == "attaque")
            {
                this.EtatFourmiliereObserver = etat;
                if (!this.StategieCourante.GetType().Equals(typeof(Retour)))
                {
                    this.StategieCourante = new Defense("Defense");

                }

            }
            else if (etat == "pluie")
            {
                this.EtatMeteoObserver = etat;
                if (!this.StategieCourante.GetType().Equals(typeof(Retour)))
                {
                    this.StategieCourante = new Retour("fuite");
                }

            }
            else if (etat == "soleil")
            {
                this.EtatMeteoObserver = etat;
                if (!this.StategieCourante.GetType().Equals(typeof(Normal)))
                {
                    this.StategieCourante = new Defense("defense");
                }

            }
            else if (etat == "brouillard")
            {
                this.EtatMeteoObserver = etat;
                if (!this.StategieCourante.GetType().Equals(typeof(Immobile)))
                {
                    this.StategieCourante = new Immobile("Immobile");
                }
            }

           
        }


        public override void AjouterEtape(int tourActuel, string description, int X, int Y)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                   DispatcherPriority.Normal,
                   (Action)delegate ()
                   {
                       
                       if(tourActuel == 0)
                       {
                           tourActuel = this.ListEtape[ListEtape.Count - 1].tour;
                       }

                        ListEtape.Add(new Etape(tourActuel, description, X, Y));
                   }
               );
        }

    }
}
