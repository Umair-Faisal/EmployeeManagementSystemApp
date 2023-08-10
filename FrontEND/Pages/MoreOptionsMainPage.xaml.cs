using Microsoft.UI.Xaml.Controls;
using ViewModel.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MoreOptionsMainPage : Page
    {
        OptionVM ViewModel = new();
        public MoreOptionsMainPage()
        {
            this.InitializeComponent();
            NewSkillBox.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            NewCatBox.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            Loaded += LoadData;
        }

        private void LoadData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }

        private void AddCatagoryBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            NewCatBox.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }
        private void AddSkillBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            NewSkillBox.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }


        private void CancelAddSkillBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            NewSkillBox.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }

        private void CancelAddCatagoryBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            NewCatBox.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }

    }
}
