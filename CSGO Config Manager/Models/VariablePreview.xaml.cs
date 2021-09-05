using CSGO_Config_Manager.Data;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSGO_Config_Manager.Models
{
    /// <summary>
    /// Interaction logic for SettingPreview.xaml
    /// </summary>
    public partial class VariablePreview : UserControl
    {
        internal CVar CVar;

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

        public new string Name => NameBox.Text;

        internal VariablePreview(CVar cvar)
        {
            InitializeComponent();

            CVar = cvar;

            DataContext = cvar;
        }

        private void NameBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            NameBox.IsReadOnly = !string.IsNullOrEmpty(NameBox.Text)
                && e.Key == Key.Enter;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CVar.Name = NameBox.Text;
        }

        private void ValueBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CVar.Value = ValueBox.Text;
        }
    }
}