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
        public Ouvriere(string nom, CoordonneesAbstrait position, CoordonneesAbstrait positionReine)
        {
            this.Nom = nom;
            this.PV = 100;
            this.Position = position;
            this.Maison = positionReine;
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
            AjouterEtape(0, "Je dépose de la nourriture à la reine.");
            Morceau.Position = new Coordonnees(Position.X, Position.Y);
            MorceauNourriture morceauRendu = Morceau;
            Morceau = null;
            TransporteNourriture = false;
            this.StategieCourante = new Normal("Normal");
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
                    if (this.TransporteNourriture == false)
                    {
                        try
                        {
                            foreach (Nourriture unObjet in zone.ObjetList.Where(x => x.GetType().Equals(typeof(Nourriture))))
                            {
                                if (unObjet.ListMorceaux.Count > 0)
                                {
                                    this.Morceau = unObjet.Recolter();
                                    AjouterEtape(0, "Je récolte un morceau de nourriture ! ");
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
                        AjouterEtape(0, "Une termite m'attaque ! ");
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
    }
}
