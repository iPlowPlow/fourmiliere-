﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionObjets;
using LibMetier.GestionEnvironnements;
using LibMetier.GestionObjets;
using LibMetier.GestionPersonnages;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using LibAbstraite.GestionStrategie;
using LibMetier.GestionStrategie;
using LibMetier;
using LibAbstraite;

namespace fourmilliereALIHM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        const Int32 BufferSize = 128;
        Stopwatch stopwatch = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.fourmilliereVM;
            dt.Tick += Redessine_Tick;
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.fourmilliereVM.vitesse);
            initPlateau();
            Dessine();

        }
        private void Redessine_Tick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                checkReineVivante();
                Dessine();
            }
           
        }
        public Boolean checkReineVivante()
        {
            if (App.fourmilliereVM.ReineMorte())
            {
                Restart();
            }
            return true;
        }
        public void Restart()
        {
            System.Windows.MessageBox.Show("La partie va être relancée", "Vous n'avez pas de reine");
            System.Windows.MessageBox.Show(App.fourmilliereVM.Stats(), "Statistiques de fin de partie");
            ChangeButtonStatus(true);
            stopwatch.Stop();
            App.fourmilliereVM = new Fourmiliere(30, 30);
            DataContext = App.fourmilliereVM;
        }
        public void Configure(object sender, RoutedEventArgs e)
        {
            Configuration config = new Configuration();
            config.Show();
        }
        public void Dessine()
        {
          
            rain.Visibility = Visibility.Collapsed;
            fog.Visibility = Visibility.Collapsed;
            initPlateau();
            switch (App.fourmilliereVM.meteo.Etat)
            {
                case "pluie":
                    rain.Visibility = Visibility.Visible;
                    break;
                case "brouillard":
                    fog.Visibility = Visibility.Visible;
                    break;
            }
            int i = 0;
            foreach (ZoneAbstrait zone in App.fourmilliereVM.ZoneList.ToList().OrderBy(x=>x.Nom))
            {
                if ((i%9) == 0) {
                    Image img = new Image();
                
                    Uri uri = new Uri("Images/grass.png", UriKind.Relative);
                    img.Source = new BitmapImage(uri);
                    Plateau.Children.Add(img);
                    Grid.SetColumn(img, zone.Position.Y);
                    Grid.SetRow(img, zone.Position.X);
                    
                }
                i++;
            }
            foreach (ObjetAbstrait unObjet in App.fourmilliereVM.ObjetList.Where(x=> !x.GetType().Equals(typeof(MorceauNourriture))).ToList())
            {
                Image img = new Image();
                if (unObjet.GetType().Equals(typeof(Oeuf)))
                {
                    Uri uri = new Uri("Images/oeuf.png", UriKind.Relative);
                    img.Source = new BitmapImage(uri);
                }
                if (unObjet.GetType().Equals(typeof(Nourriture)))
                {
                    Uri uri = new Uri("Images/nourriture.png", UriKind.Relative);
                    img.Source = new BitmapImage(uri);
                }
                if (unObjet.GetType().Equals(typeof(Pheromone)))
                {
                    Pheromone pheromone = (Pheromone)unObjet;
                    byte opacity = Convert.ToByte(pheromone.Dureevie * (255 / Pheromone.DureeVieOriginale));
                    Color color = Color.FromArgb(opacity, 0, 0, 255);
                    Rectangle pheromoneBox = new Rectangle();
                    pheromoneBox.Fill = new SolidColorBrush(color);
                    Plateau.Children.Add(pheromoneBox);
                    Grid.SetColumn(pheromoneBox, pheromone.Position.Y);
                    Grid.SetRow(pheromoneBox, pheromone.Position.X);
                }
                else
                {
                    Plateau.Children.Add(img);
                    Grid.SetColumn(img, unObjet.Position.Y);
                    Grid.SetRow(img, unObjet.Position.X);
                }
            }
            foreach (PersonnageAbstrait unInsecte in App.fourmilliereVM.PersonnagesList.ToList())
            {
                Image img = new Image();
              
              
                if (unInsecte.GetType().Equals(typeof(Guerriere)))
                {
                    Uri uri = new Uri("Images/fourmi_combattante.png", UriKind.Relative);
                    img.Source = new BitmapImage(uri);
                  
                }
                if (unInsecte.GetType().Equals(typeof(Princesse)))
                {
                    Uri uri = new Uri("Images/princesse.png", UriKind.Relative);
                    img.Source = new BitmapImage(uri);

                }
                if (unInsecte.GetType().Equals(typeof(Ouvriere)))
                {
                    string source = "Images/fourmi_ouvriere.png";
                    if (unInsecte.TransporteNourriture)
                    {
                        source = "Images/fourmi_ouvriere_chargee.png";
                    }
                    Uri uri = new Uri(source, UriKind.Relative);
                    img.Source = new BitmapImage(uri);
                   
                }
                if (unInsecte.GetType().Equals(typeof(Reine)))
                {
                 
                  
                  Uri  uri = new Uri("Images/reine.png", UriKind.Relative);
                  img.Source = new BitmapImage(uri);


                }
                if (unInsecte.GetType().Equals(typeof(Termite)))
                {
                    Uri uri = new Uri("Images/termite.png", UriKind.Relative);
                    img.Source = new BitmapImage(uri);
               
                }
           
                Plateau.Children.Add(img);

                Grid.SetColumn(img, unInsecte.Position.Y);
                Grid.SetRow(img, unInsecte.Position.X);

            }
         
        }

        private void ChangeButtonStatus(bool state)
        {
            AddGuerriere.IsEnabled = state;
            AddOuvriere.IsEnabled = state;
            AddPrincesse.IsEnabled = state;
            AddReine.IsEnabled = state;
            AddTermite.IsEnabled = state;
            btnAvance.IsEnabled = state;
            btnCharge.IsEnabled = state;
            btnSave.IsEnabled = state;
            btnSuivant.IsEnabled = state;
            deletePerso.IsEnabled = state;
            config.IsEnabled = state;
            btnStop.IsEnabled = !state;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AProposWindows apx = new AProposWindows();
            apx.Show();
        }

        private void Button_Click_Add_Guerriere(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.AjouterGuerriere();
        }

        private void Button_Click_Add_Ouvriere(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.AjouterOuvriere();
        }

        private void Button_Click_Add_Princesse(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.AjouterPrincesse();
        }

        private void Button_Click_Add_Reine(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.AjouterReine();
        }

        private void Button_Click_Add_Termite(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.AjouterTermite();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.SupprimerPersonnage();
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            if (checkReineVivante())
            {
                App.fourmilliereVM.TourSuivant();
            }
            Dessine();
        }
        private void btnAvance_Click(object sender, RoutedEventArgs e)
        {
            ChangeButtonStatus(false);
            dt.Start();
            stopwatch.Start();
            Thread tt = new Thread(App.fourmilliereVM.Avance);
            tt.Start();
            Dessine();
            
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            ChangeButtonStatus(true);
            Stop();
        }
        private void Stop()
        {
            App.fourmilliereVM.Stop();
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
            Dessine();
        }
        public void ChangeSpeed(object sender, RoutedEventArgs e) {
            App.fourmilliereVM.vitesse = (int)slider.Value;
               dt.Interval = new TimeSpan(0, 0, 0, 0, App.fourmilliereVM.vitesse);
        }
        private void redemarrer(object sender, RoutedEventArgs e)
        {
            Stop();
            Restart();
            Dessine();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<PersonnageAbstrait> listp=App.fourmilliereVM.PersonnagesList;
            ObservableCollection<PersonnageAbstrait> listpmort = App.fourmilliereVM.PersonnagesMortList;
            ObservableCollection<ObjetAbstrait> listo = App.fourmilliereVM.ObjetList;
            List<Nourriture> listn = new List<Nourriture>();
            List<Ouvriere> listouvmort = new List<Ouvriere>();
        
            List<Ouvriere> listouv = new List<Ouvriere>();
            listouvmort = listpmort.Where(x => (x.GetType().Equals(typeof(Ouvriere)))).ToList().ConvertAll(o => (Ouvriere)o);
            listouv = listp.Where(x => (x.GetType().Equals(typeof(Ouvriere)))).ToList().ConvertAll(o => (Ouvriere)o);
            listn = listo.Where(x => (x.GetType().Equals(typeof(Nourriture)))).ToList().ConvertAll(o => (Nourriture)o);
            List<Reine> actuel1 = listp.Where(x => (x.GetType().Equals(typeof(Reine)))).ToList().ConvertAll(o => (Reine)o);
            bool ponte = false;
            if (actuel1.First().OeufPondu != null)
            {
                ponte = true;
            }
            var doc = new XDocument(
                new XElement("Simulation",
                    new XElement("fourmilieres",
                        new XElement("fourmiliere",
                        new XAttribute("x", App.fourmilliereVM.DimensionX),
                        new XAttribute("y", App.fourmilliereVM.DimensionY)
                         )
                     ), ((listo.Where(x => (x.GetType().Equals(typeof(Nourriture)))).Count() > 0) ?
                     new XElement("nourritures",
                        from n in listn
                        where n.GetType().Equals(typeof(Nourriture))
                        select new XElement("nourriture",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("duree", n.Dureevie),
                             new XElement("listeNourriture",
                                   from et in n.ListMorceaux
                                   select new XElement("morceau",
                                       new XAttribute("nom", et.Nom),
                                       
                                       new XElement("coordonnees",
                                            new XAttribute("x", et.Position.X),
                                            new XAttribute("y", et.Position.Y)
                                        )
                                    ),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                        )
                     ) : null), ((listo.Where(x => (x.GetType().Equals(typeof(Oeuf)))).Count() > 0) ?
                      new XElement("oeufs",
                        from n in listo
                        where n.GetType().Equals(typeof(Oeuf))
                        select new XElement("oeuf",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("duree", n.Dureevie),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                      ) : null),
                       new XElement("pheromones",
                        from n in listo
                        where n.GetType().Equals(typeof(Pheromone))
                        select new XElement("pheromone",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("duree", n.Dureevie),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)

                                )
                             )
                        )
                     ), ((listp.Where(x => (x.GetType().Equals(typeof(Ouvriere)))).Count() > 0) ?
                        new XElement("Ouvrieres",
                        from n in listouv
                        where n.Morceau != null
                        select new XElement("Ouvriere",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XAttribute("transport", n.TransporteNourriture),
                            new XElement("morceau",
                                  new XAttribute("nom", n.Morceau.Nom),
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Morceau.Position.X),
                                    new XAttribute("y", n.Morceau.Position.Y)
                                )
                             ),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                            new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )


                        ),
                        
                        from n in listouv
                        where n.Morceau == null
                        select new XElement("Ouvriere",
                            new XAttribute("nom", n.Nom),
                             new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XAttribute("transport", n.TransporteNourriture),

                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                            new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",

                                   new XAttribute("tour",et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )


                                   )

                            )


                        )
                     
                     )
                    
                     
                     :null), ((listp.Where(x => (x.GetType().Equals(typeof(Guerriere)))).Count() > 0) ?
                        new XElement("Guerrieres",
                        from n in listp
                        where n.GetType().Equals(typeof(Guerriere))
                        select new XElement("Guerriere",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )
                        )
                     ):null), ((ponte==true) ?
                        new XElement("reines",
                          from actuel in actuel1
                          select new XElement("reine",
                            new XAttribute("nom", actuel.Nom),
                            new XAttribute("pv", actuel.pv),
                            new XAttribute("strat", actuel.StategieCourante.Nom),
                            new XElement("oeuf",
                                    new XAttribute("nom", actuel.OeufPondu.Nom),
                                    new XAttribute("age", actuel.OeufPondu.Age),
                                     new XElement("coordonnees",
                                        new XAttribute("x", actuel.Position.X),
                                        new XAttribute("y", actuel.Position.Y)
                                    )
                                ),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", actuel.Position.X),
                                    new XAttribute("y", actuel.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in actuel.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )
                        )):
                        new XElement("reines",
                          from actuel in actuel1
                          where actuel.OeufPondu == null
                          select new XElement("reine",
                            new XAttribute("nom", actuel.Nom),
                            new XAttribute("pv", actuel.pv),
                            new XAttribute("strat", actuel.StategieCourante.Nom),
                          
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", actuel.Position.X),
                                    new XAttribute("y", actuel.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in actuel.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )
                        )))
                   , ((listp.Where(x => (x.GetType().Equals(typeof(Termite)))).Count() > 0) ?
                        new XElement("termites",
                        from n in listp
                        where n.GetType().Equals(typeof(Termite))
                        select new XElement("termite",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                   new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )
                        )
                     ) : null), ((listp.Where(x => (x.GetType().Equals(typeof(Princesse)))).Count() > 0) ?
                        new XElement("princesses",
                        from n in listp
                        where n.GetType().Equals(typeof(Princesse))
                        select new XElement("princesse",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                   new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)
                                    
                                ))

                            )
                        )
                     ) : null), ((listpmort.Where(x => (x.GetType().Equals(typeof(Princesse)))).Count() > 0) ?
                        new XElement("princesses",
                        from n in listpmort
                        where n.GetType().Equals(typeof(Princesse))
                        select new XElement("princesse",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )


                                   )

                            )
                        )
                     ) : null), ((listpmort.Where(x => (x.GetType().Equals(typeof(Ouvriere)))).Count() > 0) ?
                        new XElement("Ouvrieresmorte",
                        from n in listouvmort
                        where n.Morceau != null
                        select new XElement("Ouvriere",
                            new XAttribute("nom", n.Nom),
                             new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XAttribute("transport", n.TransporteNourriture),
                            new XElement("morceau",
                                  new XAttribute("nom", n.Morceau.Nom),
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Morceau.Position.X),
                                    new XAttribute("y", n.Morceau.Position.Y)
                                )
                             ),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                            new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )


                        ),

                        from n in listouvmort
                        where n.Morceau ==null
                        select new XElement("Ouvriere",
                            new XAttribute("nom", n.Nom),
                             new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XAttribute("transport", n.TransporteNourriture),

                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                            new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )


                        )

                     )


                     : null), ((listpmort.Where(x => (x.GetType().Equals(typeof(Guerriere)))).Count() > 0) ?
                        new XElement("Guerrieresmorte",
                        from n in listpmort
                        where n.GetType().Equals(typeof(Guerriere))
                        select new XElement("Guerriere",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XAttribute("maisonx", n.Maison.X),
                            new XAttribute("maisony", n.Maison.Y),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )
                        )
                     ) : null), ((listpmort.Where(x => (x.GetType().Equals(typeof(Reine)))).Count() > 0) ?
                        new XElement("reinesmorte",
                        from n in listpmort
                        where n.GetType().Equals(typeof(Reine))
                        select new XElement("reine",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )
                        )
                     ) : null), ((listpmort.Where(x => (x.GetType().Equals(typeof(Termite)))).Count() > 0) ?
                        new XElement("termitesmorte",
                        from n in listpmort
                        where n.GetType().Equals(typeof(Termite))
                        select new XElement("termite",
                            new XAttribute("nom", n.Nom),
                            new XAttribute("pv", n.pv),
                            new XAttribute("strat", n.StategieCourante.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             ),
                                 new XElement("listeetapes",
                                   from et in n.ListEtape
                                   select new XElement("Etape",
                                   new XAttribute("tour", et.tour),
                                   new XAttribute("lieu", et.action),
                                     new XElement("coordonnees",
                                    new XAttribute("x", et.position.X),
                                    new XAttribute("y", et.position.Y)

                                )

                                   )

                            )
                        )
                     ) : null), new XElement("meteo",new XAttribute("etat",App.fourmilliereVM.meteo.Etat))
                     


                )
            );
            System.IO.Directory.CreateDirectory(@"C:\Fourmiliere");
            doc.Save(@"C:\Fourmiliere\save.xml");
      




        }

        private void btnCharge_Click(object sender, RoutedEventArgs e)
        {
                
            StringBuilder output = new StringBuilder();
            string xmlString = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
          
            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
                
              

                var myStream = new StreamReader(file, Encoding.UTF8, true, BufferSize);
              
                string line;
                    using (myStream)
                    {
                   
                   
                        while ((line=myStream.ReadLine() )!=null)
                        {
                            xmlString += line;
                        }
                      
                    }

                List<Nourriture> listn = new List<Nourriture>();
                List<Oeuf> listoe = new List<Oeuf>();
                List<Pheromone> listp = new List<Pheromone>();
                List<Guerriere> listg = new List<Guerriere>();
                List<Ouvriere> listo = new List<Ouvriere>();
                List<Ouvriere> listot = new List<Ouvriere>();
                List<Termite> listte = new List<Termite>();
                List<Guerriere> listgmorte = new List<Guerriere>();
                List<Ouvriere> listomorte = new List<Ouvriere>();
                List<Ouvriere> listotmorte = new List<Ouvriere>();
                List<Termite> listtemorte = new List<Termite>();
                List<Princesse> listeprincesse = new List<Princesse>();
                List<Princesse> listeprincessemorte = new List<Princesse>();
                Reine reine;
                XElement x = XElement.Load(new StringReader(xmlString));
                Fourmiliere fo;
                Meteo me;
                if (x != null && x.Element("fourmilieres") != null)
                {
                   fo= x.Element("fourmilieres")
                     .Elements("fourmiliere")
                     .Select(t => new Fourmiliere()
                     {
                         DimensionX = (int)t.Attribute("x"),
                         DimensionY = (int)t.Attribute("y"),
                         PersonnagesList = new ObservableCollection<PersonnageAbstrait>(),
                         ObjetList= new ObservableCollection<ObjetAbstrait>(),
                         AccesList = new List<AccesAbstrait>(),
                         ZoneList= new ObservableCollection<ZoneAbstrait>()
                     }).First();
                   
                   App.fourmilliereVM.DimensionX = fo.DimensionX;
                    App.fourmilliereVM.DimensionY = fo.DimensionY;
                    App.fourmilliereVM.RemoveAll();
                    App.fourmilliereVM.ZoneList = fo.ZoneList;
                    App.fourmilliereVM.InitZones();
                   
                }
                if (x != null && x.Element("meteo") != null)
                {
                    me = x.Elements("meteo")
                      .Select(t => new Meteo()
                      {
                          Etat = (string)t.Attribute("etat"),
                          ListObservateur = new List<PersonnageAbstrait>()
                      }).First();
                
                }
                if (x != null && x.Element("nourritures") != null)
                {
                    listn = x.Element("nourritures")
                     .Elements("nourriture")
                     .Select(t => new Nourriture()
                     {
                         Nom = (string)t.Attribute("nom"),
                         Dureevie=(int)t.Attribute("duree"),
                         ListMorceaux = new List<MorceauNourriture>(t.Element("listeNourriture").Elements("morceau")
                         .Select(mo => new MorceauNourriture((string)mo.Attribute("nom"),
                             t.Element("listeNourriture").Elements("morceau").Elements("coordonnees")
                              .Select(co=> new Coordonnees((int)co.Attribute("x"),(int)co.Attribute("y"))).First())).ToList()),
                            //new Coordonnees(10, 10))).ToList()),
                        
                         Position = t.Element("listeNourriture").Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("oeufs") != null)
                {
                    listoe = x.Element("oeufs")
                     .Elements("oeuf")
                     .Select(t => new Oeuf()
                     {
                         Nom = (string)t.Attribute("nom"),
                         Dureevie = (int)t.Attribute("duree"),
                         Position = t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("termites") != null)
                {
                    listte = x.Element("termites")
                     .Elements("termite")
                     .Select(t => new Termite()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         zone=new BoutDeTerrain("default", t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"),(int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("termitesmorte") != null)
                {
                    listte = x.Element("termitesmorte")
                     .Elements("termite")
                     .Select(t => new Termite()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         //ListEtape = new ObservableCollection<Etape>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"))).ToList()),
                         Position = t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("reines") != null)
                {
                     reine = x.Element("reines")
                      .Elements("reine")
                      .Select(t => new Reine()
                      {
                          Nom = (string)t.Attribute("nom"),
                          pv=(int)t.Attribute("pv"),

                          StategieCourante = new Immobile((string)t.Attribute("strat")),
                          ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                          Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                      }).First();

                    App.fourmilliereVM.ChargerReine(reine);
              

                }
                if (x != null && x.Element("princesses") != null)
                {
                    listeprincesse = x.Element("princesses")
                     .Elements("princesse")
                     .Select(t =>  new Princesse()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         zone = new BoutDeTerrain("default", t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("princessesmorte") != null)
                {
                    listeprincessemorte = x.Element("princessesmorte")
                     .Elements("princessemorte")
                     .Select(t => new Princesse()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("Guerrieres") != null)
                {
                    listg = x.Element("Guerrieres")
                     .Elements("Guerriere")
                     .Select(t => new Guerriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         zone = new BoutDeTerrain("default", t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("Ouvrieres") != null)
                {
                    listo = x.Element("Ouvrieres")
                     .Elements("Ouvriere")
                     .Where(tr => tr.Attribute("transport").Value == "false")
                     .Select(t => new Ouvriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         zone = new BoutDeTerrain("default", t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()),
                         TransporteNourriture = (bool)t.Attribute("transport"),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                    listot = x.Element("Ouvrieres")
                     .Elements("Ouvriere")
                     .Where(tr => tr.Attribute("transport").Value == "true")
                     .Select(t => new Ouvriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         zone = new BoutDeTerrain("default", t.Element("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()),
                         TransporteNourriture = (bool)t.Attribute("transport"),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         Morceau = t.Elements("morceau").Select(m => new MorceauNourriture()
                         {
                             Nom = (string)m.Attribute("nom"),
                             Position = m.Elements("coordonnees")
                              .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                         }).First(),
                          
                         
                         StategieCourante = new Retour((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("reinesmorte") != null)
                {
                    reine = x.Element("reinesmorte")
                     .Elements("reine")
                     .Select(t => new Reine()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         StategieCourante = new Immobile((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                         .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     }).First();

                    App.fourmilliereVM.ChargerReine(reine);
                }
                if (x != null && x.Element("Guerrieresmorte") != null)
                {
                    listgmorte = x.Element("Guerrieresmorte")
                     .Elements("Guerriere")
                     .Select(t => new Guerriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("Ouvrieresmorte") != null)
                {
                    listomorte = x.Element("Ouvrieresmorte")
                     .Elements("Ouvriere")
                     .Where(tr => tr.Attribute("transport").Value == "false")
                     .Select(t => new Ouvriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         TransporteNourriture = (bool)t.Attribute("transport"),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         StategieCourante = new Normal((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                    listotmorte = x.Element("Ouvrieresmorte")
                     .Elements("Ouvriere")
                     .Where(tr => tr.Attribute("transport").Value == "true")
                     .Select(t => new Ouvriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                         pv = (int)t.Attribute("pv"),
                         TransporteNourriture = (bool)t.Attribute("transport"),
                         Maison = new Coordonnees((int)t.Attribute("maisonx"), (int)t.Attribute("maisony")),
                         Morceau = t.Elements("morceau").Select(m => new MorceauNourriture()
                         {
                             Nom = (string)m.Attribute("nom"),
                             Position = m.Elements("coordonnees")
                              .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                         }).First(),


                         StategieCourante = new Retour((string)t.Attribute("strat")),
                         ListEtape = new ObservableCollection<EtapeAbstraite>(t.Elements("listeetapes").Elements("Etape").Select(et => new Etape((int)et.Attribute("tour"), (string)et.Attribute("lieu"), (int)et.Element("coordonnees").Attribute("x"), (int)et.Element("coordonnees").Attribute("y"))).ToList()),
                         Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("pheromones") != null)
                {
                    listp = x.Element("pheromones")
                     .Elements("pheromone")
                     .Select(t => new Pheromone()
                     {
                         Nom = (string)t.Attribute("nom"),
                         Dureevie = (int)t.Attribute("duree"),
                         Position = t.Elements("coordonneess").Elements("coordonnees").Select(c => new Coordonnees()
                         {
                             X = (int)c.Attribute("x"),
                             Y = (int)c.Attribute("y"),
                            
                         }).First()
                     })
                     .ToList();

                    
                }

                listomorte.ForEach(App.fourmilliereVM.PersonnagesMortList.Add);
                listotmorte.ForEach(App.fourmilliereVM.PersonnagesMortList.Add);
                listgmorte.ForEach(App.fourmilliereVM.PersonnagesMortList.Add);
                listtemorte.ForEach(App.fourmilliereVM.PersonnagesMortList.Add);
                listeprincessemorte.ForEach(App.fourmilliereVM.PersonnagesMortList.Add);
                listo.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listot.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listg.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listte.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listeprincesse.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listoe.ForEach(App.fourmilliereVM.ObjetList.Add);
                listp.ForEach(App.fourmilliereVM.ObjetList.Add);
                listn.ForEach(App.fourmilliereVM.ObjetList.Add);
                listo.ForEach(App.fourmilliereVM.meteo.ListObservateur.Add);
                listot.ForEach(App.fourmilliereVM.meteo.ListObservateur.Add);
                listg.ForEach(App.fourmilliereVM.meteo.ListObservateur.Add);

                listeprincesse.ForEach(App.fourmilliereVM.meteo.ListObservateur.Add);

                Dessine();
       
            }
        }

        public void initPlateau()
        {
            Plateau.ColumnDefinitions.Clear();
            Plateau.RowDefinitions.Clear();
            Plateau.Children.Clear();
            for (int i = 0; i < App.fourmilliereVM.DimensionX; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());

            }
            for (int j = 0; j < App.fourmilliereVM.DimensionY; j++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

    }
}
