using CSGO_Config_Manager.Data;
using CSGO_Config_Manager.Models;
using CSGO_Config_Manager.Tools;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSGO_Config_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Config Config;
        private readonly List<VariablePreview> Settings = new List<VariablePreview>();
        private readonly OpenFileDialog OFD = new OpenFileDialog()
        {
            Filter = "Config Files |*.cfg"
        };
        private readonly SaveFileDialog SFD = new SaveFileDialog()
        {
            Filter = "Config Files |*.cfg"
        };

        private ICollection<VariablePreview> SearchedSettings;

        private bool FirstTime = true;

        private VariablePreview SelectedSetting { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Config = new Config();

            Settings.AddVariables(Config.CVars);

            SettingsView.ItemsSource = Settings;

            SettingsView.Items.Refresh();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (OFD?.ShowDialog() ?? false)
            {
                Config.Clear();

                Settings.Clear();

                Config.Load(OFD.FileName);

                Settings.AddVariables(Config.CVars);

                SettingsView.Items.Refresh();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SFD?.ShowDialog() ?? false)
            {
                Config.SyncWith(from setting in Settings select setting.CVar);

                Config.Save(SFD.FileName);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CVar cvar = new CVar(null, 0);

            Config.Add(cvar);

            Settings.AddVariables(cvar);

            if (SettingsView.ItemsSource != Settings)
            {
                SettingsView.ItemsSource = Settings;

                SearchBox.Text = null;
            }

            SettingsView.Items.Refresh();
        }

        private void SettingsView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbItem = (ListBoxItem)ItemsControl.ContainerFromElement(SettingsView, (DependencyObject)e.OriginalSource);

            if (lbItem != null)
            {
                SelectedSetting = (VariablePreview)lbItem.Content;

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

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Settings.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(SearchBox.Text))
                {
                    SearchedSettings = Settings.Where(setting => setting.Name.Contains(SearchBox.Text)).ToList();

                    SettingsView.ItemsSource = SearchedSettings;

                    SettingsView.Items.Refresh();
                }
                else
                {
                    SettingsView.ItemsSource = Settings;

                    SettingsView.Items.Refresh();
                }
            }
        }

        private void SearchBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FirstTime)
            {
                SearchBox.Text = null;

                FirstTime = false;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (VariablePreview setting in SettingsView.SelectedItems)
            {
                Settings.Remove(setting);

                SearchedSettings?.Remove(setting);
            }

            if (SelectedSetting != null)
            {
                Settings.Remove(SelectedSetting);

                SearchedSettings?.Remove(SelectedSetting);
            }

            SettingsView.Items.Refresh();
        }
    }
}