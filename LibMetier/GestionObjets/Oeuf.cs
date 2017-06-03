﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionObjets;
namespace LibMetier.GestionObjets
{
  public  class Oeuf : ObjetAbstrait
    {
        public const int DUREE_AVANT_ECLOSION = 15;
        //Incrémenté à chaque tour
        public int Age { get; set; }
        public Oeuf()
        {
          
        }
        public Oeuf(string nom)
        {
            this.Nom = nom;
            this.Age = 0;
        }
        public override void TourPasse()
        {
            Age++;
        }
    }
}
