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
using LibMetier;
using System.Windows.Threading;
using LibAbstraite;

namespace LibMetier.GestionEnvironnements
{

    public class Fourmiliere : EnvironnementAbstrait,SujetAbstrait
    {
        public static Reine reine;
        public static CoordonneesAbstrait coordMaison;
        public int positionX = 10;
        public int positionY = 10;

        public Fourmiliere()
        {

        }

        public Fourmiliere(int _dimensionX, int _dimensionY)
        {
            TitreApplication = "Simulateur fourmili�re ALAP";
            vitesse = 200;

            this.DimensionX = _dimensionX;
            this.DimensionY = _dimensionY;
            Fabrique = new FabriqueFourmiliere();
            PersonnagesList = new ObservableCollection<PersonnageAbstrait>();
            PersonnagesMortList = new ObservableCollection<PersonnageAbstrait>();
            coordMaison = Fabrique.CreerPosition(10, 10);
            AjouterReine();
            //PersonnagesList.Add(Fabrique.CreerGuerriere("Guerriere 0"));
            //PersonnagesList.Add(Fabrique.CreerOuvriere("Ouvriere 0", Fabrique.CreerPosition(10, 10)));
            //PersonnagesList.Add(Fabrique.CreerTermite("Termite 0"));
            //PersonnagesList.Add(Fabrique.CreerTermite(String.Format("Termite {0}", PersonnagesList.Count), Fabrique.CreerPosition(3, 3), Fabrique.CreerPosition(positionX, positionY)));

            ListObservateur = new List<PersonnageAbstrait>();
            ObjetList = new ObservableCollection<ObjetAbstrait>();
            ZoneList = new ObservableCollection<ZoneAbstrait>();

            meteo = new Meteo();
            meteo.ListObservateur = new List<PersonnageAbstrait>();
            //place des morceaux de nourriture sous la reine pour qu'elle puisse pondre d�s le d�but...
            //plus besoin de faire spawn de fourmis
            for (int i = 0; i < 50; i++)
            {
                ObjetList.Add(new MorceauNourriture("Morceau N�" + i, Fabrique.CreerPosition(10, 10)));
            }
            InitZones();

        }
        

        public override void AjouterGuerriere()
        {
            PersonnageAbstrait g = Fabrique.CreerGuerriere(String.Format("Guerriere {0}", PersonnagesList.Count), Fabrique.CreerPosition(coordMaison.X, coordMaison.Y), coordMaison);
            PersonnagesList.Add(g);
            ListObservateur.Add(g);
            meteo.ListObservateur.Add(g);
        }
        public override void AjouterPrincesse()
        {
            PersonnageAbstrait g = Fabrique.CreerPrincesse(String.Format("Princesse {0}", PersonnagesList.Count), Fabrique.CreerPosition(coordMaison.X, coordMaison.Y), coordMaison);
            PersonnagesList.Add(g);
            ListObservateur.Add(g);
            meteo.ListObservateur.Add(g);
        }
        public override void AjouterOuvriere()
        {

            PersonnageAbstrait g = Fabrique.CreerOuvriere(String.Format("Ouvriere {0}", PersonnagesList.Count), Fabrique.CreerPosition(coordMaison.X, coordMaison.Y), coordMaison);
            
            PersonnagesList.Add(g);
            ListObservateur.Add(g);
            meteo.ListObservateur.Add(g);
            
        }
        public override void AjouterTermite()
        {

            System.Windows.Application.Current.Dispatcher.Invoke(
                 DispatcherPriority.Normal,
                 (Action)delegate ()
                 {
                     PersonnagesList.Add(Fabrique.CreerTermite(String.Format("Termite {0}", PersonnagesList.Count), Fabrique.CreerPosition(DimensionX, DimensionY), coordMaison));
                 }
             );
            
        }
        public void AjouteNourriture()
        {
            CoordonneesAbstrait position = new Coordonnees(Hazard.Next(1, DimensionX), Hazard.Next(1, DimensionY));
            while(position.X.Equals(coordMaison.X)||position.Y.Equals(coordMaison.Y))
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
                reine = (Reine)Fabrique.CreerReine("La reine", coordMaison);
                PersonnagesList.Add(reine);
            }
        }
        public override void AjouteZone(params ZoneAbstrait[] zoneArray)
        {
            
        }
        public void ChargerReine(Reine reinec)
        {
            reine = Reine.Instance(reinec);
            coordMaison.X = reine.Position.X;
            coordMaison.Y = reine.Position.Y;
        }

