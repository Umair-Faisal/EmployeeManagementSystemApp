using Backend.Models;
using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Controls;
using CommunityToolkit.WinUI.UI.Controls.Primitives;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using System.IO;
using ViewModel.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ApplicantViewMainPage : Page
    {
        AppMainPageVM ViewModel = new();
        public ApplicantViewMainPage()
        {
            this.InitializeComponent();
            Loaded += LoadData;
        }

        private async void LoadData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadData();
        }

        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var p = new Process();
            p.StartInfo = new(btn.Tag as string)
            {
                UseShellExecute = true
            };
            p.Start();

        }
    }
}
