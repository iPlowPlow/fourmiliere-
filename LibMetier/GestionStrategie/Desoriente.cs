using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;

namespace LibMetier.GestionStrategie
{
    public class Desoriente : StrategieAbstraite
    {
        public Desoriente(string nom)
        {
            this.Nom = nom;
        }

        public override void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso)
        {
            int maCase = Hazard.Next(unPerso.ChoixZoneSuivante.Zonesaccessibles.Count);

            unPerso.Position.X = unPerso.ChoixZoneSuivante.Zonesaccessibles[maCase].Position.X;
            unPerso.Position.Y = unPerso.ChoixZoneSuivante.Zonesaccessibles[maCase].Position.Y;


        }
    }
}
