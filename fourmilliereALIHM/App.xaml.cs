﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LibAbstraite
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        public static FourmilliereModel fourmilliereVM { get; set; }
        public App()
        {
            fourmilliereVM = new FourmilliereModel();
        }
      
    }
}
