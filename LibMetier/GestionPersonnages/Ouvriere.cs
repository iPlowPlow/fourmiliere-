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
    
  public class Ouvriere : PersonnageAbstrait
  {
        
        public MorceauNourriture Morceau { get; set; }
        public Ouvriere()
        {
         
        }
        public Ouvriere(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine)
        {
            this.Nom = nom;
            this.PV = 100;
            this.Position = position;

            this.Maison = positionReine;
            ListEtape = new ObservableCollection<EtapeAbstraite>();

            zone = new BoutDeTerrain("default", position);
            StategieCourante = new Normal("Normal");
        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }
        public MorceauNourriture DeposeMorceau()
        {

            AjouterEtape(0, "Je dépose de la nourriture à la reine.", Position.X, Position.Y);
            Morceau.Position = new Coordonnees(Position.X, Position.Y);
            MorceauNourriture morceauRendu = Morceau;
            Morceau = null;
            TransporteNourriture = false;
            this.StategieCourante = new Normal("Normal");
            return morceauRendu;
        }
        public override string ToString()
        {
            return " Mon " + this.Nom;
        }

        public override void AnalyseSituation()
        {
            if (zone.ObjetList.Count > 0)
            {

                /*On ne prend pas la nourriture de la maison*/
                if (zone.Position.X != this.Maison.X && zone.Position.Y != this.Maison.Y)
                {
                    if (this.TransporteNourriture == false)
                    {
                        try
                        {
                            foreach (Nourriture unObjet in zone.ObjetList.Where(x => x.GetType().Equals(typeof(Nourriture))))
                            {
                                if (unObjet.ListMorceaux.Count > 0)
                                {
                                    this.Morceau = unObjet.Recolter();
                                    AjouterEtape(0, "Je récolte un morceau de nourriture ! ", Position.X, Position.Y);
                                    this.StategieCourante = new Retour("Retour");
                                    this.TransporteNourriture = true;
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            e.ToString();
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
                        AjouterEtape(0, "Une termite m'attaque ! ", this.Position.X, this.Position.Y);
                    }

                }

            }
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
            else if(etat =="soleil" ) 
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
    }
}
