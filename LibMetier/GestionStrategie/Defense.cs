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
    public class Defense : StrategieAbstraite
    {
        public Defense(string nom)
        {
            this.Nom = nom;
        }

        public override void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso)
        {

            ObservableCollection<ZoneAbstrait> zoneAccessible = new ObservableCollection<ZoneAbstrait>();
            ZoneAbstrait zoneTermite = null;

            foreach (ZoneAbstrait uneZone in unPerso.ChoixZoneSuivante.Zonesaccessibles)
            {
                if (uneZone.Position.Y>=unPerso.Maison.Y - 5 && uneZone.Position.Y <= unPerso.Maison.Y +5 && uneZone.Position.X >= unPerso.Maison.X - 5 && uneZone.Position.X <= unPerso.Maison.X+5)
                {
                    /*si on un termite on l'attaque*/
                    if (uneZone.PersonnageList.Where(x => x.GetType().Equals(typeof(Termite))).Count() > 0)
                    {
                        zoneTermite = uneZone;
                        break;
                    }
                    else
                    {
                        zoneAccessible.Add(uneZone);
                    }
                }

            }

            if (zoneTermite != null)
            {
                unPerso.Position.X = zoneTermite.Position.X;
                unPerso.Position.Y = zoneTermite.Position.Y;

            }else
            {
                int maCase = Hazard.Next(zoneAccessible.Count);
                if (zoneAccessible.Count > 0)
                {
                    unPerso.Position.X = zoneAccessible[maCase].Position.X;
                    unPerso.Position.Y = zoneAccessible[maCase].Position.Y;
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
