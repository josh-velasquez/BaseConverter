using System.Diagnostics;
using System.Windows;

namespace BaseConverter
{
    /// <summary>
    /// Interaction logic for Instructions.xaml
    /// </summary>
    public partial class Instructions : Window
    {
        public Instructions()
        {
            InitializeComponent();
        }

        private void OnDismissClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UnaryRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri.ToString());
        }
    }
}