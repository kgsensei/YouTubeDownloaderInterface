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
using System.Windows.Shapes;

namespace YouTubeDownloaderInterface
{
    public class Globals
    {
        public static string download { get; set; } = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads";
        public static string watchURL { get; set; } = "none";
        public static bool audioOnly { get; set; } = false;
        public static bool playlist { get; set; } = false;
    }
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            downloadLocationBar.Text = Globals.download;
        }

        // URL input
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.watchURL = youtubelink.Text;
        }

        // Download button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string command = "/C yt-dlp --restrict-filenames --windows-filenames --ignore-errors " +
                "-o \"" + Globals.download + "\\%(title)s.%(ext)s\" ";
            if (Globals.playlist)
            {
                command += "--yes-playlist ";
            }
            if (Globals.audioOnly)
            {
                command += "--format bestaudio --extract-audio --audio-format mp3 --audio-quality 160K ";
            }
            command += Globals.watchURL;
            System.Diagnostics.Process.Start("CMD.exe", command);
        }

        // Download location input
        private void downloadLocationBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string path = e.Source.ToString();
            if (Directory.Exists(path))
            {
                //Globals.download = path;
            }
        }

        // Audio only checked
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Globals.audioOnly = true;
        }

        // Audio only unchecked
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Globals.audioOnly = false;
        }

        // Download playlist checked
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            Globals.playlist = true;
        }

        // Download playlist unchecked
        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            Globals.playlist = false;
        }
    }
}
