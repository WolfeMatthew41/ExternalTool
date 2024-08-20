using System;
using System.Collections.Generic;
using System.IO;
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
using System.Diagnostics;

namespace External_Tool.Pages
{
    /// <summary>
    /// Interaction logic for FFMpegIntegrate.xaml
    /// </summary>
    public partial class FFMpegIntegrate : Page
    {
        public FFMpegIntegrate()
        {
            InitializeComponent();
        }

        private string GetVideoCodecSetting()
        {
            string CodecSetting = "";

            switch (VideoCodecSettingsBox.SelectedIndex)
            {
                case 0:
                    CodecSetting = "-c:v libx264 -crf 23";
                    break;
                case 1:
                    CodecSetting = "-c:v libx265 -crf 28";
                    break;
                case 2:
                    CodecSetting = "-c:v libvpx-vp9 -b:v 2M";
                    break;
                case 3:
                    CodecSetting = "-c:v prores -profile:v 3";
                    break;
                case 4:
                    CodecSetting = "-c:v huffyuv";
                    break;
            }
            return CodecSetting;
        }

        private string GetPixelSetting()
        {
            string Setting = "-pix_fmt ";

            switch (VideoPixelBox.SelectedIndex)
            {
                case 0:
                    Setting += "yuv420p";
                    break;
                case 1:
                    Setting += "yuv422p";
                    break;
                case 2:
                    Setting += "yuv444p";
                    break;
                case 3:
                    Setting += "rgb24";
                    break;
                case 4:
                    Setting += "rgba"; //transparency
                    break;
                case 5:
                    Setting += "gray"; 
                    break;
                case 6:
                    Setting += "monow"; 
                    break;
            }
            return Setting;
        }

        private string GetFileType()
        {
            string Setting = "";

            switch (FileTypeBox.SelectedIndex)
            {
                case 0:
                    Setting += ".mp4";
                    break;
                case 1:
                    Setting += ".webm";
                    break;
                case 2:
                    Setting += ".mov";
                    break;
            }
            return Setting;
        }
       private bool CheckRenderSettings()
        {
            if (!Directory.Exists(InputLocationBox.Text))
            {
                MessageBox.Show("Enter a valid Input Folder Location!");
                return false;
            }

            if (double.TryParse(FrameRateBox.Text, out double number))
            {
                if (number <= 0)
                {
                    MessageBox.Show("Enter a valid Frame Rate");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Enter a valid Frame Rate");
                return false;
            }

            if (!(VideoCodecSettingsBox.SelectedIndex >= 0) || !(VideoPixelBox.SelectedIndex >= 0) || !(FileTypeBox.SelectedIndex >= 0))
            {
                MessageBox.Show("Make sure to choose all selections!");
                return false;
            }

            if (!(OutputNameBox.Text.Length > 0))
            {
                MessageBox.Show("Enter a name for the video!");
                return false;
            }

            if (!Directory.Exists(OutputLocationBox.Text))
            {
                OutputLocationBox.Text = InputLocationBox.Text;
                //MessageBox.Show("Enter a valid Output Folder Location!");
                //return false;
            }

            return true;
        }

        private void Render_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckRenderSettings()) 
            {
                return;
            }

            //Get image files from the directory
            var files = Directory.GetFiles(InputLocationBox.Text, "*.png").OrderBy(f => f).ToList();
            string inputFilePath = Path.Combine(InputLocationBox.Text, "input.txt");

            //Get frame rate
            double framerate;
            double.TryParse(FrameRateBox.Text, out framerate);
            framerate = 1 / framerate;

            using (StreamWriter writer = new StreamWriter(inputFilePath))
            { 
                foreach (var file in files) 
                {
                    writer.WriteLine($"file '{file}'");
                    writer.WriteLine($"duration {framerate}");
                }
            }

                //Render with ffmpeg
                string command = "ffmpeg " + "-f concat -safe 0 -i " + "input.txt"
                    + " -vsync vfr" + " " + GetPixelSetting() + " " + GetVideoCodecSetting() + " "
                    + OutputNameBox.Text + GetFileType();


            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                { 
                    WorkingDirectory = InputLocationBox.Text,
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true ,
                    RedirectStandardError = true ,
                    UseShellExecute = false ,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                { 
                    process.StartInfo = processStartInfo;
                    process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                    process.ErrorDataReceived += (sender, args) => Console.WriteLine("ERROR: " + args.Data);

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }
                File.Delete(inputFilePath);

                if (InputLocationBox.Text != OutputLocationBox.Text)
                {
                    File.Copy(Path.Combine(InputLocationBox.Text, OutputNameBox.Text + GetFileType()),
                    Path.Combine(OutputLocationBox.Text, OutputNameBox.Text + GetFileType()), true);

                    File.Delete(Path.Combine(InputLocationBox.Text, OutputNameBox.Text + GetFileType()));
                }

                

                MessageBox.Show("Render Complete!");

                //Open the output folder
                if (Directory.Exists(OutputLocationBox.Text))
                {
                    Process.Start("explorer.exe", OutputLocationBox.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encountered an issue: " + ex.Message);
            }
        }
    }
}