        public override void ChargerPersonnage(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }
        public  void RemoveAll()
        {
            for(int i = PersonnagesList.Count - 1; i > 0; i--)
            {
                PersonnagesList.RemoveAt(i);
            }
            for (int i = PersonnagesMortList.Count - 1; i > 0; i--)
            {
                PersonnagesMortList.RemoveAt(i);
            }
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
        public void SupprimerPersonnage()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                 DispatcherPriority.Normal,
                 (Action)delegate ()
                 {
                     if (PersoSelect != null)
                     {
                         var persoToDelete = PersoSelect;
                         Mourrir(PersoSelect);
                         PersonnagesMortList.Remove(persoToDelete);
                     }
                 });
        }

        public void Mourrir(PersonnageAbstrait unPerso)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                 DispatcherPriority.Normal,
                 (Action)delegate ()
                 {
                     unPerso.ListEtape.Add(new Etape(0, "Je meurs.......", unPerso.Position.X, unPerso.Position.Y));
                     PersonnagesMortList.Add(unPerso);
                     PersonnagesList.Remove(unPerso);
                     meteo.ListObservateur.Remove(unPerso);
                     ListObservateur.Remove(unPerso);
                     if (unPerso.Equals(reine))
                     {
                         List<PersonnageAbstrait> princesses = PersonnagesList.Where(x => x.GetType().Equals(typeof(Princesse))).ToList();
                         
                         if (princesses.Count > 0)
                         {
                             Princesse nouvelleReine = (Princesse) princesses[0];
                             coordMaison.X = nouvelleReine.Position.X;
                             coordMaison.Y = nouvelleReine.Position.Y;
                             reine = Reine.RemplacerReine(nouvelleReine);
                             PersonnagesList.Remove(nouvelleReine);
                             PersonnagesList.Add(reine);
                         }
                         else
                         {
                             reine = null;
                         }
                     }
                 }
             );
        }

        public void AjouterFourmi(PersonnageAbstrait fourmi)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                 DispatcherPriority.Normal,
                 (Action)delegate ()
                 {
                     PersonnagesList.Add(fourmi);
                     meteo.Attach(fourmi);
                     ListObservateur.Add(fourmi);
                 }
             );
        }
     
        public override void TourSuivant()
        {
            if (!ReineMorte())

            {
                if (reine.sousAttaque == false)
                {
                    if (Etat != "normal")
                    {
                        Etat = "normal";
                        Notify();
                    }
                   
                 
                }
                if (reine.sousAttaque == true)
                {
                    
                        Etat = "attaque";
                        Notify();
                        reine.sousAttaque = false;
                    
                   
                }
                if (TourActuel % 10 == 0)
                {
                    meteoChange();
                }
                reine = (Reine)PersonnagesList.Where(x => x.GetType().Equals(typeof(Reine))).FirstOrDefault();
                if (reine.OeufPondu != null)
                {
                    List<ObjetAbstrait> morceaux = ObjetList.Where(x => x.GetType().Equals(typeof(MorceauNourriture)) && x.Position.toString().Equals(reine.Position.toString())).ToList();
                    if (morceaux.Count > 0)
                    {
                        ObjetList.Remove(morceaux[0]);
                        ObjetList.Add(reine.OeufPondu);
                    }
                }
                reine.OeufPondu = null;
                foreach (Oeuf unOeuf in ObjetList.Where(x => x.GetType().Equals(typeof(Oeuf))).ToList())
                {
                    if (unOeuf.Age > Oeuf.DureeAvantEclosion - 1)
                    {
                        PersonnageAbstrait fourmi = unOeuf.fourmiARetourner;
                        fourmi.Nom += PersonnagesList.Count;
                        AjouterFourmi(fourmi);
                        ObjetList.Remove(unOeuf);
                    }
                }
                foreach (Pheromone unePheromone in ObjetList.Where(x => x.GetType().Equals(typeof(Pheromone))).ToList())
                {
                    if (unePheromone.Dureevie < 1)
                    {
                        ObjetList.Remove(unePheromone);
                    }
                }
                foreach (Nourriture nourriture in ObjetList.Where(x => x.GetType().Equals(typeof(Nourriture))).ToList())
                {
                    if (nourriture.ListMorceaux.Count < 1 || nourriture.Dureevie < 1)
                    {
                        ObjetList.Remove(nourriture);
                    }
                }
                foreach (ObjetAbstrait unObjet in ObjetList)
                {
                    unObjet.TourPasse(meteo);
                }
                Repositioner();
                FournirAcces();
                foreach (PersonnageAbstrait unInsecte in PersonnagesList.ToList())
                {
                    unInsecte.Avance1Tour(DimensionX, DimensionY, TourActuel);
                    if (unInsecte.GetType().Equals(typeof(Ouvriere)) && unInsecte.TransporteNourriture == true)
                    {
                        if (unInsecte.Position.toString().Equals(coordMaison.toString()))
                        {
                            Ouvriere ouvriere = (Ouvriere)unInsecte;
                            MorceauNourriture morceau = ouvriere.DeposeMorceau();
                            ObjetList.Add(morceau);
                        }
                        Coordonnees coordonnees = new Coordonnees(unInsecte.Position.X, unInsecte.Position.Y);
                        Pheromone unPheromone = new Pheromone("pheromone", coordonnees);
                        ObjetList.Add(unPheromone);

                    }
                }
                List<PersonnageAbstrait> persosMorts = PersonnagesList.Where(x => x.PV < 1).ToList();
                foreach (PersonnageAbstrait persomort in persosMorts)
                {
                    Mourrir(persomort);
                }
                if (Hazard.Next(1, 7) == 1)
                {
                    AjouteNourriture();
                }

                if (TourActuel % 50 == 0)
                {
                    for(int i =0; i<Config.nbrTermites; i++) AjouterTermite();

                }
                TourActuel++;
            }
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
            while (encours == true && reine!= null)
            {
                Thread.Sleep(500-vitesse);
                TourSuivant();

            }
        }
        public override void Stop()
        {
            encours = false;
        }



        public  void Attach(PersonnageAbstrait observer)
        {
            ListObservateur.Add(observer);
        }

        public  void Detach(PersonnageAbstrait observer)
        {
            ListObservateur.Remove(observer);
        }

        public  void Notify()
        {
            foreach (PersonnageAbstrait unInsecte in ListObservateur)
            {
                unInsecte.maj(this.Etat);
            }
        }
        public void meteoChange()
        {
            int rand = Hazard.Next(1, 100);
            if ( rand >0 && rand<Config.probaPluie)
            {
                meteo.Etat = "pluie";
                meteo.Notify();
            }else if (rand > Config.probaPluie && rand <(Config.probaPluie+Config.probaBrouillard))
            {
                meteo.Etat = "brouillard";
                meteo.Notify();
            }
            else
            {
                meteo.Etat = "soleil";
                meteo.Notify();
            }
        }
        public Boolean ReineMorte()
        {
            return reine == null ? true : false;
        }
        public override string Stats()
        {
            return String.Format("Fourmies ouvri�res n�es : {0} \nFourmies ouvri�res mortes : {1} \nFourmies guerri�res n�es : {2} \nFourmies guerri�res mortes : {3} \nTermites n�es : {4} \nTermites mortes : {5} \nPrincesses n�es : {6} \nPrincesses mortes : {7} \nTransformations de princesses en reine : {8} \nReines mortes : {9}"
                , (PersonnagesList.Where(x => x.GetType().Equals(typeof(Ouvriere))).Count() + PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Ouvriere))).Count())
                , PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Ouvriere))).Count()
                , (PersonnagesList.Where(x => x.GetType().Equals(typeof(Guerriere))).Count() + PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Guerriere))).Count())
                , PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Guerriere))).Count()
                , (PersonnagesList.Where(x => x.GetType().Equals(typeof(Termite))).Count() + PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Termite))).Count())
                , PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Termite))).Count()
                , ((PersonnagesList.Where(x => x.GetType().Equals(typeof(Reine))).Count() + PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Reine))).Count())+(PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Princesse))).Count()-1))
                , PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Princesse))).Count()
                , ((PersonnagesList.Where(x => x.GetType().Equals(typeof(Reine))).Count() + PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Reine))).Count())-1)
                , (PersonnagesList.Where(x => x.GetType().Equals(typeof(Reine))).Count() + PersonnagesMortList.Where(x => x.GetType().Equals(typeof(Reine))).Count())
            );
        }
    }
}
