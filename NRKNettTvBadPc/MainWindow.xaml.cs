using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using NRKNettTvBadPc.Models;

namespace NRKNettTvBadPc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ReadArguments();

        }

        public void ReadArguments()
        {
            if (String.IsNullOrEmpty(Properties.Settings.Default.VLC))
            {
                var dialog = new OpenFileDialog();

                dialog.DefaultExt = ".exe";
                dialog.Title = "Velg Avspiller (.exe)";

                dialog.ShowDialog();

                Properties.Settings.Default.VLC = dialog.FileName;
                Properties.Settings.Default.Save();
            }

            var args = Environment.GetCommandLineArgs();

            if (args.Length < 2)
            {
                return;
            }

            var path = args[1];

            var playlist = Playlist.FromFile(path);

            ListResolutions.ItemsSource = playlist.Items.Reverse();

            ListResolutions.SelectedIndex = 0;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ReadArguments();
        }

        private void ButtonPlayFeed_OnClick(object sender, RoutedEventArgs e)
        {
            PlaySelectedFile();
        }

        private void PlaySelectedFile()
        {
            if (ListResolutions.SelectedItem == null) return;

            var selectedItem = (PlaylistItem) ListResolutions.SelectedItem;

            var process = new Process
                              {
                                  StartInfo =
                                      {
                                          FileName = Properties.Settings.Default.VLC,
                                          Arguments = selectedItem.URL
                                      }
                              };

            process.Start();

            Application.Current.Shutdown();
        }
    }
}
