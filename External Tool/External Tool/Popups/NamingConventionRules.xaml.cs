using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Microsoft.Win32;
using External_Tool.Pages;

namespace External_Tool.Popups
{
    /// <summary>
    /// Interaction logic for NamingConventionRules.xaml
    /// </summary>
    /// 


    public partial class NamingConventionRules : Window
    {

        public class FileNameObj 
        {
            public string Rule { get; set; }
            public string Extension { get; set; }
        }

        internal List<FileNameObj> FileNamingRulesList = new List<FileNameObj>();

        public Rename renameWindow { get; set; }

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

            //currentList = JsonConvert.DeserializeObject<List<string>>(json);

            //Update the list
            PopulateFileNamingList(JsonConvert.DeserializeObject<List<string>>(json));

            UpdateList();

        }

        private void OnAddNamingListButton(object sender, RoutedEventArgs e)
        {
            //Check if the folder list box is empty
            if (FileExtensionRuleName.Text.Equals("", StringComparison.Ordinal))
            {
                return;
            }

            //Check if that name exists already in the list
            if (NamingRuleList.Items.Cast<object>().Any(item => item.ToString() == FileExtensionRuleName.Text))
            {
                return;
            }

            //Add to the list and create the JSON file
            NamingRuleList.Items.Add(FileExtensionRuleName.Text);

            string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                                 "Resources\\NamingConventionRules\\") + FileExtensionRuleName.Text + ".json";

            using (File.CreateText(jsonFile)){ }

