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

namespace External_Tool.Templates
{
    /// <summary>
    /// Interaction logic for FileItemTemplate.xaml
    /// </summary>
    public partial class FileItemTemplate : UserControl
    {
        public string FilePath { get; set; } = "";

        public void ConstructItem(string FullFilePath, string FileExtension, bool IsNew, string CurrentName, string BaseName, List<string> RulesList)
        {
            FilePath = FullFilePath;

            FileExtensionText.Text = FileExtension;

            if (IsNew)
            {
                NewText.Visibility = Visibility.Visible;
            }
            else 
            {
                NewText.Visibility = Visibility.Hidden;
            }

            CurrentFileName.Text = CurrentName;

            BaseFileNameBox.Text = BaseName;

            foreach (string Rule in RulesList)
            {
                NamingRuleList.Items.Add(Rule);
            }
            NamingRuleList.SelectedIndex = 0;
        }

        public FileItemTemplate()
        {
            InitializeComponent();
        }
    }
}
