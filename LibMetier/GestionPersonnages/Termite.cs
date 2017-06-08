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
    public class Termite : PersonnageAbstrait
    {

        public Termite(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.PV = 75;
            this.Position = position;
            
            ListEtape = new ObservableCollection<Etape>();
            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Normal("Strategie normal");
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

                if (unPerso.GetType().Equals(typeof(Guerriere)))
                {
                    unPerso.PV -= 20;
                    ListEtape.Add(new Etape("J'attaque la Guerriere! "));
                }
                else if (unPerso.GetType().Equals(typeof(Reine)))
                {
                    unPerso.PV -= 20;
                    ListEtape.Add(new Etape("J'attaque laReine! "));
                }
                else if (unPerso.GetType().Equals(typeof(Ouvriere)))
                {
                    unPerso.PV -= 20;
                    ListEtape.Add(new Etape("J'attaque l'Ouvriere! "));
                }
                

            }
        }
    }
}
