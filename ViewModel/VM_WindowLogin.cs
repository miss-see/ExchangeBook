using DTO;
using Newtonsoft.Json;
using Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModel.Base;

namespace ViewModel
{
    public class VM_WindowLogin : BaseViewModel
    {
        public VM_WindowLogin()
        {
            Username = new TextBox<string>();
            Password = new TextBox<string>();
            Tip = new TextBlock<string>();
        }

        #region 数据控件

        public TextBox<String> Username { get; set; }
        public TextBox<String> Password { get; set; }
        public TextBlock<String> Tip { get; set; }

        #endregion


        /// <summary>
        /// 注册
        /// </summary>
        public BaseCommand registercommand
        {
            get { return new BaseCommand(Register); }
        }
        private void Register(object send)
        {

        }

        /// <summary>
        /// 登录
        /// </summary>
        public BaseCommand logincommand
        {
            get { return new BaseCommand(Login); }
        }
        private void Login(object send)
        {
            String name = this.Username.Text;
            String pwd = this.Password.Text;

            if ((String.IsNullOrWhiteSpace(name)) || String.IsNullOrWhiteSpace(pwd))
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    this.Tip.Text = "用户名或密码不能为空！！！！";
                    Thread.Sleep(2000);
                    this.Tip.Text = "";
                    return;
                }));

                return;
            }

            ExchangeBookServer.Login(name, pwd);
        }
    }
}
