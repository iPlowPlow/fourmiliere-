using System;
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
                Dessine();
            }
           
        }
        public void Dessine()
        {
            initPlateau();
            //ImageBrush background = new ImageBrush();
            //Uri backuri = new Uri("Images/nourriture.png", UriKind.Relative);
            //background.ImageSource = new BitmapImage(backuri);
            //Plateau.Background = background;
            foreach (ZoneAbstrait zone in App.fourmilliereVM.ZoneList.ToList())
            {
                
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
                    byte opacity = Convert.ToByte(pheromone.Dureevie * (255 / Pheromone.DUREE_VIE_ORIGINALE));
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

                    Uri uri1 = new Uri("Images/Home.jpg", UriKind.Relative);

                    Plateau.Background = new ImageBrush(new BitmapImage(uri));


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
            App.fourmilliereVM.TourSuivant();
            Dessine();
        }
        private void btnAvance_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
            stopwatch.Start();
            Thread tt = new Thread(App.fourmilliereVM.Avance);
            tt.Start();
            Dessine();
            
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

            App.fourmilliereVM.Stop();
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
            Dessine();
        }

     
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<PersonnageAbstrait> listp=App.fourmilliereVM.PersonnagesList;
            ObservableCollection<ObjetAbstrait> listo = App.fourmilliereVM.ObjetList;
            var doc = new XDocument(
                new XElement("Simulation",
                    new XElement("fourmilieres",
                        new XElement("fourmiliere",
                        new XAttribute("x", App.fourmilliereVM.DimensionX),
                        new XAttribute("y", App.fourmilliereVM.DimensionY)
                         )
                     ), ((listo.Where(x => (x.GetType().Equals(typeof(Nourriture)))).Count() > 0) ?
                     new XElement("nourritures",
                        from n in listo
                        where n.GetType().Equals(typeof(Nourriture))
                        select new XElement("nourriture",
                            new XAttribute("nom", n.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                     ):null), ((listo.Where(x => (x.GetType().Equals(typeof(Oeuf)))).Count() > 0) ?
                      new XElement("oeufs",
                        from n in listo
                        where n.GetType().Equals(typeof(Oeuf))
                        select new XElement("oeuf",
                            new XAttribute("nom", n.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                      ):null),
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
                        from n in listp
                        where n.GetType().Equals(typeof(Ouvriere))
                        select new XElement("Ouvriere",
                            new XAttribute("nom", n.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                     ):null), ((listp.Where(x => (x.GetType().Equals(typeof(Guerriere)))).Count() > 0) ?
                        new XElement("Guerrieres",
                        from n in listp
                        where n.GetType().Equals(typeof(Guerriere))
                        select new XElement("Guerriere",
                            new XAttribute("nom", n.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                     ):null), ((listp.Where(x => (x.GetType().Equals(typeof(Reine)))).Count() > 0) ?
                        new XElement("reines",
                        from n in listp
                        where n.GetType().Equals(typeof(Reine))
                        select new XElement("reine",
                            new XAttribute("nom", n.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                     ) : null), ((listp.Where(x => (x.GetType().Equals(typeof(Termite)))).Count() > 0) ?
                        new XElement("termites",
                        from n in listp
                        where n.GetType().Equals(typeof(Termite))
                        select new XElement("termites",
                            new XAttribute("nom", n.Nom),
                            new XElement("coordonneess",
                                new XElement("coordonnees",
                                    new XAttribute("x", n.Position.X),
                                    new XAttribute("y", n.Position.Y)
                                )
                             )
                        )
                     ) : null)


                )
            );
            
            doc.Save("save.xml");
      




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
                   
                    // Insert code to read the stream here.
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
                List<Termite> listte = new List<Termite>();
                Reine reine;
                XElement x = XElement.Load(new StringReader(xmlString));
                Fourmiliere fo;
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
                }
                if (x != null && x.Element("nourritures") != null)
                {
                    listn = x.Element("nourritures")
                     .Elements("nourriture")
                     .Select(t => new Nourriture()
                     {
                         Nom = (string)t.Attribute("nom"),
                         Position = t.Element("coordonneess").Elements("coordonnees")
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
                          Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                      }).First();
                    App.fourmilliereVM.PersonnagesList.Add(reine);
                }
                if (x != null && x.Element("Guerrieres") != null)
                {
                    listg = x.Element("Guerrieres")
                     .Elements("Guerriere")
                     .Select(t => new Guerriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                          Position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }
                if (x != null && x.Element("Ouvrieres") != null)
                {
                    listo = x.Element("Ouvrieres")
                     .Elements("Ouvriere")
                     .Select(t => new Ouvriere()
                     {
                         Nom = (string)t.Attribute("nom"),
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

                
                listo.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listg.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listte.ForEach(App.fourmilliereVM.PersonnagesList.Add);
                listoe.ForEach(App.fourmilliereVM.ObjetList.Add);
                listp.ForEach(App.fourmilliereVM.ObjetList.Add);
                listn.ForEach(App.fourmilliereVM.ObjetList.Add);

              
                
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
