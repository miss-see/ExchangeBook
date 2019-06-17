using LogService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Utility
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        private ILogService _logservice = IOCContainer.GetFromFac<ILogService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string base64 = (string)value;

                string imagebase64 = base64.Substring(base64.IndexOf(",") + 1);
                byte[] streamBase = System.Convert.FromBase64String(imagebase64);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(streamBase);
                bi.EndInit();

                return bi;
            }
            catch (Exception ex)
            {
                _logservice.Error(ex.Message);
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
