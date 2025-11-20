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
            
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
