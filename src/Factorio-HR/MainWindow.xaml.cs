using System.Windows;
using Factorio_HR.ViewModel.Designer;

namespace Factorio_HR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(new PLinkAdapter(new SettingService()));
            InitializeComponent();
        }
    }
}
