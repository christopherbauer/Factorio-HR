using System.Windows;
using Factorio_HR.Services;
using Factorio_HR.ViewModels;

namespace Factorio_HR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var messageBusSingleton = new MessageBus();
            DataContext = new MainWindowViewModel(new CmdAdapter(new AppSettingService(), messageBusSingleton), messageBusSingleton);
            InitializeComponent();
        }
    }
}
