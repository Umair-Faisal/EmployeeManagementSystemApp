using Frontend.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        List<Tuple<string, string>> Tags { get; set; } = new();

        private bool Theme { get; set; }
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(TitleBar);
            FillTags();
            Theme = App.Current.RequestedTheme == ApplicationTheme.Light;
        }

        private void FillTags()
        {
            List<NavigationViewItem> NavItems = MainView.MenuItems.OfType<NavigationViewItem>().ToList();
            foreach (var item in NavItems)
            {
                string Tag = item.Tag.ToString();
                Tags.Add(new(Tag, " "));
                foreach (var menuItem in item.MenuItems.OfType<NavigationViewItem>())
                {
                    string Tag2 = menuItem.Tag.ToString();
                    Tags.Add(new(Tag, Tag2));
                }
            }
        }

        private void MainView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                MainFrame_Navigate(typeof(SettingsMainPage), args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer != null && args.SelectedItemContainer.Tag != null)
            {
                Type PageType = Type.GetType(args.SelectedItemContainer.Tag.ToString());
                MainFrame_Navigate(PageType, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void MainFrame_Navigate(Type navpageType, NavigationTransitionInfo transitionInfo)
        {
            Type preNavPageType = MainFrame.CurrentSourcePageType;
            if (navpageType is not null && !Type.Equals(preNavPageType, navpageType))
            {
                MainFrame.Navigate(navpageType, transitionInfo);
            }
        }

        private void MainView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        private bool TryGoBack()
        {
            if (!MainFrame.CanGoBack)
                return false;
            MainFrame.GoBack();
            return true;
        }

        private void MainFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page" + e.SourcePageType.FullName);
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            MainView.SelectedItem = MainView.MenuItems[0];
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            MainView.IsBackEnabled = MainFrame.CanGoBack;

            if (MainFrame.SourcePageType == typeof(SettingsMainPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                MainView.SelectedItem = (NavigationViewItem)MainView.SettingsItem;
            }
            else if (MainFrame.SourcePageType != null)
            {
                // Select the nav view item that corresponds to the page being navigated to.
                var selectedtag = MainFrame.SourcePageType.FullName.ToString();
                if (Tags.Find(x => x.Item1 == selectedtag) != null)
                    MainView.SelectedItem = MainView.MenuItems
                            .OfType<NavigationViewItem>()
                            .First(i => i.Tag.Equals(MainFrame.SourcePageType.FullName.ToString()));
                else if (Tags.Find(x => x.Item2 == selectedtag) is not null and var Match)
                {
                    var selecteditem = MainView.MenuItems
                        .OfType<NavigationViewItem>()
                        .First(i => i.Tag.ToString().Equals(Match.Item1));
                    MainView.SelectedItem = selecteditem.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == Match.Item2);
                }


            }
        }

        private void ThemeSwith_Toggled(object sender, RoutedEventArgs e)
        {

            if (ThemeSwith.IsOn)
            {
                if (this.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = ElementTheme.Light;
                }
            }
            else
            {
                if (this.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = ElementTheme.Dark;
                }
            }
        }
    }
}
