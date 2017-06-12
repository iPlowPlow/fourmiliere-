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

namespace LibMetier.GestionPersonnages
{
    
  public class Ouvriere : PersonnageAbstrait
   {
        
        public MorceauNourriture Morceau { get; set; }
        public Ouvriere()
        {
         
        }
        public Ouvriere(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.PV = 100;
            this.Position = position;
            this.Maison = new Coordonnees();
            this.Maison.X = position.X;
            this.Maison.Y = position.Y;
            ListEtape = new ObservableCollection<Etape>();
            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Normal("Normal");
        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }
        public MorceauNourriture DeposeMorceau()
        {
            MorceauNourriture morceauRendu = Morceau;
            Morceau = null;
            return morceauRendu;
        }
        public override string ToString()
        {
            return "Mon Ouvriere" + this.Nom;
        }

        public override void AnalyseSituation()
        {

            

            if (zone.ObjetList.Count > 0)
            {

                /*On ne prend pas la nourriture de la maison*/
                if (zone.Position.X != this.Maison.X && zone.Position.Y != this.Maison.Y)
                {
                    foreach (Nourriture unObjet in zone.ObjetList.Where(x => x.GetType().Equals(typeof(Nourriture))))
                    {
                        if (unObjet.ListMorceaux.Count > 0) {
                            this.Morceau = unObjet.Recolter();
                            ListEtape.Add(new Etape("Je récolte un morceau de nourriture ! "));
                            this.StategieCourante = new Retour("Retour");
                            this.TransporteNourriture = true;
                        }   
                    }
                }
            }

            if (zone.PersonnageList.Count > 0)
            {
                foreach (PersonnageAbstrait unPerso in zone.PersonnageList)
                {

                    if (unPerso.GetType().Equals(typeof(Guerriere)))
                    {
                        /*Indiquer chemin nourriture*/
                    }
                    else if (unPerso.GetType().Equals(typeof(Termite)))
                    {
                        ListEtape.Add(new Etape("Une termite m'attaque ! "));
                    }
                    else if (this.TransporteNourriture == true && unPerso.GetType().Equals(typeof(Reine)))
                    {
                        zone.ObjetList.Add(this.Morceau);
                        this.TransporteNourriture = false;
                        this.Morceau = null;
                        this.StategieCourante = new Normal("Normal");
                        
                    }

                }

            }

        }
    }
}
