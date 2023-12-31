using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ViewModel.VM_Models;
using Windows.UI;
using Windows.UI.Xaml.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Controls
{
    public sealed partial class SkillView : UserControl
    {
        public SkillView()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var panel = (StackPanel)button.Parent;
            var textblock = panel.FindChild<TextBlock>();
            string skillname = textblock.Text;
            var skill = SkillList.First(x => x.SkillName == skillname);
            SkillList.Remove(skill);
        }


        public int PerRow
        {
            get { return (int)GetValue(PerRowProperty); }
            set { SetValue(PerRowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PerRow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PerRowProperty =
            DependencyProperty.Register("PerRow", typeof(int), typeof(SkillView), new PropertyMetadata(3));


        public ObservableCollection<SkillVM> SkillList
        {
            get { return (ObservableCollection<SkillVM>)GetValue(SkillListProperty); }
            set { SetValue(SkillListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SkillList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SkillListProperty =
            DependencyProperty.Register("SkillList", typeof(ObservableCollection<SkillVM>), typeof(SkillView), new PropertyMetadata(null));






    }
}
