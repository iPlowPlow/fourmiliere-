using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;
using LibMetier.GestionPersonnages;
using System.Collections.ObjectModel;
using LibMetier.GestionObjets;

namespace LibMetier.GestionStrategie
{
    public class Normal : StrategieAbstraite
    {
        public Normal(string nom)
        {
            this.Nom = nom;
        }

        public override void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso)
        {
            ObservableCollection<ZoneAbstrait> zonePheromone = new ObservableCollection<ZoneAbstrait>();
            ObservableCollection<ZoneAbstrait> zoneAccessible = new ObservableCollection<ZoneAbstrait>();
            ZoneAbstrait zoneNourriture = null;

            //Console.WriteLine(unPerso.ChoixZoneSuivante.Zonesaccessibles.Count);
            foreach (ZoneAbstrait uneZone in unPerso.ChoixZoneSuivante.Zonesaccessibles)
            {
                if (uneZone.PersonnageList.Where(x => x.GetType().Equals(typeof(Termite))).Count() == 0)
                {
                    /*si on trouve de la nourriture on go*/
                    try
                    {
                        if (uneZone.ObjetList.Where(x => x.GetType().Equals(typeof(Nourriture))).Count() > 0)
                        {
                            zoneNourriture = uneZone;
                            break;
                        }
                        else if (uneZone.ObjetList.Where(x => x.GetType().Equals(typeof(Pheromone))).Count() > 0)
                        {
                            zonePheromone.Add(uneZone);
                        }
                        else
                        {
                            zoneAccessible.Add(uneZone);
                        }
                    }catch(Exception e)
                    {
                        e.ToString();
                    }
                }
                
            }

            if (zoneNourriture!= null)
            {
                unPerso.Position.X = zoneNourriture.Position.X;
                unPerso.Position.Y = zoneNourriture.Position.Y;

            }
            else if(zonePheromone.Count >0)
            {
                ZoneAbstrait zoneMinPhero = null;
                int dureeDeVeMin = Pheromone.DUREE_VIE_ORIGINALE;
                foreach(ZoneAbstrait uneZone in zonePheromone)
                {
                    foreach(Pheromone phermonoe in uneZone.ObjetList.Where(x => x.GetType().Equals(typeof(Pheromone))))
                    {
                        if (phermonoe.Dureevie< dureeDeVeMin)
                        {
                            dureeDeVeMin = phermonoe.Dureevie;
                            zoneMinPhero = uneZone;
                        }
                    }
                }

                unPerso.Position.X = zoneMinPhero.Position.X;
                unPerso.Position.Y = zoneMinPhero.Position.Y;

            }
            else
            {

                /*Selectionne une case vide au harzard*/
                int maCase = Hazard.Next(zoneAccessible.Count);

                /*Récupération de l'ancienne case pour ne pas retourner en arriere*/
                int oldX = unPerso.ListEtape[unPerso.ListEtape.Count-1].position.X;
                int oldY = unPerso.ListEtape[unPerso.ListEtape.Count-1].position.Y;
                if (unPerso.Position.Y != zoneAccessible[maCase].Position.Y  && unPerso.Position.X != zoneAccessible[maCase].Position.X && (zoneAccessible[maCase].Position.X != oldX || zoneAccessible[maCase].Position.Y != oldY))
                {
                    unPerso.Position.X = zoneAccessible[maCase].Position.X;
                    unPerso.Position.Y = zoneAccessible[maCase].Position.Y;
                }
                /*int newX = unPerso.Position.X + Hazard.Next(3) - 1;
                int newY = unPerso.Position.Y + Hazard.Next(3) - 1;
                if ((newX >= 0) && (newX < dimX)) unPerso.Position.X = newX;
                if ((newY >= 0) && (newY < dimX)) unPerso.Position.Y = newY;*/
            }
           
        }

    }
}
