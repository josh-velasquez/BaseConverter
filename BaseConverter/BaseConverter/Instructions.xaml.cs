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
            this.Close();
        }
    }
}