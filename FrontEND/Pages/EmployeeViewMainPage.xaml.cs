using Microsoft.UI.Xaml.Controls;
using ViewModel.ViewModels;

namespace Frontend.Pages;


public sealed partial class EmployeeViewMainPage : Page
{
    public EmpMainPageVM ViewModel = new();

    public EmployeeViewMainPage()
    {

        this.InitializeComponent();
        Loaded += LoadData;


    }

    private async void LoadData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.LoadData();
    }

}

