using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;
using System.Collections.ObjectModel;

namespace LibMetier.GestionStrategie
{
    public class Retour : StrategieAbstraite
    {
        public Retour(string nom)
        {
            this.Nom = nom;
        }

        public override void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso)
        {
            if (unPerso.Position.Y != unPerso.Maison.Y)
            {
                if (unPerso.Position.Y < unPerso.Maison.Y)
                {
                    unPerso.Position.Y++;
                }
                else
                {
                    unPerso.Position.Y--;
                }
            }
            else if (unPerso.Position.X != unPerso.Maison.X)
            {
                if (unPerso.Position.X < unPerso.Maison.X)
                {
                    unPerso.Position.X++;
                }
                else
                {
                    unPerso.Position.X--;
                }
            }

        }
    }
}
