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
using LibAbstraite.GestionPersonnage;

namespace LibAbstraite
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
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
            foreach (PersonnageAbstrait unPerso in App.fourmilliereVM.PersonnagesList)
            {
                Image img = new Image();
                Uri uri = new Uri("Roi.png", UriKind.Relative);
                img.Source = new BitmapImage(uri);
                
                Plateau.Children.Add(img);
                Grid.SetColumn(img, unPerso.X);
                Grid.SetRow(img, unPerso.Y);
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
