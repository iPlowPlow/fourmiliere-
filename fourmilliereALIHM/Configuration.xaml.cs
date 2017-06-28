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
using System.Windows.Shapes;

namespace fourmilliereALIHM
{
    /// <summary>
    /// Logique d'interaction pour Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        public Configuration()
        {
            InitializeComponent();
            DataContext = App.config;
        }
        private void Spawn_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (OuvriereProb != null && GuerriereProb != null && PrincesseProb != null)
            {
                Slider current = (Slider)sender;
                int OuvriereProba = Convert.ToInt32(OuvriereProb.Value);
                int GuerriereProba = Convert.ToInt32(GuerriereProb.Value);
                int PrincesseProba = Convert.ToInt32(PrincesseProb.Value);
                int totalSpawnvalue = OuvriereProba + GuerriereProba + PrincesseProba;

                if (totalSpawnvalue > 101)
                {
                    switch (current.Name)
                    {
                        case "OuvriereProb":
                            GuerriereProba -= ((totalSpawnvalue - 100) / 2);
                            PrincesseProba -= ((totalSpawnvalue - 100) / 2);
                            break;
                        case "GuerriereProb":
                            PrincesseProba -= ((totalSpawnvalue - 100) / 2);
                            OuvriereProba -= ((totalSpawnvalue - 100) / 2);
                            break;
                        case "PrincesseProb":
                            GuerriereProba -= ((totalSpawnvalue - 100) / 2);
                            OuvriereProba -= ((totalSpawnvalue - 100) / 2);
                            break;
                    }
                }
                else if (totalSpawnvalue < 99)
                {
                    switch (current.Name)
                    {
                        case "OuvriereProb":
                            GuerriereProba += ((100 - totalSpawnvalue) / 2);
                            PrincesseProba += ((100 - totalSpawnvalue) / 2);
                            break;
                        case "GuerriereProb":
                            PrincesseProba += ((100 - totalSpawnvalue) / 2);
                            OuvriereProba += ((100 - totalSpawnvalue) / 2);
                            break;
                        case "PrincesseProb":
                            GuerriereProba += ((100 - totalSpawnvalue) / 2);
                            OuvriereProba += ((100 - totalSpawnvalue) / 2);
                            break;
                    }
                }
                if (totalSpawnvalue == 99)
                {
                    OuvriereProba++;
                }
                if (totalSpawnvalue == 101)
                {
                    OuvriereProba--;
                }
                OuvriereProb.Value = OuvriereProba>0? OuvriereProba : 0;
                GuerriereProb.Value = GuerriereProba>0? GuerriereProba : 0;
                PrincesseProb.Value = PrincesseProba>0? PrincesseProba : 0;
            }
        }

        private void Weather_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PluieProb != null && BrouillardProb != null && ClairProb != null)
            {
                Slider current = (Slider)sender;
                int PluieProba = Convert.ToInt32(PluieProb.Value);
                int ClairProba = Convert.ToInt32(ClairProb.Value);
                int BrouillardProba = Convert.ToInt32(BrouillardProb.Value);
                int totalvalue = PluieProba + ClairProba + BrouillardProba;

                if (totalvalue > 101)
                {
                    switch (current.Name)
                    {
                        case "PluieProb":
                            BrouillardProba -= ((totalvalue - 100) / 2);
                            ClairProba -= ((totalvalue - 100) / 2);
                            break;
                        case "BrouillardProb":
                            ClairProba -= ((totalvalue - 100) / 2);
                            PluieProba -= ((totalvalue - 100) / 2);
                            break;
                        case "ClairProb":
                            BrouillardProba -= ((totalvalue - 100) / 2);
                            PluieProba -= ((totalvalue - 100) / 2);
                            break;
                    }
                }
                else if (totalvalue < 99)
                {
                    switch (current.Name)
                    {
                        case "PluieProb":
                            BrouillardProba += ((100 - totalvalue) / 2);
                            ClairProba += ((100 - totalvalue) / 2);
                            break;
                        case "BrouillardProb":
                            ClairProba += ((100 - totalvalue) / 2);
                            PluieProba += ((100 - totalvalue) / 2);
                            break;
                        case "ClairProb":
                            BrouillardProba += ((100 - totalvalue) / 2);
                            PluieProba += ((100 - totalvalue) / 2);
                            break;
                    }
                }
                if(totalvalue == 99)
                {
                    ClairProba++;
                }
                if(totalvalue == 101)
                {
                    ClairProba--;
                }
                PluieProb.Value = PluieProba>0? PluieProba :0;
                BrouillardProb.Value = BrouillardProba > 0 ? BrouillardProba :0;
                ClairProb.Value = ClairProba>0? ClairProba:0;
            }
        }
    }
}
