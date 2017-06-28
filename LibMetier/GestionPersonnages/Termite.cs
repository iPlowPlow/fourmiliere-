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
    public class Termite : PersonnageAbstrait
    {
        public static int HPNaissance = 100;
        public Termite()
        {
            
        }

        public Termite(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionFourmilliere)
        {
            this.Nom = nom;
            this.PV = HPNaissance;
            this.Position = position;
            this.Maison = positionFourmilliere;
            ListEtape = new ObservableCollection<EtapeAbstraite>();
            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Attaque("Attaque");
        }

        public override string ToString()
        {
            return " La " + this.Nom;
        }

        public override void AnalyseSituation()
        {

            PersonnageAbstrait persoInteraction = null;
            foreach (PersonnageAbstrait unPerso in zone.PersonnageList)
            {

                if (unPerso.GetType().Equals(typeof(Reine)))
                {
                    persoInteraction = unPerso;
                    break;
                   /* unPerso.PV -= 20;
                    AjouterEtape(0, "J'attaque la Guerriere !", this.Position.X, this.Position.Y);*/
                }

                else if (unPerso.GetType().Equals(typeof(Guerriere)) && persoInteraction == null)
                {
                    persoInteraction = unPerso;
                }
                else if ((unPerso.GetType().Equals(typeof(Ouvriere)) || unPerso.GetType().Equals(typeof(Princesse))) && persoInteraction == null)
                {
                    persoInteraction = unPerso;
                }
            }
            if (persoInteraction != null) {
                persoInteraction.PV -= 20;
                AjouterEtape(0, "J'attaque une " + persoInteraction.GetType().Name + " !", this.Position.X, this.Position.Y);
            }
           

        }

        public override void maj(string etat)
        {
           
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
    }
}
