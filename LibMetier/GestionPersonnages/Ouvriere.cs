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
        public Ouvriere(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.PV = 100;
            this.Position = position;
            this.Maison = new Coordonnees();
            this.Maison.X = position.X;
            this.Maison.Y = position.Y;
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
            
            Morceau.Position = Position;
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
                Console.WriteLine("X : " + zone.Position.X + " y : " + zone.Position.Y);
                /*On ne prend pas la nourriture de la maison*/
                if (zone.Position.X != this.Maison.X && zone.Position.Y != this.Maison.Y)
                {
                    if (zone.ObjetList.Count > 0)
                    {
                        foreach (Nourriture unObjet in zone.ObjetList.Where(x => x.GetType().Equals(typeof(Nourriture))))
                        {
                            if (unObjet.ListMorceaux.Count > 0)
                            {
                                this.Morceau = unObjet.Recolter();
                                AjouterEtape(0, "Je récolte un morceau de nourriture ! ", this.Position.X, this.Position.Y);
                                this.StategieCourante = new Retour("Retour");
                                this.TransporteNourriture = true;
                            }
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
                    else if (this.TransporteNourriture == true && unPerso.GetType().Equals(typeof(Reine)))
                    {
                        AjouterEtape(0, "Je dépose de la nourriture à la reine.", this.Position.X, this.Position.Y);
                        zone.ObjetList.Add(this.Morceau);
                        this.TransporteNourriture = false;
                        this.Morceau = null;
                        this.StategieCourante = new Normal("Normal");   
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
