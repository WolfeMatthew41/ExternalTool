using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
//using System.Windows.Shapes;
using External_Tool.Popups;
using External_Tool.Templates;
using System.IO;
using Newtonsoft.Json;

namespace External_Tool.Pages
{
    /// <summary>
    /// Interaction logic for Rename.xaml
    /// </summary>
    public partial class Rename : Page
    {

        public class FileNameContainer
        {
            public string FileName { get; set; }
            public string BaseName { get; set; }
        }
        private void OnApply_Click(object sender, RoutedEventArgs e)
        {
            if (FileList.Items.Count == 0)
            {
                return;
            }

            List<FileNameContainer> fileNameList = new List<FileNameContainer>();

            //Get the save list started by checking if the base json file exists
            string folderPath = FolderListSelector.Text;

            string jsonPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                 "Resources\\Rename\\" + folderPath + ".json");

            if (File.Exists(jsonPath))
            {
                //Load the list to our running list
                string json = File.ReadAllText(jsonPath);

                fileNameList = JsonConvert.DeserializeObject<List<FileNameContainer>>(json);

                if (fileNameList == null)
                { 
                    fileNameList = new List<FileNameContainer>();
                }
            }


            foreach (FileItemTemplate file in FileList.Items) 
            {
                if (!File.Exists(file.FilePath))
                {
                    continue;
                }
                //Add to our list to save
                //Should check to see if it exists already by current name
                bool isFound = false;
                int index = 0;

                for (int i = 0; i < fileNameList.Count; i++)
                {
                    if (fileNameList[i].FileName == file.CurrentFileName.Text)
                    {
                        isFound = true;
                        index = i;
                        break;
                    }
                }

                if (isFound)
                {
                    fileNameList[index].BaseName = file.BaseFileNameBox.Text;
                    fileNameList[index].FileName = file.NamingRuleList.Text + file.BaseFileNameBox.Text;
                }
                else
                {
                    FileNameContainer fileName = new FileNameContainer();
                    fileName.BaseName = file.BaseFileNameBox.Text;
                    fileName.FileName = file.NamingRuleList.Text + file.BaseFileNameBox.Text;
                    fileNameList.Add(fileName);
                }

                //Rename the file using the file path stored
                string newName = file.NamingRuleList.Text + file.BaseFileNameBox.Text + file.FileExtensionText.Text;

                string newFilePath = Path.Combine(Path.GetDirectoryName(file.FilePath), newName);

                File.Move(file.FilePath, newFilePath);
            }

            //Use the list to save to save to a json
            //Create or save to the list
            //Create a file using the folder list name
            if (!File.Exists(jsonPath))
            {
                using (File.CreateText(jsonPath)) { }
            }
            string jsonFile = JsonConvert.SerializeObject(fileNameList);

            File.WriteAllText(jsonPath, jsonFile);


            UpdateFileList();
        }

        private void FolderSelectionClick(object sender, RoutedEventArgs e)
        {
            FolderSelectionWindow selectionWindow = new FolderSelectionWindow();
            //Call any additional functions or set variables here before the window loads
            selectionWindow.renameWindow = this;

            selectionWindow.ShowDialog();
            
        }

        private void NamingConventionClick(object sender, RoutedEventArgs e)
        {
            NamingConventionRules namingWindow = new NamingConventionRules();
            //Call any additional functions or set variables here before the window loads
            namingWindow.renameWindow = this;

            namingWindow.ShowDialog();
            
        }
        private void NamingRuleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFileList();
        }

        private void FolderListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFileList();
        }

        public void UpdateFolderNameListSelection()
        {
            FolderListSelector.Items.Clear();
            NamingRuleList.Items.Clear();

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

        public class FileNameObj
        {
            public string Rule { get; set; }
            public string Extension { get; set; }
        }

        private void UpdateFileList()
        { 
            FileList.Items.Clear();

            int newSpot = 0;

            //First get the list of folder paths to be looking into
            string folderListSelection = FolderListSelector.SelectedItem as string;

            string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                                 "Resources\\FolderList\\") + folderListSelection + ".json";

            if (!File.Exists(jsonFile))
            {
                //MessageBox.Show(jsonFile);
                return;
            }

            string json = File.ReadAllText(jsonFile);

            List<string> folderPaths = JsonConvert.DeserializeObject<List<string>>(json);

            if (folderPaths == null)
            {
                return;
            }

            //Next go through each folder path
            foreach (string folderPath in folderPaths)
            {
                //Get all files within the folder path and go through each item
                string[] fileList = Directory.GetFiles(folderPath);

                foreach (string file in fileList)
                {
                    //Check if file exists
                    if (!File.Exists(file))
                    {
                        return;
                    }

                    //Set base parameters based on the file name
                    string fileExtension = Path.GetExtension(file);
                    bool isNew = true;
                    string currentName = Path.GetFileNameWithoutExtension(file);
                    string baseName = "";

                    //Need to get the rules list for the file extension
                    string namingListSelection = NamingRuleList.SelectedItem as string;

                    jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                     "Resources\\NamingConventionRules\\") + namingListSelection + ".json";

                    if (!File.Exists(jsonFile))
                    {
                        //MessageBox.Show(jsonFile);
                        return;
                    }

                    json = File.ReadAllText(jsonFile);

                    List<FileNameObj> entireRulesList = JsonConvert.DeserializeObject<List<FileNameObj>>(json);

                    if (entireRulesList == null)
                    {
                        return;
                    }

                    int found = 0;

                    List<string> rulesList = new List<string>();

                    foreach (FileNameObj rule in entireRulesList)
                    {
                        if (rule.Extension == fileExtension)
                        {
                            rulesList.Add(rule.Rule);
                            found++;
                            continue;
                        }
                        else if (found >= 1)
                        {
                            break;
                        }

                    }

                    //Check JSON file if it exists in the list
                    string jsonPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                     "Resources\\Rename\\" + FolderListSelector.Text + ".json");

                    List<FileNameContainer> fileNameList = new List<FileNameContainer>();

                    bool isFound = false;
                    int index = 0;

                    if (File.Exists(jsonPath))
                    {
                        string renameJson = File.ReadAllText(jsonPath);

                        fileNameList = JsonConvert.DeserializeObject<List<FileNameContainer>>(renameJson);

                        if (fileNameList == null)
                        {
                            fileNameList = new List<FileNameContainer>();
                        }
                    }
                    for (int i = 0; i < fileNameList.Count; i++)
                    {
                        if (fileNameList[i].FileName == currentName)
                        {
                            isFound = true;
                            index = i;
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        isNew = true;
                        baseName = currentName;
                    }
                    else 
                    {
                        isNew = false;
                        baseName = fileNameList[index].BaseName;
                    }

                    //Set the file in the item listing
                    FileItemTemplate newItem = new FileItemTemplate();

                    newItem.ConstructItem(file, fileExtension, isNew, currentName, baseName, rulesList);

                    if (isNew)
                    {
                        FileList.Items.Insert(newSpot, newItem);
                        newSpot++;
                    }
                    else 
                    { 
                        FileList.Items.Add(newItem);
                    }
                }
            }
        }



        public Rename()
        {
            InitializeComponent();

            UpdateFolderNameListSelection();

            UpdateFileList();
        }

    }
}
