using Microsoft.UI.Xaml.Automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class AppDataPath
    {
        private static StringBuilder BasePathGenerator()
        {
            StringBuilder builder = new StringBuilder();
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            builder.Append(appdata);
            builder.Append(@"\EmployeeManagementSystem\");
            return builder;
        }

        public static string BasePath
        {
            get { return BasePathGenerator().ToString(); }
        }

        public static string PDFPath
        {
            get { return BasePathGenerator().Append(@"CVs\").ToString(); }
        }

        public static string ImgPath
        {
            get
            {
                return BasePathGenerator().Append(@"Images\").ToString();
            }
        }

    }
}
