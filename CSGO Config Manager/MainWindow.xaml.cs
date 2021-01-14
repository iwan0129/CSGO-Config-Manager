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

        private readonly List<VariablePreview> Variables = new List<VariablePreview>();

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

            Variables.AddVariables(Config);

            VariableView.ItemsSource = Variables;

            VariableView.Items.Refresh();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (OFD?.ShowDialog() ?? false)
            {
                Config.Clear();

                Variables.Clear();

                Config.Load(OFD.FileName);

                Variables.AddVariables(Config);

                VariableView.Items.Refresh();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SFD?.ShowDialog() ?? false)
            {
                Config.SyncWith(from setting in Variables select setting.CVar);

                Config.Save(SFD.FileName);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CVar cvar = new CVar(null, 0);

            Config.Add(cvar);

            Variables.AddVariables(cvar);

            if (VariableView.ItemsSource != Variables)
            {
                VariableView.ItemsSource = Variables;

                SearchBox.Text = null;
            }

            VariableView.Items.Refresh();
        }

        private void VariableView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbItem = (ListBoxItem)ItemsControl.ContainerFromElement(VariableView, 
                (DependencyObject)e.OriginalSource);

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
            if (Variables.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(SearchBox.Text))
                {
                    SearchedSettings = Variables.Where(
                        setting => setting.Name.Contains(SearchBox.Text))
                        .ToList();

                    VariableView.ItemsSource = SearchedSettings;

                    VariableView.Items.Refresh();
                }
                else
                {
                    VariableView.ItemsSource = Variables;

                    VariableView.Items.Refresh();
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
            foreach (VariablePreview setting in VariableView.SelectedItems)
            {
                Variables.Remove(setting);

                SearchedSettings?.Remove(setting);
            }

            if (SelectedSetting != null)
            {
                Variables.Remove(SelectedSetting);

                SearchedSettings?.Remove(SelectedSetting);
            }

            VariableView.Items.Refresh();
        }

        private void GenerateDefaultButton_Click(object sender, RoutedEventArgs e)
        {
            Config.GenerateDefault();

            Variables.Clear();

            SearchedSettings?.Clear();

            Variables.AddVariables(Config);

            VariableView.Items.Refresh();

            if (!FirstTime)
            {
                string searchVal = SearchBox.Text;

                SearchBox.Text = null;

                SearchBox.Text = searchVal;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Config.Clear();

            Variables.Clear();

            SearchedSettings?.Clear();

            VariableView.Items.Refresh();
        }
    }
}