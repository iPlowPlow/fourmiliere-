﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionPersonnages
{
    class Termite : PersonnageAbstrait
    {
        String nom { get; set; }
        public Termite(String _nom)
        {
            this.nom = _nom;
        }
        public override ZoneAbstrait ChoixZoneSuivante(List<AccesAbstrait> accesList)
        {
            throw new NotImplementedException();
        }
    }
}