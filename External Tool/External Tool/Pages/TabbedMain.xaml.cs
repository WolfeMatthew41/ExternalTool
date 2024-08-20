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
    /// Interaction logic for TabbedMain.xaml
    /// </summary>
    public partial class TabbedMain : UserControl
    {
        public int SelectedTabIndex { get; set; } = 0;
        public TabbedMain()
        {
            InitializeComponent();
            Loaded += LoadPage;
            //LoadPage();
        }

        private void LoadPage(object sender, System.Windows.RoutedEventArgs e)
        { 
            MainTabControl.SelectedIndex = SelectedTabIndex;
        }
        //private void LoadPage()
        //{
        //    MainTabControl.SelectedIndex = SelectedTabIndex;
        //}
    }
}
