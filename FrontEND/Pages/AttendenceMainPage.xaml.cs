using Microsoft.UI.Xaml.Controls;
using System;
using ViewModel.ViewModels;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AttendenceMainPage : Page
    {
        AttendencePageVM ViewModel = new();
        public AttendenceMainPage()
        {
            this.InitializeComponent();
            Loaded += LoadData;
        }

        private async void LoadData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadData();
        }

        private async void SaveToExelButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var FilePicker = new FileOpenPicker();
            FilePicker.ViewMode = PickerViewMode.Thumbnail;
            FilePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            FilePicker.FileTypeFilter.Add(".xlsx");

            var Window = (App.Current as App)?.m_window as MainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Window);
            WinRT.Interop.InitializeWithWindow.Initialize(FilePicker, hWnd);

            var File = await FilePicker.PickSingleFileAsync();
            if (File != null)
            {
                ViewModel.ExcelFile = File.Path;
                await ViewModel.SaveToExcelCommand.ExecuteAsync(null);
            }
        }
    }
}
