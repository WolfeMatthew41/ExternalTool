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
//using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using External_Tool.Pages;

namespace External_Tool.Popups
{
    /// <summary>
    /// Interaction logic for FolderSelectionWindow.xaml
    /// </summary>
    public partial class FolderSelectionWindow : Window
    {

        internal List<string> currentList = new List<string>();

        public Rename renameWindow { get; set; }

        private void PathList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemovePathButton.Opacity = 1;
        }

        private void FolderPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FolderPathBox.Text.Equals("", StringComparison.Ordinal))
            {
                AddPathButton.Opacity = 0.5;
            }
            else
            {
                AddPathButton.Opacity = 1;
            }
        }

        private void OnAddButton(object sender, RoutedEventArgs e)
        {
            //Checking if the button is allowed to be pressed or not
            if (AddPathButton.Opacity < 1)
            {
                return;

            }
            //Check if the text is a valid folder path and leave if it isn't one
            if (!Directory.Exists(FolderPathBox.Text))
            {
                return;
            }

            //Check if it exists in the list already
            if (currentList.Contains(FolderPathBox.Text))
            {
                return;
            }

            //Add the text to the folder path list
            currentList.Add(FolderPathBox.Text);
            UpdateList();

            //Reset the Type-in box if it works
            FolderPathBox.Text = "";

            //Adding stuff does deselect stuff so should manage the opacity of the remove button here
            RemovePathButton.Opacity = 0.5;

            if (currentList.Count > 0)
            {
                ConfirmButton.Opacity = 1;
            }
        }

        private void OnRemoveButton(object sender, RoutedEventArgs e)
        {
            //Checking if the button is allowed to be pressed or not
            if (RemovePathButton.Opacity < 1)
            {
                return;
            }

            //PathList.Items.RemoveAt(PathList.Items.IndexOf(PathList.SelectedItem));
            currentList.RemoveAt(PathList.Items.IndexOf(PathList.SelectedItem));
            UpdateList();

            RemovePathButton.Opacity = 0.5;

            if (currentList.Count == 0)
            {
                ConfirmButton.Opacity = 0.5;
            }
        }

        private void OnAddFolderListButton(object sender, RoutedEventArgs e) 
        {
            //Check if the folder list box is empty
            if (FolderListNameBox.Text.Equals("", StringComparison.Ordinal))
            {
                return;
            }

            //Check if that name exists already in the list
            if (FolderListSelector.Items.Cast<object>().Any(item => item.ToString() == FolderListNameBox.Text))
            {
                return;
            }

            //Add to the list and create the JSON file
            FolderListSelector.Items.Add(FolderListNameBox.Text);

            string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                                 "Resources\\FolderList\\") + FolderListNameBox.Text + ".json";

            using (File.CreateText(jsonFile)) { }

            renameWindow.UpdateFolderNameListSelection();
        }

        private void FolderListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the JSON file based on the selection
            string folderListSelection = FolderListSelector.SelectedItem as string;

            string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                                 "Resources\\FolderList\\") + folderListSelection + ".json";

            if (!File.Exists(jsonFile))
            {
                MessageBox.Show(jsonFile);
                return;
            }

            //Use the JSON file and add the list of folder paths to the current list
            string json = File.ReadAllText(jsonFile);

            currentList = JsonConvert.DeserializeObject<List<string>>(json);

            //If the list was empty, it will be null
            if (currentList == null)
            { 
                currentList = new List<string>();
            }

            //Update the list
            UpdateList();

        }

        private void OnOkButton(object sender, RoutedEventArgs e)
        {
            //Checking if the button is allowed to be pressed or not
            if (ConfirmButton.Opacity < 1)
            {
                return;
            }

            //Get JSON file based on selection
            string folderListSelection = FolderListSelector.SelectedItem as string;

            string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                         "Resources\\FolderList\\") + folderListSelection + ".json";

            if (!File.Exists(jsonFile))
            {
                return;
            }

            //Write list to that file completely overwriting it
            string jsonConvert = JsonConvert.SerializeObject(currentList);

            File.WriteAllText(jsonFile, jsonConvert);



            /* Adjusting the main windows list
            main.folderList.Clear();
            main.fileGroups.Clear();

            main.folderList = currentList;

            main.fileGroups = main.LoadSlideShow();
            main.fileGroups = main.RandomSort(main.fileGroups);
            main.mainIndex = 0;
            main.innerIndex = 0;
            main.UpdateSetup();
            */
            Close();
        }

        private void OnCancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateList()
        {
            PathList.ClearValue(ItemsControl.ItemsSourceProperty);
            PathList.ItemsSource = currentList;
        }

        private void StartUp()
        {
            //Get a list of the json files in the directory
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                             "Resources\\FolderList");

            string[] jsonFiles = Directory.GetFiles(filePath, $"*{".json"}");
            List<string> jsonFileNames = jsonFiles.Select(Path.GetFileNameWithoutExtension).ToList();

            //Add each of the files into the folder list
            foreach (string file in jsonFileNames)
            {
                FolderListSelector.Items.Add(file);
            }
        }

        public FolderSelectionWindow()
        {
            InitializeComponent();

            StartUp();
        }


    }
}
