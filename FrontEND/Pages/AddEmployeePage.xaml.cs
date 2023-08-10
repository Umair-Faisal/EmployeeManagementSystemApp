using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.ViewModels;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AddEmployeePage : Page
{
    EmpAddPageVM ViewModel = new();
    public AddEmployeePage()
    {
        this.InitializeComponent();
        Loaded += AddEmployeePage_Loaded;
        AddButton.Click += AfterClick;
    }

    private async void AddEmployeePage_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadData();
    }


    private async void ImageSelector_Click(object sender, RoutedEventArgs e)
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
            PersonalPic.ProfilePicture = imageSource;
        }

    }
    private async void AfterClick(object sender, RoutedEventArgs e)
    {
        await Task.Delay(1500);
        Button_Click(sender, e);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(EmployeeViewMainPage));
    }
}
