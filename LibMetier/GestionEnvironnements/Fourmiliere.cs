using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.Fabrique;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System.Collections.ObjectModel;

using System.Xml.Serialization;
using System.Threading;
using LibMetier.GestionPersonnages;
using LibMetier.GestionObjets;
using System.Windows.Threading;
using LibMetier.GestionObjets;
using LibAbstraite;

namespace LibMetier.GestionEnvironnements
{

    public class Fourmiliere : EnvironnementAbstrait
    {
        public static Reine reine;
        public Fourmiliere()
        {

        }

        public Fourmiliere(int _dimensionX, int _dimensionY)
        {
            TitreApplication = "Application FourmilliereAL";
            vitesse = 500;

            this.DimensionX = _dimensionX;
            this.DimensionY = _dimensionY;

            
            Fabrique = new FabriqueFourmiliere();
            PersonnagesList = new ObservableCollection<PersonnageAbstrait>();
            PersonnagesMortList = new ObservableCollection<PersonnageAbstrait>();

            //PersonnagesList.Add(Fabrique.CreerGuerriere("Guerriere 0"));
            PersonnagesList.Add(Fabrique.CreerOuvriere("Ouvriere 0", Fabrique.CreerPosition(10, 10)));
            //PersonnagesList.Add(Fabrique.CreerTermite("Termite 0"));
            AjouterReine();

            ObjetList = new ObservableCollection<ObjetAbstrait>();
            ZoneList = new ObservableCollection<ZoneAbstrait>();

            InitZones();

         

        }
        

        public override void AjouterGuerriere()
        {
            PersonnagesList.Add(Fabrique.CreerGuerriere(String.Format("Guerriere {0}",PersonnagesList.Count), Fabrique.CreerPosition(10, 10)));
        }
        public override void AjouterOuvriere()
        {
            PersonnagesList.Add(Fabrique.CreerOuvriere(String.Format("Ouvriere {0}", PersonnagesList.Count), Fabrique.CreerPosition(10, 10)));
        }
        public override void AjouterTermite()
        {

            System.Windows.Application.Current.Dispatcher.Invoke(
                 DispatcherPriority.Normal,
                 (Action)delegate ()
                 {
                     PersonnagesList.Add(Fabrique.CreerTermite(String.Format("Termite {0}", PersonnagesList.Count), Fabrique.CreerPosition(DimensionX, DimensionY)));
                 }
             );
            
        }
        public void AjouteNourriture()
        {
            CoordonneesAbstrait position = new Coordonnees(Hazard.Next(1, DimensionX), Hazard.Next(1, DimensionY));
            while(position.X.Equals(reine.Position.X)||position.Y.Equals(reine.Position.Y))
            {
                position = new Coordonnees(Hazard.Next(1, DimensionX), Hazard.Next(1, DimensionY));
            }
            ObjetList.Add(Fabrique.CreerNourriture("Nourriture N "+ ObjetList.Count, position));
        }

        public override void AjouteOeuf(ObjetAbstrait unOeuf)
        {
            this.ObjetList.Add(unOeuf);
        }

        public override void AjoutePheromone(ObjetAbstrait unPheromone)
        {
            this.ObjetList.Add(unPheromone);
        }
        public override void AjouterReine()
        {
            if (PersonnagesList.Where(x => x.GetType().Equals(typeof(Reine))).Count() == 0)
            {
                reine = (Reine)Fabrique.CreerReine("La reine", Fabrique.CreerPosition(10, 10));
                PersonnagesList.Add(reine);
            }
        }
        public override void AjouteZone(params ZoneAbstrait[] zoneArray)
        {
            
        }

        public override void ChargerEnv(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }
        
