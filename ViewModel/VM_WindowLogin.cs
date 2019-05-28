using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModel.Base;

namespace ViewModel
{
    public class VM_WindowLogin : BaseViewModel
    {
        public Image<String> nameImage { get; set; }
        public Image<String> passwordImage { get; set; }

        public VM_WindowLogin()
        {
            nameImage = new Image<String>();
            passwordImage = new Image<String>();
            nameImage.Source =  AppDomain.CurrentDomain.BaseDirectory+ @"/Resources/backgroud.bmp";
            passwordImage.Source = AppDomain.CurrentDomain.BaseDirectory + @"/Resources/login_name.png";
        }
    }
}