            renameWindow.UpdateFolderNameListSelection();
        }

        private void SaveJSONFile(string jsonFilePath)
        {
            string json = JsonConvert.SerializeObject(FileNamingRulesList);

            File.WriteAllText(jsonFilePath, json);
        }

        private void NamingRuleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NamingRuleList.SelectedItem != null)
            {
                ExportButton.Opacity = 1;
                OkayButton.Opacity = 1;
            }
            else 
            {
                ExportButton.Opacity = 0.5;
                OkayButton.Opacity = 0.5;
            }

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

            //currentList = JsonConvert.DeserializeObject<List<string>>(json);

            //Update the list
            PopulateFileNamingList(JsonConvert.DeserializeObject<List<string>>(json));

            UpdateList();
        }

        private void OnImportButton(object sender, RoutedEventArgs e)
        {
            //Find json file through file browser
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Json files (*.json,)|*.json;|All files (*.*)|*.*";

            string filePath = "";

            if (fileDialog.ShowDialog() == true)
            {
                filePath = fileDialog.FileName;
            }
            else 
            {
                return;
            }

            //Check to see if the json file is compatible with the list
            if (filePath == "")
            {
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                List<FileNameObj> testDeserialize = JsonConvert.DeserializeObject<List<FileNameObj>>(json);
            }
            catch (Exception ex)
            { 
                MessageBox.Show($"That JSON File does not work with this list! {ex.Message}");
                return;
            }

            //Add a new list and save this json file to it
            //Check if that name exists already in the list
            if (NamingRuleList.Items.Cast<object>().Any(item => item.ToString() == Path.GetFileNameWithoutExtension(filePath)))
            {
                //If it exists, we should just overwrite the current list
                string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                      "Resources\\NamingConventionRules\\") + Path.GetFileNameWithoutExtension(filePath) + ".json";

                File.WriteAllText(jsonFile, File.ReadAllText(filePath));
            }
            else 
            {
                //We create the file and write to it
                NamingRuleList.Items.Add(Path.GetFileNameWithoutExtension(filePath));

                string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                      "Resources\\NamingConventionRules\\") + Path.GetFileNameWithoutExtension(filePath) + ".json";

                using (File.CreateText(jsonFile)) { }

                File.WriteAllText(jsonFile, File.ReadAllText(filePath));
            }

        }

        private void OnExportButton(object sender, RoutedEventArgs e)
        {
            if (ExportButton.Opacity != 1) 
            {
                return;
            }

            //Create the file path and JSON file
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "JSON|*.json";
            saveFile.Title = "Save The Naming Convetion Rules";
            saveFile.ShowDialog();

            if (saveFile.FileName == "")
            {
                return;
            }

            using (File.CreateText(saveFile.FileName)) { }

            //Save content to that file
            SaveJSONFile(saveFile.FileName);
        }

        private void OnOkayButton(object sender, RoutedEventArgs e)
        {
            if (OkayButton.Opacity != 1)
            {
                return;
            }

            string namingListSelection = NamingRuleList.SelectedItem as string;

            string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                     "Resources\\NamingConventionRules\\") + namingListSelection + ".json";

            SaveJSONFile(jsonFile);

            Close();
        }

        private void OnCancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PopulateFileNamingList(List<string> folderPaths)
        {

            FileNamingRulesList.Clear();

            //Check for existing file extension rules list selection
            if (NamingRuleList.SelectedItem != null) 
            {
                //Add the selected rules list into the list

                string namingListSelection = NamingRuleList.SelectedItem as string;

                string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                                     "Resources\\NamingConventionRules\\") + namingListSelection + ".json";

                string json = File.ReadAllText(jsonFile);

                FileNamingRulesList = JsonConvert.DeserializeObject<List<FileNameObj>>(json);

                if (FileNamingRulesList == null)
                {
                    FileNamingRulesList = new List<FileNameObj>();
                }
            }

            //Go through the folder paths and add the unique file extensions to the list
            //Each Rule should be left blank
            foreach (string path in folderPaths)
            {
                //Get all the files in the directory
                string[] files = Directory.GetFiles(path);

                foreach (string file in files) 
                { 
                    //Get the file extension
                    FileInfo fileInfo = new FileInfo(file);

                    string ext = fileInfo.Extension;

                    //Check if it already is in the list
                    if (CheckListByExtension(ext))
                    {
                        continue;
                    }
                    FileNameObj temp = new FileNameObj();
                    temp.Extension = ext;
                    temp.Rule = "";
                    //Add to the list
                    FileNamingRulesList.Add(temp);
                }
            }
        }

        private bool CheckListByExtension(string fileType)
        {
            foreach (FileNameObj file in FileNamingRulesList)
            {
                if (file.Extension == fileType)
                {
                    return true;
                }

            }
            return false;
        }

        private void OnSetRuleButton(object sender, RoutedEventArgs e)
        {
            if (SetRuleButton.Opacity != 1)
            {
                return;
            }

            FileNamingRulesList[FileNamingList.SelectedIndex].Rule = FileExtensionRuleBox.Text;

            UpdateList();
        }

        private void OnAddRuleButton(object sender, RoutedEventArgs e)
        {
            if (AddRuleButton.Opacity != 1)
            {
                return;
            }

            FileNameObj file = new FileNameObj();
            file.Extension = FileNamingRulesList[FileNamingList.SelectedIndex].Extension;
            file.Rule = "";

            FileNamingRulesList.Insert(FileNamingList.SelectedIndex, file);

            UpdateList();
        }

        private void OnRemoveRuleButton(object sender, RoutedEventArgs e)
        {
            if (RemoveRuleButton.Opacity != 1)
            {
                return;
            }

            //Check for duplicate extension here to completely remove it from list
            //Otherwise just set the rule to blank
            bool duplicateFound = false;
            int count= 0;

            foreach (FileNameObj file in FileNamingRulesList) 
            {
                if (file.Extension == FileNamingRulesList[FileNamingList.SelectedIndex].Extension)
                {
                    count++;
                }
                else if (count == 1)
                {
                    break;
                }

                if (count == 2)
                {
                    duplicateFound = true;
                    break;
                }
            }

            if (duplicateFound)
            {
                FileNamingRulesList.RemoveAt(FileNamingList.SelectedIndex);
            }
            else 
            {
                FileNamingRulesList[FileNamingList.SelectedIndex].Rule = "";
            }

            UpdateList();
        }

        private void FileExtensionRuleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FileNamingList.SelectedIndex >= 0)
            {
                if (FileExtensionRuleBox.Text.Length > 0)
                {
                    SetRuleButton.Opacity = 1;
                    return;
                }
            }

            SetRuleButton.Opacity = 0.5;
        }

        private void FileNamingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selection = "";

            if (FileNamingList.SelectedIndex >= 0)
            {
                selection = FileNamingRulesList[FileNamingList.SelectedIndex].Extension;

                FileExtensionRuleBox.Text = FileNamingRulesList[FileNamingList.SelectedIndex].Rule;

                AddRuleButton.Opacity = 1;
                RemoveRuleButton.Opacity = 1;
            }
            else 
            {
                AddRuleButton.Opacity = 0.5;
                RemoveRuleButton.Opacity = 0.5;
            }

            FileExtensionText.Text = selection;
        }

        private void UpdateList()
        {
            FileNamingList.Items.Clear();

            foreach (FileNameObj file in FileNamingRulesList)
            { 
                Grid grid = new Grid();
                grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                ColumnDefinition column1 = new ColumnDefinition();
                column1.Width = new GridLength(1, GridUnitType.Star);

                ColumnDefinition column2 = new ColumnDefinition();
                column2.Width = new GridLength(1, GridUnitType.Star);

                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);

                TextBlock ruleBlock = new TextBlock();
                ruleBlock.HorizontalAlignment = HorizontalAlignment.Center;
                ruleBlock.Text = file.Rule;
                //ruleBlock.Text = "Test";

                Grid.SetColumn(ruleBlock, 0);

                grid.Children.Add(ruleBlock);
                //Grid.SetColumn(ruleBlock, 0);

                TextBlock extBlock = new TextBlock();
                extBlock.HorizontalAlignment = HorizontalAlignment.Center;
                extBlock.Text = file.Extension;

                Grid.SetColumn(extBlock, 1);

                grid.Children.Add(extBlock);
                //Grid.SetColumn(extBlock, 1);

                FileNamingList.Items.Add(grid);
            }
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

            //Do this again but for the naming convention rules
            filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                             "Resources\\NamingConventionRules");

            jsonFiles = Directory.GetFiles(filePath, $"*{".json"}");

            jsonFileNames = jsonFiles.Select(Path.GetFileNameWithoutExtension).ToList();

            foreach (string file in jsonFileNames)
            {
                NamingRuleList.Items.Add(file);
            }


        }

        public NamingConventionRules()
        {
            InitializeComponent();

            StartUp();
        }

    }
}
