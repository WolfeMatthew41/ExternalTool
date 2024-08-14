using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace External_Tool.Pages
{
    /// <summary>
    /// Interaction logic for TextureCompress.xaml
    /// </summary>
    public partial class TextureCompress : Page
    {


        private void TextureCompressing(string directory, string command, string textureName)
        {

            if (directory == "" || command == "" || textureName == "")
            {
                MessageBox.Show("One of the supplied details was not filled in correct!");
                return;
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = directory
            };

            Process process = new Process { StartInfo = processStartInfo };
            process.Start();

            process.StandardInput.WriteLine(command + " " + textureName);

            process.StandardInput.Close();

            process.WaitForExit();

            //MessageBox.Show(process.StandardOutput.ReadToEnd());

            process.Close();
        }

        private void TextureFileSelect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Texture files (*.png,)|*.png;|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == true)
            {
                textureFileBox.Text = fileDialog.FileName;
            }
        }

        private string GetTextureCompressCommand()
        {
            return "bc7enc";
        }

        private void TextureCompressButtonPress(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(textureFileBox.Text))
            {
                MessageBox.Show("Need a texture selected!");
                return;
            }

            string resourceDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                                 "Resources\\TextureCompression\\bc7enc");

            string destinationPath = Path.Combine(resourceDirectory, Path.GetFileName(textureFileBox.Text));

            try
            {
                File.Copy(textureFileBox.Text, destinationPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return;
            }

            TextureCompressing(resourceDirectory, GetTextureCompressCommand(), Path.GetFileName(textureFileBox.Text));

            //Transfer .DDS file to the Output folder
            string outputDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                                                "Output");
            string outputPath = Path.Combine(outputDirectory, Path.GetFileName(textureFileBox.Text)[..^3] + "dds");

            try
            {
                File.Copy(destinationPath[..^3] + "dds", outputPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} \nPerhaps bc7enc was not properly installed?");
                return;
            }

            //Clean up the files in the Resources folder
            string[] pngFiles = Directory.GetFiles(resourceDirectory, $"*{".png"}");
            string[] ddsFiles = Directory.GetFiles(resourceDirectory, $"*{".dds"}");

            foreach (string file in pngFiles)
            {
                File.Delete(file);
            }
            foreach (string file in ddsFiles)
            {
                File.Delete(file);
            }

            MessageBox.Show("Texture Compression Complete!");

            //Open the output folder
            if (Directory.Exists(outputDirectory))
            {
                Process.Start("explorer.exe", outputDirectory);
            }
        }
        public TextureCompress()
        {
            InitializeComponent();
        }
    }
}
