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
using LibMetier.GestionEnvironnements;
using LibMetier.GestionObjets;
using LibMetier.GestionPersonnages;
using System.Xml.Linq;

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
            foreach (Pheromone unePheromone in App.fourmilliereVM.PheromoneList)
            {
                byte opacity = Convert.ToByte(unePheromone.dureevie*10);
                Color color = Color.FromArgb(opacity, 0, 0, 255);
                Rectangle pheromoneBox = new Rectangle();
                pheromoneBox.Fill = new SolidColorBrush(color);
                Plateau.Children.Add(pheromoneBox);
                Grid.SetColumn(pheromoneBox, unePheromone.position.X);
                Grid.SetRow(pheromoneBox, unePheromone.position.Y);
            }
            foreach (Fourmis unefourmi in App.fourmilliereVM.FourmisList)
            {
                Image img = new Image();
                Uri uri = new Uri("Images/fourmi_ouvriere.png", UriKind.Relative);
                img.Source = new BitmapImage(uri);
                
                Plateau.Children.Add(img);
                Grid.SetColumn(img, unefourmi.X);
                Grid.SetRow(img, unefourmi.Y);
                //Coordonnees coordonnees = new Coordonnees(unefourmi.X, unefourmi.Y);
                //Pheromone unPheromone = new Pheromone("pheromone de fourmi", coordonnees);
                //App.fourmilliereVM.PheromoneList.Add(unPheromone);
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AProposWindows apx = new AProposWindows();
            apx.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.AjouterFourmis();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.fourmilliereVM.SupprimerFourmis();
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
                List<Guerriere> listg = new List<Guerriere>();
                List<Ouvriere> listo = new List<Ouvriere>();
                Reine reine;
                XElement x = XElement.Load(new StringReader(xmlString));
                if (x != null && x.Element("nourritures") != null)
                {
                    listn = x.Element("nourritures")
                     .Elements("nourriture")
                     .Select(t => new Nourriture()
                     {
                         Nom = (string)t.Attribute("nom"),
                         position = t.Element("coordonneess").Elements("coordonnees")
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
                         position = t.Element("coordonneess").Elements("coordonnees")
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
                          position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                      }).First();
                    
                }
                if (x != null && x.Element("Guerrieres") != null)
                {
                    listg = x.Element("Guerrieres")
                     .Elements("Guerriere")
                     .Select(t => new Guerriere()
                     {
                         Nom = (string)t.Attribute("nom"),
                          position = t.Elements("coordonneess").Elements("coordonnees")
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
                          position = t.Elements("coordonneess").Elements("coordonnees")
                          .Select(c => new Coordonnees((int)c.Attribute("x"), (int)c.Attribute("y"))).First()
                     })
                     .ToList();
                }

                App.fourmilliereVM.AjouteOuvriere(listo);


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
            for (int i = 0; i < App.fourmilliereVM.DimensionY; i++)
            {

                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
    }
}
