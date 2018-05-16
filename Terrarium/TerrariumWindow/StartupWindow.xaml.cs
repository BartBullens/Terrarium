using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Terrarium;

namespace TerrariumWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class StartupWindow : Window
    {
        private Terrarium.Terrarium terrariumCode = new Terrarium.Terrarium(0);
        private Organism[,] terrariumArray;
        private int day;
        private double[] startValues = new double[4] { 0, 0, 0, 0 };

        public StartupWindow(int sizeTerrarium, double numberOfPlants, double numberOfHerbivores, double numberOfCarnivores, double numberOfHumans)
        {
            // startwaarde voor firstday
            startValues[0] = numberOfPlants;
            startValues[1] = numberOfHerbivores;
            startValues[2] = numberOfCarnivores;
            startValues[3] = numberOfHumans;

            InitializeComponent();

            ComboBoxMenuStyle.Items.Add("ForestStyle");
            ComboBoxMenuStyle.Items.Add("JungleStyle");
            ComboBoxMenuStyle.Items.Add("OceanStyle");
            ComboBoxMenuStyle.Items.Add("PokemonStyle");
            ComboBoxMenuStyle.Items.Add("RoboticStyle");
            ComboBoxMenuStyle.SelectedIndex = 0;

            FillComboBox();
            // maak grid ten grootte van terrarium
            terrariumCode.Size = sizeTerrarium;
            CreateGrid(sizeTerrarium);

            // make array for the first day
            // display array for the first day
            FirstDay(startValues);
        }

        private void FillComboBox()
        {
            var number = 100;
            for (int i = 1; i <= number; i++)
            {
                ComboBoxSkipDays.Items.Add(i);
            }
            ComboBoxSkipDays.SelectedIndex = 0;
        }

        private void NextDay()
        {
            //dummy logboek
            Logboek logboek = new Logboek(terrariumCode);

            terrariumCode.NextDay(ref terrariumArray, ref logboek);

            // dag aanpassen in logboek
            day++;
            LabelDayCounter.Content = day.ToString();

            // dag 1 omhoog in 'to day method'
            Label_DayNumber.Content = day;
        }

        private void FirstDay(double[] startValues)
        {
            terrariumArray = terrariumCode.FirstDay(startValues);
            Display(terrariumArray);

            // dag resetten in logboek
            day = 1;
            LabelDayCounter.Content = day.ToString();

            // dag resetten for 'to day method'
            Label_DayNumber.Content = "1";

            // combobox selection terug op index 0
            ComboBoxSkipDays.SelectedIndex = 0;
        }

        private void ButtonNextDay_Click(object sender, RoutedEventArgs e)
        {
            NextDay();
            Display(terrariumArray);
        }

        private void Display(Organism[,] arrOrganism)
        {
            // clean slate grid
            Terrarium.Children.Clear();

            var image = new Image();

            for (int y = 1; y <= Terrarium.RowDefinitions.Count; y++)
            {
                for (int x = 1; x <= Terrarium.ColumnDefinitions.Count; x++)
                {
                    var organism = arrOrganism[y - 1, x - 1];

                    // background color toevoegen aan grid cell (wpf kent dit niet, dus een figuur met kleur toevoegen)
                    var background = new Rectangle();
                    if (organism is Terrain)
                    {
                        background.Fill = Brushes.SandyBrown;
                    }
                    else if (organism is Animal)
                    {
                        if (((Animal)organism).Sex == Sex.Female)
                        {
                            background.Fill = Brushes.Pink;
                        }
                        else
                        {
                            background.Fill = Brushes.Blue;
                        }
                    }
                    else if (organism is Plant)
                    {
                        background.Fill = Brushes.Green;
                    }
                    else if (organism is VolcanicEruption)
                    {
                        background.Fill = Brushes.Red;
                    }
                    // volledige uitzondering dat soort niet gevonden kan worden
                    else
                    {
                        background.Fill = Brushes.White;
                    }

                    string soort = (organism.GetType()).Name;

                    image = CreateImage(soort);
                    Grid.SetRow(image, x - 1);
                    Grid.SetColumn(image, y - 1);

                    Grid.SetRow(background, x - 1);
                    Grid.SetColumn(background, y - 1);

                    Terrarium.Children.Add(background);
                    Terrarium.Children.Add(image);
                }
            }
            CreateGridLines();
        }

        private Image CreateImage(string soort)
        {
            // hier wordt thema aangeduid dat in het menu gekozen is
            // zo wordt de juiste foto uit de juiste map gehaald
            var theme = string.Empty;

            foreach (MenuItem menuItem in MenuTheme.Items)
            {
                if (menuItem.IsChecked == true)
                {
                    theme = menuItem.Header.ToString();
                    break;
                }
            }

            Image image = new Image();
            ImageSource source = new BitmapImage(new Uri(@"Images/" + theme + "/" + soort + ".png", UriKind.RelativeOrAbsolute));
            image.Source = source;
            image.Stretch = Stretch.Fill;
            image.SnapsToDevicePixels = true;

            return image;
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem menuItem in MenuTheme.Items)
            {
                if (menuItem.IsChecked == true)
                {
                    menuItem.IsChecked = false;
                }
            }
            ((MenuItem)sender).IsChecked = true;
            Display(terrariumArray);

            //Links Theme choice to menustyle
            string stijl = ((MenuItem)sender).Header.ToString() + "Style";
            ComboBoxMenuStyle.Text = stijl;
        }

        private void CreateGridLines()
        {
            for (int row = 1; row <= Terrarium.RowDefinitions.Count; row++)
            {
                for (int column = 1; column <= Terrarium.ColumnDefinitions.Count; column++)
                {
                    var border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.Black;
                    Grid.SetRowSpan(border, row);
                    Grid.SetColumnSpan(border, column);
                    Terrarium.Children.Add(border);
                }
            }
        }

        private void CreateGrid(int sizeTerrarium)
        {
            for (int column = 0; column < sizeTerrarium; column++)
            {
                var columnDefinition = new ColumnDefinition();
                Terrarium.ColumnDefinitions.Add(columnDefinition);
            }
            for (int row = 0; row < sizeTerrarium; row++)
            {
                var rowDefinition = new RowDefinition();
                Terrarium.RowDefinitions.Add(rowDefinition);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the program?", "Closing", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            // close all open windows
            else
            {
                Environment.Exit(0);
            }
        }

        private void ButtonSkip_Click(object sender, RoutedEventArgs e)
        {
            var counter = (int)ComboBoxSkipDays.SelectedValue;
            for (int i = 0; i < counter; i++)
            {
                NextDay();
            }
            Display(terrariumArray);
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            FirstDay(startValues);
        }

        // method om data van één organisme waarop je klikt te weergeven
        private void Terrarium_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // bepaal vakje waarop je klikt

            double totalWidth = 0.0;
            double totalHeight = 0.0;

            int column = 0;
            int row = 0;

            var clickPoint = Mouse.GetPosition(Terrarium);
            //kolom

            foreach (var columnDefinition in Terrarium.ColumnDefinitions)
            {
                totalWidth += columnDefinition.ActualWidth;
                if (totalWidth >= clickPoint.X)
                {
                    break;
                }
                column++;
            }

            //rij
            foreach (var rowDefinition in Terrarium.RowDefinitions)
            {
                totalHeight += rowDefinition.ActualHeight;
                if (totalHeight >= clickPoint.Y)
                {
                    break;
                }
                row++;
            }

            // transparant array (switch row - column)
            var selectedOrganism = terrariumArray[column, row];

            var theme = string.Empty;

            foreach (MenuItem menuItem in MenuTheme.Items)
            {
                if (menuItem.IsChecked == true)
                {
                    theme = menuItem.Header.ToString();
                    break;
                }
            }

            var gender = "Not specified";
            if (selectedOrganism is Animal)
            {
                gender = ((Animal)selectedOrganism).Sex.ToString();
            }

            var dataOrganismWindow = new DataOrganism(selectedOrganism.GetType().Name, gender, selectedOrganism.Life, selectedOrganism.Position.xPosition,
                selectedOrganism.Position.yPosition, selectedOrganism.IsWalkable, theme);
            dataOrganismWindow.Show();
        }

        // make a new terrarium with new start values
        private void MenuNewTerrarium_Click(object sender, RoutedEventArgs e)
        {
            // eerst de method Window_Closing removen van de window zodat het geen message toont en programma volledig afsluit

            this.Closing -= Window_Closing;

            var popupWindow = new PopupWindow();
            popupWindow.Show();

            this.Close();
        }

        private void Button_MinPlusDay_Click(object sender, RoutedEventArgs e)
        {
            var dayNumber = Convert.ToInt32(Label_DayNumber.Content);
            var usedButton = ((Button)sender).Content.ToString();

            // bepalen welke knop er ingeduwd is en actie ondernemen
            switch (usedButton)
            {
                // << -10 dagen
                case "<<":
                    if ((dayNumber - day) >= 10)
                    {
                        dayNumber -= 10;
                    }
                    else
                    {
                        dayNumber = day;
                    }
                    break;
                // < -1 dag
                case "<":
                    if (dayNumber > day)
                    {
                        dayNumber--;
                    }
                    break;
                // > +1 dag
                case ">":
                    dayNumber++;
                    break;
                // >> +10 dagen
                case ">>":
                    dayNumber += 10;
                    break;

                default:
                    break;
            }
            Label_DayNumber.Content = dayNumber.ToString();
        }

        private void Button_GoToDay_Click(object sender, RoutedEventArgs e)
        {
            var counter = Convert.ToInt32(Label_DayNumber.Content) - day;
            for (int i = 0; i < counter; i++)
            {
                NextDay();
            }
            Display(terrariumArray);
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBoxMenuStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@"/LayoutStyles/" + ComboBoxMenuStyle.SelectedValue.ToString() + ".xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}