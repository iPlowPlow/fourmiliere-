using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;
using System.Collections.ObjectModel;
using LibMetier.GestionPersonnages;

namespace LibMetier.GestionStrategie
{
    public class Attaque : StrategieAbstraite
    {
        public Attaque(string nom)
        {
            this.Nom = nom;
        }

        public override void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso)
        {

            ObservableCollection<ZoneAbstrait> zoneAccessible = new ObservableCollection<ZoneAbstrait>();
            ZoneAbstrait zoneFourmille = null;

            foreach (ZoneAbstrait uneZone in unPerso.ChoixZoneSuivante.Zonesaccessibles)
            {
                
                /*si on trouve une Fourmille on l'attaque*/
                if (uneZone.PersonnageList.Where(x => x.GetType().Equals(typeof(Reine))).Count() > 0)
                {
                    zoneFourmille = uneZone;
                    break;
                }
                else if (uneZone.PersonnageList.Where(x => x.GetType().Equals(typeof(Ouvriere))).Count() > 0)
                {
                    zoneFourmille = uneZone;
                    break;
                }
                else if (uneZone.PersonnageList.Where(x => x.GetType().Equals(typeof(Guerriere))).Count() > 0)
                {
                    zoneFourmille = uneZone;
                    break;
                }
                else
                {
                    zoneAccessible.Add(uneZone);
                }
                

            }
            
            if (zoneFourmille != null)
            {
                unPerso.Position.X = zoneFourmille.Position.X;
                unPerso.Position.Y = zoneFourmille.Position.Y;

            }else
            {
                int maCase = Hazard.Next(zoneAccessible.Count);
                zoneAccessible.OrderByDescending(x => x.Position.X);
                if (unPerso.Position.X > unPerso.Maison.X)
                {
                    if (zoneAccessible[maCase].Position.X != unPerso.Position.X && (unPerso.Position.X - unPerso.Maison.X) > zoneAccessible[maCase].Position.X - unPerso.Maison.X) unPerso.Position.X = zoneAccessible[maCase].Position.X;
                }else
                {
                    if (zoneAccessible[maCase].Position.X != unPerso.Position.X && (unPerso.Position.X - unPerso.Maison.X) < zoneAccessible[maCase].Position.X - unPerso.Maison.X) unPerso.Position.X = zoneAccessible[maCase].Position.X;
                }

                if (unPerso.Position.Y > unPerso.Maison.Y)
                {
                    if (zoneAccessible[maCase].Position.Y != unPerso.Position.Y && (unPerso.Position.Y - unPerso.Maison.Y) > zoneAccessible[maCase].Position.Y - unPerso.Maison.Y) unPerso.Position.Y = zoneAccessible[maCase].Position.Y;

                }else
                {
                    if (zoneAccessible[maCase].Position.Y != unPerso.Position.Y && (unPerso.Position.Y - unPerso.Maison.Y) < zoneAccessible[maCase].Position.Y - unPerso.Maison.Y) unPerso.Position.Y = zoneAccessible[maCase].Position.Y;
                }

            }
            /*
            int newX = unPerso.Position.X + Hazard.Next(3) - 1;
            int newY = unPerso.Position.Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX) && (newX<=unPerso.Maison.X+5) && (newX>=unPerso.Maison.X-5)) unPerso.Position.X = newX;
            if ((newY >= 0) && (newY < dimX) && (newY <= unPerso.Maison.Y + 5) && (newY >= unPerso.Maison.Y - 5)) unPerso.Position.Y = newY;
            */

        }
    }
}
