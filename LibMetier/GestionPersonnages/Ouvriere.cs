﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;
using System.Xml.Serialization;

namespace LibMetier.GestionPersonnages
{
    
  public class Ouvriere : PersonnageAbstrait
   {

        public Ouvriere()
        {
         
        }
        public Ouvriere(string nom)
        {
            this.Nom = nom;
            this.pointsdevie = 20;
        }
        public override ZoneAbstrait ChoixZoneSuivante()
        {
            throw new NotImplementedException();
        }
        public override void Avance1Tour(int dimX, int dimY)
        {
            AvanceAuHazard(dimX, dimY);
            //ListEtape.Add(new Etape());
        }
        public override void AvanceAuHazard(int dimX, int dimY)
        {
            int newX = this.position.X + Hazard.Next(3) - 1;
            int newY = this.position.Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX)) this.position.X = newX;
            if ((newY >= 0) && (newY < dimX)) this.position.Y = newY;
        }
    }
}
