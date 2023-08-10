using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.ViewModels;
using ViewModel.VM_Models;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateEmployeePage : Page
    {
        public EmpUpdatePageVM ViewModel { get; set; }
        public UpdateEmployeePage()
        {
            this.InitializeComponent();
            ViewModel = new EmpUpdatePageVM();
            Loaded += LoadData;
        }

        private async void LoadData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadData();
        }



        private async void UpdateBtn_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await Task.Delay(1000);
            Button_Click_1(sender, e);
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var item = combobox.SelectedItem as EmployeeVM;
            if (item != null)
            {
                item.Image = await FileHandler.GetImgSourceAsync(item.EmployeeImg);
            }
        }

        private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            var Window = (App.Current as App)?.m_window as MainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Window);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                byte[] bytes = await FileHandler.GetBytesFromFileAsync(file);
                SoftwareBitmapSource imageSource = await FileHandler.GetImgSourceAsync(bytes);
                ViewModel.Employee.EmployeeImg = bytes;
                ViewModel.Employee.Image = imageSource;
            }
        }

        private void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmployeeViewMainPage));
        }
    }
}
