using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace ViewModel.VM_Models
{
    public class MainWindowVM : ObservableObject
    {
        ObservableCollection<NavigationViewItem> NavigationViewItems { get; init; }



    }
}
