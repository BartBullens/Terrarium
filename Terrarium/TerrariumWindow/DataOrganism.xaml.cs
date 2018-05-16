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

namespace TerrariumWindow
{
    /// <summary>
    /// Interaction logic for DataOrganism.xaml
    /// </summary>
    public partial class DataOrganism : Window
    {
        public DataOrganism(string name, string gender, int life, int xLocation, int yLocation, bool isWalkable, string theme)
        {
            InitializeComponent();

            ImageSource source = new BitmapImage(new Uri("Images/" + theme + "/" + name + ".png", UriKind.RelativeOrAbsolute));
            ImageOrganism.Source = source;

            Label_Name.Content = name;
            Label_Gender.Content = gender;
            Label_LifePoints.Content = life.ToString();
            Label_Coordinates.Content = "x : " + (xLocation + 1).ToString() + " , y : " + (yLocation + 1).ToString();
            Label_IsWalkable.Content = isWalkable.ToString();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            try
            {
                base.OnDeactivated(e);
                this.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}