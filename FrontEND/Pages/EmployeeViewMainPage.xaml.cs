using Microsoft.UI.Xaml.Controls;
using System;
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

    private  void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(AddEmployeePage));

    }

    private void EditButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var Btn = (Button)sender;
        int id = int.Parse(Btn.Tag.ToString());
        Frame.Navigate(typeof(UpdateEmployeePage),id);

    }
}

