using DTO;
using LogService;
using Model;
using Proxy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModel.Base;
using ViewModel.DataControl;

namespace ViewModel
{
    public class VM_WindowMain : BaseViewModel
    {

        public VM_WindowMain()
        {
            _logservice = IOCContainer.GetFromFac<ILogService>();

            PopupTable = new Hashtable();
            CharacteristicBookList = new Popup<bool>();
            PopularWriterList = new Popup<bool>();
            DetailData = new DataGrid<DetailData>();

            PopupTable.Add("CharacteristicBookList", CharacteristicBookList);
            PopupTable.Add("PopularWriterList", PopularWriterList);

            DetailData.SkipNumber = 1;
            DetailData.BindSource(LoadData);
        }

        /// <summary>
        /// 日志服务
        /// </summary>
        private ILogService _logservice;
        /// <summary>
        /// 当前选择书籍类型
        /// </summary>
        public String _family = null;

        /// <summary>
        /// popup 集合  
        /// </summary>
        private Hashtable PopupTable;
        /// <summary>
        /// 特色书单
        /// </summary>
        public Popup<bool> CharacteristicBookList { get; set; }
        /// <summary>
        /// 人气作家
        /// </summary>
        public Popup<bool> PopularWriterList { get; set; }


        /// <summary>
        /// 列表详细数据
        /// </summary>
        public DataGrid<DetailData> DetailData { get; set; }
        /// <summary>
        /// 设置 ItemsSource
        /// </summary>
        public void LoadData()
        {
            int currentPage = DetailData.CurrentPage;
            int skipNumber = DetailData.SkipNumber;


            DetailDataResult resul = ExchangeBookServer.GetBookDetailData(currentPage, skipNumber, _family);
            DetailData.SetItemsSource(resul.Data);
            DetailData.RecordCount = resul.TotalCount;
        }



        /// <summary>
        /// 鼠标从popup 离开事件
        /// </summary>
        public BaseCommand PopupMouseLeaveCommand
        {
            get { return new BaseCommand(PopupMouseLeaveFunc); }
        }
        private void PopupMouseLeaveFunc(object obj)
        {
            String popupstring = obj.ToString();
            //取出对应popup   改变状态
            Popup<bool> popup = PopupTable[popupstring] as Popup<bool>;
            popup.IsOpen = false;
        }


        /// <summary>
        /// 按钮 打开  关闭popup
        /// </summary>
        public BaseCommand PopupChangeStatusCommand
        {
            get { return new BaseCommand(PopupChangeStatusFunc); }

        }
        private void PopupChangeStatusFunc(object obj)
        {
            try
            {
                String popupstring = obj.ToString();
                //取出对应popup   改变状态
                foreach (String item in PopupTable.Keys)
                {
                    Popup<bool> popup = PopupTable[item] as Popup<bool>;
                    if (popupstring.Contains(item))
                    {
                        popup.IsOpen = !popup.IsOpen;
                        continue;
                    }
                    //如果不是当前按钮对应的popup  则全部关闭显示
                    popup.IsOpen = false;
                }
            }
            catch (Exception e)
            {
                _logservice.Error(e.Message);
                return;
            }
        }

        public BaseCommand MenuCommand
        {
            get { return new BaseCommand(MenuFunc); } 
        }
        private void MenuFunc(object obj)
        {
            String key = obj.ToString();


            LoadData();
        }
    }
}
