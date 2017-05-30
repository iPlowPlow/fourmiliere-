﻿using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.Fabrique
{
    public abstract class FabriqueAbstraite
    {
        public abstract string Titre { get; }
        public abstract EnvironnementAbstrait CreerEnvironnement(int dimensionX, int dimensionY);
        public abstract ZoneAbstrait CrerZone(string nom);
        public abstract AccesAbstrait CreerAcces(ZoneAbstrait zDebut, ZoneAbstrait zFin);
        public abstract PersonnageAbstrait CreerGuerriere(string nom);
        public abstract PersonnageAbstrait CreerOuvriere(string nom);
        public abstract PersonnageAbstrait CreerReine(string nom);
        public abstract PersonnageAbstrait CreerTermite(string nom);
        public abstract ObjetAbstrait CreerOeuf(string nom);
        public abstract ObjetAbstrait CreerPheromone(string nom);
        public abstract ObjetAbstrait CreerNourriture(string nom);

    }
}
