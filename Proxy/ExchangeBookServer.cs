using DTO;
using LogService;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Proxy
{
    public class ExchangeBookServer
    {
        /// <summary>
        /// 服务器
        /// </summary>
        static ServiceReference_exchangebook.ExchangeBookServiceSoapClient _server = new ServiceReference_exchangebook.ExchangeBookServiceSoapClient();
        static ILogService _logservice = IOCContainer.GetFromFac<ILogService>();

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static LoginResult Login(String name, String pwd)
        {
            String msg = _server.Login(name, pwd);

           return JsonConvert.DeserializeObject<LoginResult>(msg);
        }

        /// <summary>
        /// 获取数据详细数据
        /// </summary>
        /// <returns></returns>
        public static List<BookDetailData> GetBookDetailData()
        {
            List<BookDetailData> list = new List<BookDetailData>();

            try
            {
                String remsg = _server.GridData();
                if (String.IsNullOrEmpty(remsg)) ///若返回字符串为空 则返回k空
                {
                    _logservice.Error("请求书籍详细数据，服务器返回值为空");
                    return null;
                }
                BookDetailData data = JsonConvert.DeserializeObject<BookDetailData>(remsg);
                list.Add(data);

                return list;
            }
            catch (Exception e)
            {
                _logservice.Error(e.Message);
                return null;
            }

        }

    }
}
