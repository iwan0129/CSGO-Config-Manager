using CSGO_Config_Manager.Data;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSGO_Config_Manager.Models
{
    /// <summary>
    /// Interaction logic for SettingPreview.xaml
    /// </summary>
    public partial class SettingPreview : UserControl
    {
        public bool IsNameReadOnly
        {
            get => NameBox.IsReadOnly;

            set => NameBox.IsReadOnly = value;
        }

        public bool IsValueReadOnly
        {
            get => ValueBox.IsReadOnly;

            set => ValueBox.IsReadOnly = value;
        }

        internal SettingPreview(CVar cvar)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(cvar.Name))
            {
                NameBox.IsReadOnly = false;
            }

            NameBox.Text = cvar.Name;

            ValueBox.Text = cvar.Value.ToString();
        }

        private void NameBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && e.Key == Key.Enter)
            {
                NameBox.IsReadOnly = true;
            }
        }
    }
}