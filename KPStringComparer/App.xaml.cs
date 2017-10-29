using System.Windows;
using KPStringComparer.PresentationLayer.ViewModel;

namespace KPStringComparer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var stringCompareViewModel = new StringCompareViewModel();
            var mainWindowViewModel = new MainWindowViewModel(stringCompareViewModel);
            var mainWindow = new MainWindow() { DataContext = mainWindowViewModel };

            mainWindow.Show();
        }
    }
}