        public override void ChargerObjet(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void ChargerPersonnage(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void DeplacerPersonnage(PersonnageAbstrait unPersonnage, ZoneAbstrait zdebut, ZoneAbstrait zfin)
        {
            throw new NotImplementedException();
        }

        public override void Repositioner()
        {
            foreach(var boutDeTerrain in ZoneList)
            {
                boutDeTerrain.PersonnageList.Clear();
                boutDeTerrain.ObjetList.Clear();
                boutDeTerrain.PersonnageList.AddRange(PersonnagesList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())));
                boutDeTerrain.ObjetList.AddRange(ObjetList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())));
                foreach (PersonnageAbstrait unPerso in PersonnagesList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())))
                {
                    unPerso.zone = boutDeTerrain;
                }
            }
        }

        public override void FournirAcces()
        {

            
            foreach (var unInsecte in PersonnagesList)
            {
                List<ZoneAbstrait> zoneAdjacentes = new List<ZoneAbstrait>();
                zoneAdjacentes.AddRange(ZoneList.Where(x => x.Position.X > (unInsecte.Position.X - 2) && x.Position.X < (unInsecte.Position.X + 2) 
                && x.Position.Y > (unInsecte.Position.Y - 2) && x.Position.Y < (unInsecte.Position.Y + 2)
                ));
                
                unInsecte.ChoixZoneSuivante = Fabrique.CreerAcces(zoneAdjacentes);
            }

        }

        public override string Simuler()
        {
            throw new NotImplementedException();
        }

        public override string Statistiques()
        {
            throw new NotImplementedException();
        }

        public void SupprimerPersonnage()
        {
            PersonnagesList.Remove(PersoSelect);
        }

        public void Mourrir(PersonnageAbstrait unPerso)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                 DispatcherPriority.Normal,
                 (Action)delegate ()
                 {
                     unPerso.ListEtape.Add(new Etape(0, "Je meurs......."));
                     PersonnagesMortList.Add(unPerso);
                     PersonnagesList.Remove(unPerso);  
                 }
             );
        }

        public void AjouterReine(Reine reine)
        {
            throw new NotImplementedException();
        }
        public void AjouterFourmi(PersonnageAbstrait fourmi)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                 DispatcherPriority.Normal,
                 (Action)delegate ()
                 {
                     PersonnagesList.Add(fourmi);
                 }
             );
        }
        public override void TourSuivant()
        {
            Repositioner();
            FournirAcces();
            foreach (Pheromone unePheromone in ObjetList.Where(x => x.GetType().Equals(typeof(Pheromone))).ToList())
            {
                if (unePheromone.Dureevie < 1)
                {
                    ObjetList.Remove(unePheromone);
                }
            }
            foreach (Oeuf unOeuf in ObjetList.Where(x => x.GetType().Equals(typeof(Oeuf))).ToList())
            {
                if(unOeuf.Age == Oeuf.DUREE_AVANT_ECLOSION)
                {
                    PersonnageAbstrait fourmi = unOeuf.fourmiARetourner;
                    fourmi.Nom += PersonnagesList.Count;
                    AjouterFourmi(fourmi);
                    ObjetList.Remove(unOeuf);
                }
            }
            foreach (Nourriture nourriture in ObjetList.Where(x => x.GetType().Equals(typeof(Nourriture))).ToList())
            {
                if (nourriture.ListMorceaux.Count < 1)
                {
                    ObjetList.Remove(nourriture);
                }
            }
            foreach (ObjetAbstrait unObjet in ObjetList)
            {
                unObjet.TourPasse();
            }
            Repositioner();
            FournirAcces();
            foreach (PersonnageAbstrait unInsecte in PersonnagesList.ToList())
            {
                if (unInsecte.GetType().Equals(typeof(Reine)))
                {
                    Reine reine = (Reine)unInsecte;
                    if (reine.OeufPondu != null)
                    {
                        List<ObjetAbstrait> morceaux = ObjetList.Where(x => x.GetType().Equals(typeof(MorceauNourriture))).ToList();
                        ObjetList.Remove(morceaux[0]);
                        ObjetList.Add(reine.OeufPondu);
                        Repositioner();
                        FournirAcces();
                    }
                    reine.OeufPondu = null;
                }
                unInsecte.Avance1Tour(DimensionX, DimensionY, tourActuel);
                if (unInsecte.GetType().Equals(typeof(Ouvriere)) && unInsecte.TransporteNourriture == true)
                {
                    if (unInsecte.Position.toString().Equals(reine.Position.toString()))
                    {
                        Ouvriere ouvriere = (Ouvriere)unInsecte;
                        MorceauNourriture morceau = ouvriere.DeposeMorceau();
                        ObjetList.Add(morceau);
                    }
                    Coordonnees coordonnees = new Coordonnees(unInsecte.Position.X, unInsecte.Position.Y);
                    Pheromone unPheromone = new Pheromone("pheromone", coordonnees);
                    ObjetList.Add(unPheromone);

                }
                if (unInsecte.PV <= 0)
                {
                    Mourrir(unInsecte);
                } 
            }
            if (Hazard.Next(1, 11) == 1)
            {
                AjouteNourriture();
            }


            
           if(tourActuel>50 && Hazard.Next(1, 11) == 1)
            {
                AjouterTermite();
            }
            tourActuel++;
        }

        public override void InitZones()
        {
            for (int i = 0; i < DimensionX; i++)
            {
                for (int j = 0; j < DimensionY; j++)
                {
                    ZoneList.Add(Fabrique.CreerZone(String.Format("Zone en X={0}, Y={1}", i, j), Fabrique.CreerPosition(i, j)));
                }
            }
        }


        public override void Avance()
        {
            encours = true;
            while (encours == true)
            {
                Thread.Sleep(vitesse);
                TourSuivant();

            }
        }
        public override void Stop()
        {
            encours = false;
        }

        public override void AjouteNourriture(ObjetAbstrait unObject)
        {
            throw new NotImplementedException();
        }
    }
}
