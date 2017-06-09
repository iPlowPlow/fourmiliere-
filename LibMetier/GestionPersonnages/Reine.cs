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

namespace LibMetier.GestionPersonnages
{
    public class Reine : PersonnageAbstrait 
    {
        private static Reine instance;
        private Oeuf oeufPondu;

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
            instance.PV = 500;
            instance.Position = position;
            instance.ListEtape = new ObservableCollection<Etape>();

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
            List<ObjetAbstrait> morceaux = zone.ObjetList.Where(x => x.GetType().Equals(typeof(MorceauNourriture))).ToList();
            if (morceaux.Count > 0)
            {
                zone.ObjetList.Remove(morceaux[0]);
                PondreOeuf();
            }
        }

        public override string ToString()
        {
            return "Ma Reine" + instance.Nom;
        }
        public void PondreOeuf()
        {
            FabriqueFourmiliere fabrique = new FabriqueFourmiliere();
            oeufPondu=(Oeuf)fabrique.CreerOeuf(String.Format("Oeuf N° {0}", zone.ObjetList.Where(x=>x.GetType().Equals(typeof(Oeuf))).Count()), Position);
        }
    }
}
