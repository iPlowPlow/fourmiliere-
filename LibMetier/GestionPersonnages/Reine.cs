using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibAbstraite.GestionObjets;
using LibMetier.GestionEnvironnements;
using LibMetier.GestionObjets;
using LibMetier.GestionStrategie;
using System.Windows.Threading;

namespace LibMetier.GestionPersonnages
{
    public class Reine : PersonnageAbstrait 
    {
        private static Reine instance;
        private Oeuf oeufPondu;
        public List<ObjetAbstrait> morceaux;
        public Reine()
        {
         

        }

        public static Reine Instance()
        {

            if (instance == null)
            {
                instance = new Reine();
            }
            return instance;
        }
        public static Reine Instance(string nom, CoordonneesAbstrait position)
        {
            if (instance == null)
            {
                instance = new Reine();
            }
            instance.Nom = nom;
            instance.PV = 250;
            instance.Position = position;
            instance.Maison = new Coordonnees();
            instance.Maison.X = position.X;
            instance.Maison.Y = position.Y;
            instance.ListEtape = new ObservableCollection<EtapeAbstraite>();
            instance.zone = new BoutDeTerrain("default", position);
            instance.StategieCourante = new Immobile("Immobile");
            instance.morceaux = new List<ObjetAbstrait>();
            return instance;
        }
        public static Reine Instance(Reine reine)
        {
            if (instance == null)
            {
                instance = new Reine();
            }
            instance.PV = 250;
            instance.Position = new Coordonnees();
            instance.Position.X = reine.Position.X;
            instance.Position.Y = reine.Position.Y;
            instance.Maison = new Coordonnees();
            instance.Maison.X = reine.Position.X;
            instance.Maison.Y = reine.Position.Y;
            instance.ListEtape = reine.ListEtape;
            instance.zone = reine.zone;
            instance.StategieCourante = new Immobile("Immobile");
            return instance;
        }
        public static Reine RemplacerReine(Princesse princesse)
        {
            if (instance == null)
            {
                instance = new Reine();
            }
            instance.PV = 250;
            instance.Position = new Coordonnees();
            instance.Position.X = princesse.Position.X;
            instance.Position.Y = princesse.Position.Y;
            instance.Maison = new Coordonnees();
            instance.Maison.X = princesse.Position.X;
            instance.Maison.Y = princesse.Position.Y;
            instance.ListEtape = princesse.ListEtape;
            instance.zone = princesse.zone;
            instance.StategieCourante = new Immobile("Immobile");
            return instance;
        }
        public Oeuf OeufPondu { 
            get
            {
                if (instance == null)
                {
                    return null;
                }
                return instance.oeufPondu;
            }
            set
            {
                if (instance != null)
                {
                    instance.oeufPondu = value;
                }
            }

        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }

        public override void AnalyseSituation()
        {
            try
            {
                List<ObjetAbstrait> morceaux = zone.ObjetList.Where(x => x.GetType().Equals(typeof(MorceauNourriture))).ToList();
                if (morceaux.Count > 0)
                {
                    PondreOeuf();
                }
                foreach (PersonnageAbstrait unPerso in zone.PersonnageList)
                {


                    if (unPerso.GetType().Equals(typeof(Guerriere)))
                    {
                        /*Indiquer chemin nourriture*/
                    }
                    else if (unPerso.GetType().Equals(typeof(Termite)))
                    {
                        AjouterEtape(0, "Une termite m'attaque ! ", Position.X, Position.Y);
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public override string ToString()
        {
            return "Ma Reine" + instance.Nom;
        }
        public void PondreOeuf()
        {
            FabriqueFourmiliere fabrique = new FabriqueFourmiliere();
            CoordonneesAbstrait pos = new Coordonnees();
            pos.X = Position.X + Fourmiliere.Hazard.Next(-1, 2);
            pos.Y = Position.Y + Fourmiliere.Hazard.Next(-1, 2);
            oeufPondu =(Oeuf)fabrique.CreerOeuf(String.Format("Oeuf N° {0}", zone.ObjetList.Where(x=>x.GetType().Equals(typeof(Oeuf))).Count()), pos);
            AjouterEtape(0 ,String.Format("Je pond un oeuf en X={0}, Y={1}", pos.X, pos.Y), Position.X, Position.Y);
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
