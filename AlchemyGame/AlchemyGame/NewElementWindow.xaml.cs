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

namespace AlchemyGame.ViewModel
{
    /// <summary>
    /// Interaction logic for NewElementWindow.xaml
    /// </summary>
    public partial class NewElementWindow : Window
    {
        public NewElementWindow(string name, string iconPath)//ovo ce da primi kao parametre
        {
            InitializeComponent();
            ElementNameText.Text = name;
            //ElementIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.Relative));
            if (!string.IsNullOrEmpty(iconPath))
            {
                try
                {
                    ElementIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.Relative));
                }
                catch
                {
                    // fallback in case image not found
                    ElementIcon.Source = null;
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
