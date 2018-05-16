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
using System.Text.RegularExpressions;
using Terrarium;

namespace TerrariumWindow
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        public PopupWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int teller = 6; teller < 21; teller++)
            {
                cmb_Size.Items.Add(teller);
            }
            cmb_Size.SelectedIndex = 0;
            Reset();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            var terrariumSize = int.Parse(cmb_Size.Text) * int.Parse(cmb_Size.Text);

            var window = new StartupWindow((int)cmb_Size.SelectedValue, SliderPlants.Value, SliderHerbis.Value, SliderCarnis.Value, SliderHumans.Value);
            window.Show();
            this.Close();
        }

        private void Reset()
        {
            SliderPlants.Value = 0;
            SliderHerbis.Value = 0;
            SliderCarnis.Value = 0;
            SliderHumans.Value = 0;
        }

        private double som = 100;

        public void Maximumberekenen()
        {
            SliderPlants.Maximum = som - SliderHerbis.Value - SliderCarnis.Value - SliderHumans.Value;
            SliderHerbis.Maximum = som - SliderPlants.Value - SliderCarnis.Value - SliderHumans.Value;
            SliderCarnis.Maximum = som - SliderHerbis.Value - SliderPlants.Value - SliderHumans.Value;
            SliderHumans.Maximum = som - SliderHerbis.Value - SliderCarnis.Value - SliderPlants.Value;
        }

        private void SliderPlants_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelstatusPlant.Content = SliderPlants.Value + "%";
            Maximumberekenen();
        }

        private void SliderHerbis_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelstatusHerbis.Content = SliderHerbis.Value + "%";
            Maximumberekenen();
        }

        private void SliderCarnis_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelstatusCarnis.Content = SliderCarnis.Value + "%";
            Maximumberekenen();
        }

        private void SliderHumans_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelstatusHumans.Content = SliderHumans.Value + "%";
            Maximumberekenen();
        }
    }
}