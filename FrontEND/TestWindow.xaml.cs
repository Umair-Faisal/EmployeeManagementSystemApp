using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModel.VM_Models;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestWindow : Window
    {
        ObservableCollection<SkillVM> Testskills = new();
        public TestWindow()
        {
            FillSkills();
            this.InitializeComponent();
        }

        private void FillSkills()
        {
            for(int i =0; i<100; i++)
            {
                SkillVM skillVM = new SkillVM()
                {
                    SkillName = $"SKill {i}",
                    SkillId = i,
                };
                Testskills.Add(skillVM);
            }
        }
    }
}
