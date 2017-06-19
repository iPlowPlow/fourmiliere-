using LibAbstraite;
using LibAbstraite.GestionEnvironnement;
using LibMetier.GestionEnvironnements;

namespace LibMetier
{
    public class Etape : EtapeAbstraite
    {
      
        public Etape(int tour, string action,int X, int Y) {
            this.action = action;
            this.tour = tour;
            position = new Coordonnees();
            this.position.X = X;
            this.position.Y = Y;
        }

        public Etape(string action, int x, int y)
        {
            this.action = action;
            this.position.X = x;
            this.position.Y = y;
        }
    }
}