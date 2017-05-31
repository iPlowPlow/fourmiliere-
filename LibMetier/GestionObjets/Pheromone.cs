﻿using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionObjets
{
    class Pheromone : ObjetAbstrait
    {
        String nom { get; set; }

        public Pheromone(string nom)
        {
            this.nom = nom;
        }

    }
}