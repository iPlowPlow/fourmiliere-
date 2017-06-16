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
        public Termite()
        {
            
        }

        public Termite(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionFourmilliere)
        {
            this.Nom = nom;
            this.PV = 75;
            this.Position = position;
            this.Maison = positionFourmilliere;
            ListEtape = new ObservableCollection<EtapeAbstraite>();
            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Attaque("Attaque");
        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "Ma Termite" + this.Nom;
        }

        public override void AnalyseSituation()
        {
            foreach (PersonnageAbstrait unPerso in zone.PersonnageList)
            {

                if (unPerso.GetType().Equals(typeof(Reine)))
                {
                    unPerso.PV -= 20;
                    AjouterEtape(0, "J'attaque la Reine !", this.Position.X, this.Position.Y);
                }

                else if (unPerso.GetType().Equals(typeof(Guerriere)))
                {
                    unPerso.PV -= 20;
                    AjouterEtape(0, "J'attaque la Guerriere !", this.Position.X, this.Position.Y);
                    
                }
                else if (unPerso.GetType().Equals(typeof(Ouvriere)))
                {
                    unPerso.PV -= 20;
                    AjouterEtape(0, "J'attaque l'Ouvriere !", this.Position.X, this.Position.Y); 
                }
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
