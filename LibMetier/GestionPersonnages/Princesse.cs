using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;
using LibMetier.GestionObjets;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibMetier.GestionEnvironnements;
using LibAbstraite.GestionStrategie;
using LibMetier.GestionStrategie;
using LibAbstraite.GestionObjets;
using System.Windows.Threading;

namespace LibMetier.GestionPersonnages
{

    public class Princesse : PersonnageAbstrait
    {
        public static int HPNaissance = 175;
        public Princesse()
        {

        }
        public Princesse(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine)
        {
            this.Nom = nom;
            this.PV = HPNaissance;
            this.Position = position;
            this.Maison = positionReine;
            ListEtape = new ObservableCollection<EtapeAbstraite>();
            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Normal("Normal");
        }

        public override void AnalyseSituation()
        {

        }
        public override void maj(string etat)
        {

            if (TransporteNourriture == true)
            {
                //AnalyseSituation();
                this.EtatMeteoObserver = etat;
                if (!this.StategieCourante.GetType().Equals(typeof(Retour)))
                {
                    this.StategieCourante = new Retour("Retour");
                }

            }
            else if (etat == "attaque")
            {
                this.EtatFourmiliereObserver = etat;
                if (!this.StategieCourante.GetType().Equals(typeof(Retour)))
                {
                    this.StategieCourante = new Retour("fuite");

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
                    this.StategieCourante = new Normal("Normal");
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

                      if (tourActuel == 0)
                      {
                          tourActuel = this.ListEtape[ListEtape.Count - 1].tour;
                      }

                      ListEtape.Add(new Etape(tourActuel, description, X, Y));
                  }
              );
        }
        public override string ToString()
        {
            return " Ma " + this.Nom;
        }
    }
}
