using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using ViewModel.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeleteEmployeePage : Page
    {
        public EmpDelPageVM ViewModel = new();
        public DeleteEmployeePage()
        {
            this.InitializeComponent();

        }

        private async void LoadData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadData();
        }

        private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.DelButtonCommand.ExecuteAsync(null);
            this.Frame.Navigate(typeof(EmployeeViewMainPage));
        }
    }
}
