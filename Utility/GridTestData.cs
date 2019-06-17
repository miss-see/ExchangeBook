using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class GridTestData
    {

        public static List<BookDetailData> TestData()
        {
            List<BookDetailData> list = new List<BookDetailData>();

            for (int i = 0; i < 10; i++)
            {
                BookDetailData data = new BookDetailData();
                data.Image = AppDomain.CurrentDomain.BaseDirectory + "Resources\\backgroud.bmp";
                data.Author = i.ToString();
                data.Description = i.ToString();
                data.Description = "1234567890";
                data.UserName = i.ToString();
                data.Level = i;

                list.Add(data);
            }

            return list;
        }

    }
}
