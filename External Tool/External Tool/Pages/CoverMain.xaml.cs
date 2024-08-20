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
//using System.Windows.Shapes;

namespace External_Tool.Pages
{
    /// <summary>
    /// Interaction logic for CoverMain.xaml
    /// </summary>
    public partial class CoverMain : UserControl
    {
        public CoverMain()
        {
            InitializeComponent();
        }
        private void NavigateToTabbedPage(int Index)
        { 
            TabbedMain tabbedMain = new TabbedMain();
            tabbedMain.SelectedTabIndex = Index;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            mainWindow.MainContent.Content = tabbedMain;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateToTabbedPage(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigateToTabbedPage(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigateToTabbedPage(2);
        }
    }
}
