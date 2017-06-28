using fourmilliereALIHM;
using LibMetier.GestionEnvironnements;
using LibMetier;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace fourmilliereALIHM
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        public static Fourmiliere fourmilliereVM { get; set; }
        public static Config config { get; set; }
        public static Meteo meteo { get; set; }
        public App()
        {   

            fourmilliereVM = new Fourmiliere(30, 30);
            config = new Config();

        }
    }
}
