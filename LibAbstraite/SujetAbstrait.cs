﻿using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite
{
    public interface SujetAbstrait
    {

        void Attach(PersonnageAbstrait observer);
        void Detach(PersonnageAbstrait observer);
        void Notify();
    }
}
