using CSGO_Config_Generator.Data;
using CSGO_Config_Generator.Models;
using CSGO_Config_Generator.Tools;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSGO_Config_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Config Config;

        private readonly List<SettingPreview> Settings = new List<SettingPreview>();

        private readonly OpenFileDialog OFD = new OpenFileDialog()
        {
            Filter = "Config Files |*.cfg"
        };

        private readonly SaveFileDialog SFD = new SaveFileDialog()
        {
            Filter = "Config Files |*.cfg"
        };

        private SettingPreview SelectedSetting { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Config = new Config();

            Settings.AddCVars(Config.CVars);

            SettingsView.ItemsSource = Settings;

            SettingsView.Items.Refresh();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (OFD?.ShowDialog() ?? false)
            {
                Config.Clear();

                Settings.Clear();

                Config.Read(OFD.FileName);

                Settings.AddCVars(Config.CVars);

                SettingsView.Items.Refresh();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SFD?.ShowDialog() ?? false)
            {
                Config.Write(SFD.FileName);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CVar cvar = new CVar(null, 0);

            Config.Add(cvar);

            Settings.AddCVars(cvar);

            SettingsView.Items.Refresh();
        }

        private void SettingsView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbItem = (ListBoxItem)ItemsControl.ContainerFromElement(SettingsView, (DependencyObject)e.OriginalSource);

            if (lbItem != null)
            {
                SelectedSetting = (SettingPreview)lbItem.Content;

                switch (e.ChangedButton)
                {
                    case MouseButton.Left:
                        SelectedSetting.IsNameReadOnly = SelectedSetting.IsValueReadOnly = false;
                        break;

                    case MouseButton.Right:
                        SelectedSetting.IsNameReadOnly = SelectedSetting.IsValueReadOnly = true;
                        break;
                }
            }
        }
    }
}
