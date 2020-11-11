using CSGO_Config_Generator.Data;
using CSGO_Config_Generator.Models;
using CSGO_Config_Generator.Tools;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSGO_Config_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Config Config;

        private List<SettingPreview> Settings = new List<SettingPreview>();

        private SettingPreview SelectedSetting { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Config = new Config();

            Config.Add(new CVar("viewmodel_fov", 68));
            Config.Add(new CVar("viewmodel_offset_x", 2));
            Config.Add(new CVar("viewmodel_offset_y", 0));
            Config.Add(new CVar("viewmodel_offset_z", -2));
            Config.Add(new CVar("viewmodel_recoil", 0));
            Config.Add(new CVar("cl_bob_lower_amt", 0));
            Config.Add(new CVar("cl_bobamt_lat", 0));
            Config.Add(new CVar("cl_bobamt_vert", 0));
            Config.Add(new CVar("cl_viewmodel_shift_left_amt", 0));
            Config.Add(new CVar("cl_viewmodel_shift_right_amt", 0));
            Config.Add(new CVar("cl_autohelp", 0));
            Config.Add(new CVar("cl_showhelp", 0));
            Config.Add(new CVar("cl_cmdrate", 128));
            Config.Add(new CVar("cl_updaterate", 129));
            Config.Add(new CVar("cl_interp_ratio", 1));
            Config.Add(new CVar("cl_interp", 0));
            Config.Add(new CVar("m_mousespeed", 0));
            Config.Add(new CVar("m_customaccel", 0));
            Config.Add(new CVar("m_rawinput", 0));
            Config.Add(new CVar("r_eyegloss", 0));
            Config.Add(new CVar("r_eyemove", 0));
            Config.Add(new CVar("r_eyeshift_x", 0));
            Config.Add(new CVar("r_eyeshift_y", 0));
            Config.Add(new CVar("r_eyeshift_z", 0));
            Config.Add(new CVar("r_eyesize", 0));
            Config.Add(new CVar("sv_logbans", 1));
            Config.Add(new CVar("sv_logecho", 1));
            Config.Add(new CVar("sv_logfile", 0));
            Config.Add(new CVar("sv_logflush", 1));
            Config.Add(new CVar("sv_log_onefile", 1));
            Config.Add(new CVar("snd_musicvolume", 0.5));
            Config.Add(new CVar("snd_deathcamera_volume", 0));
            Config.Add(new CVar("snd_mapobjective_volume", 0));
            Config.Add(new CVar("snd_menumusic_volume", 0));
            Config.Add(new CVar("snd_roundend_volume", 0));
            Config.Add(new CVar("snd_roundstart_volume", 0));
            Config.Add(new CVar("snd_tensecondwarning_volume", 1));
            Config.Add(new CVar("snd_mute_losefocus", 0));
            Config.Add(new CVar("host_writeconfig", null));

            Settings.AddCVars(Config.CVars);

            SettingsView.ItemsSource = Settings;

            SettingsView.Items.Refresh();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CVar cvar = new CVar(null, 0);

            Config.Add(cvar);

            Settings.AddCVars(cvar);

            SettingsView.Items.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SettingsView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedSetting != null)
            {
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

            ListBoxItem lbItem = (ListBoxItem)ItemsControl.ContainerFromElement(SettingsView, (DependencyObject)e.OriginalSource);

            SelectedSetting = (SettingPreview)lbItem.Content;
        }
    }
}
