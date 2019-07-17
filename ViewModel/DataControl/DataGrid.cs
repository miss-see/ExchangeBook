using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utility;
using System.Windows.Data;
using System.Data;
using System.Threading;
using System.Reflection;
using System.Collections.ObjectModel;
using Model;

namespace ViewModel
{
    public interface IDataGrid
    {
        ObservableCollection<Object> ItemsSource { get; }
        Object SelectedItem { get; }
        Object Condition { get; }

        event Action<Object> ItemsSourceChange;
        string DataGridName { get; set; }

        Action<Object, Object, Object> TextChange { get; set; }
        Action<Object, Object, Object, Object, Object> TextLostFocus { get; set; }
        Action<Object> AllCheck { get; set; }
        Action ExcuteItemsSourceChange { get; set; }
        string DetailVisibility { get; set; }
        List<Object> DetailSource { get; set; }
        bool IsBusyDetail { get; set; }

    }

    public class DataGrid<T> : Control<T>, IDataGrid
    {
        /// <summary>
        /// 加载数据方法
        /// </summary>
        private Action<T> LoadAction = null;
        public Action<T> SelectCallBack = null;
        private Func<object, bool> DataFilter = null;

        #region 分页 
        private volatile int _CurrentPage = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get { return _CurrentPage; }
            set
            {
                _CurrentPage = value;
                if (_CurrentPage > PageCount)
                {
                    _CurrentPage = PageCount;
                }
                if (_CurrentPage < 1)
                {
                    _CurrentPage = 1;
                }
                OnPropertyChanged();
            }
        }

        private int _PageCount = 1;
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get { return _PageCount; }
            set
            {
                _PageCount = value;
                OnPropertyChanged();
            }
        }


        private int _RecordCount = 0;
        /// <summary>
        /// 记录条数
        /// </summary>
        public int RecordCount
        {
            get { return _RecordCount; }
            set
            {
                _RecordCount = value;
                if (_RecordCount <= SkipNumber)
                {
                    PageCount = 1;
                }
                else
                {
                    PageCount = int.Parse(Math.Ceiling((double)RecordCount / (double)SkipNumber).ToString());
                }
                if (_CurrentPage > PageCount)
                {
                    _CurrentPage = PageCount;
                }
                OnPropertyChanged();
            }
        }

        private int _SkipNumber = 30;
        /// <summary>
        /// 每页条数
        /// </summary>
        public int SkipNumber
        {
            get { return _SkipNumber; }
            set
            {
                _SkipNumber = value;
                OnPropertyChanged();
            }
        }

        private TextBox<string> _JumpTextBox = new TextBox<string>();
        public TextBox<string> JumpTextBox
        {
            get { return _JumpTextBox; }
            set
            {
                _JumpTextBox = value;
                OnPropertyChanged();
            }
        }

        #region 跳页

        /// <summary>
        /// 跳页命令
        /// </summary>
        public BaseCommand JumpCommand
        {
            get
            {
                return new BaseCommand(JumpCommand_Executed);
            }
        }
        /// <summary>
        /// 跳页
        /// </summary>
        /// <param name="send"></param>
        void JumpCommand_Executed(object send)
        {
            int pagenum = 0;

            if (int.TryParse(JumpTextBox.Text, out pagenum))
            {
                if (pagenum <= PageCount && pagenum > 0)
                {
                    CurrentPage = pagenum;

                    if (LoadAction != null)
                    {
                        LoadAction(Condition);
                    }
                }
                else
                {
                    MessageBox.Show("请正确填写跳转页数。", "提示信息");
                }
            }
            else
            {
                MessageBox.Show("请正确填写跳转页数。", "提示信息");
            }
        }
        #endregion

        #region 上一页

        /// <summary>
        /// 上一页
        /// </summary>
        public BaseCommand PreviousCommand
        {
            get
            {
                return new BaseCommand(PreviousCommand_Executed);
            }
        }
        void PreviousCommand_Executed(object send)
        {
            if (CurrentPage > 1)
            {
                CurrentPage -= 1;
                if (LoadAction != null)
                {
                    LoadAction(Condition);
                }
            }
            else
            {
                MessageBox.Show("已至首页。", "提示信息");
            }
        }
        #endregion

        #region 下一页

        /// <summary>
        /// 下一页
        /// </summary>
        public BaseCommand NextCommand
        {
            get
            {
                return new BaseCommand(NextCommand_Executed);
            }
        }
        void NextCommand_Executed(object send)
        {
            if (CurrentPage < PageCount)
            {
                CurrentPage += 1;

                if (LoadAction != null)
                {
                    LoadAction(Condition);
                }
            }
            else
            {
                MessageBox.Show("已至末页。", "提示信息");
            }
        }

        #endregion

        #endregion

        private ObservableCollection<T> _ItemsSource = new ObservableCollection<T>();
        public ObservableCollection<T> ItemsSource
        {
            get { return _ItemsSource; }
            set
            {
                _ItemsSource = value;
                if (_ItemsSource != null && _ItemsSource.Count > 0 && SelectedItem == null)
                {
                    SelectedItem = _ItemsSource.First();
                }
                OnPropertyChanged();
                if (ItemsSourceChange != null)
                {
                    ItemsSourceChange(this);
                }
            }
        }
        public void SetItemsSource(List<T> itemSource)
        {
            ItemsSource = new ObservableCollection<T>(itemSource);
        }

        public T _SelectedItem;
        public T SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem == null || !_SelectedItem.Equals(value))
                {
                    _SelectedItem = value;
                    if (SelectCallBack != null)
                    {
                        SelectCallBack(_SelectedItem);
                    }
                    OnPropertyChanged();
                }
            }
        }

        private ListCollectionView _ListCollectionView;
        public ListCollectionView ListCollectionView
        {
            get
            {
                _ListCollectionView = this.ItemsSourceView as ListCollectionView;
                return _ListCollectionView;
            }
            set
            {
                _ListCollectionView = value;
                OnPropertyChanged();
            }
        }

        private ICollectionView _ItemsSourceView;
        public ICollectionView ItemsSourceView
        {
            get
            {
                _ItemsSourceView = CollectionViewSource.GetDefaultView(_ItemsSource);
                return _ItemsSourceView;
            }
            set
            {
                _ItemsSourceView = value;
                OnPropertyChanged();
            }
        }

        private T _Condition = (T)Activator.CreateInstance(typeof(T));

        public T Condition
        {
            get { return _Condition; }
            set
            {
                _Condition = value;
                OnPropertyChanged();
            }
        }



        #region 方法  
        public DataGrid()
        {
            typePropertylist = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();
        }

        /// <summary>
        /// 绑定数据加载方法
        /// </summary>
        /// <param name="loadAction"></param>
        /// <param name="conditionRow"></param>
        public void BindSource(Action<T> loadAction, T conditionRow = default(T))
        {
            LoadAction = loadAction;
            if (LoadAction != null)
            {
                CurrentPage = 1;
                LoadAction(conditionRow);
            }
        }
        /// <summary>
        /// 绑定数据加载方法
        /// </summary>
        /// <param name="loadAction"></param>
        public void BindSource(Action loadAction)
        {
            LoadAction = new Action<T>((obj) => {
                loadAction();
            }); ;
            if (LoadAction != null)
            {
                CurrentPage = 1;
                LoadAction(default(T));
            }
        }

        /// <summary>
        /// 重新绑定ItemsSource
        /// </summary>
        public void ItemsSourceReBind()
        {
            BindSource(LoadAction);
        }
        public void SelectedItemReBind()
        {
            T newitem = (T)Activator.CreateInstance(typeof(T));
            List<System.Reflection.PropertyInfo> plist = typeof(T).GetProperties().ToList();

            foreach (var propertyInfo in plist)
            {
                propertyInfo.SetValue(newitem, propertyInfo.GetValue(SelectedItem));
            }
            SelectedItem = newitem;
        }

        #region 过滤
        public void SetFilter(Func<object, bool> dataFilter)
        {
            try
            {
                DataFilter = dataFilter;
                _ItemsSourceView = CollectionViewSource.GetDefaultView(_ItemsSource);
                _ItemsSourceView.Filter = new Predicate<object>(DataFilter);
            }
            catch (Exception ex)
            {

            }
        }
        public List<PropertyInfo> typePropertylist = new List<PropertyInfo>();
        private long usingResource = 0;
        List<FilterProperty> ComparePropertyList = new List<FilterProperty>();
        public List<string> NumberColumn = new List<string>() {
            "decimal",
            "int",
            "int32",
            "int64",
            "long",
            "double",
            "float"
        };




        public static object GetDefaultForType(Type propertyType)
        {
            if (propertyType.Name == "String")
            {
                return string.Empty;
            }
            else
            {
                return propertyType.IsValueType ? Activator.CreateInstance(propertyType) : null;
            }
        }




        public string GetValueAndCondition(object filterValue, out object actualfilterValue)
        {
            string conditionStr = "=";
            string filterValueStr = filterValue.ToString();

            if (filterValueStr.StartsWith(">"))
            {
                conditionStr = ">";
                filterValue = filterValueStr.Replace(">", "");
            }
            if (filterValueStr.StartsWith("<"))
            {
                conditionStr = "<";
                filterValue = filterValueStr.Replace("<", "");
            }
            if (filterValueStr.StartsWith("<="))
            {
                conditionStr = "<=";
                filterValue = filterValueStr.Replace("<=", "");
            }
            if (filterValueStr.StartsWith(">="))
            {
                conditionStr = ">=";
                filterValue = filterValueStr.Replace(">=", "");
            }
            if (filterValueStr.StartsWith("="))
            {
                conditionStr = "=";
                filterValue = filterValueStr.Replace("=", "");
            }
            else if (filterValueStr.StartsWith("!="))
            {
                conditionStr = "!=";
                filterValue = filterValueStr.Replace("!=", "");
            }
            if (filterValueStr.StartsWith("!like"))
            {
                conditionStr = "!like";
                filterValue = filterValueStr.Replace("!like", "");
            }
            else if (filterValueStr.StartsWith("like"))
            {
                conditionStr = "like";
                filterValue = filterValueStr.Replace("like", "");
            }
            actualfilterValue = filterValue;
            return conditionStr;
        }

        #endregion

        public void Refresh()
        {
            if (_ItemsSourceView == null)
            {
                _ItemsSourceView = CollectionViewSource.GetDefaultView(this.ItemsSource);
            }
            _ItemsSourceView.Refresh();
        }
        #endregion



        #region IDataGrid 
        ObservableCollection<object> IDataGrid.ItemsSource
        {
            get
            {
                ObservableCollection<Object> ret = new ObservableCollection<Object>();
                foreach (var item in this.ItemsSource)
                {
                    ret.Add(item);
                }
                return ret;
            }
        }

        object IDataGrid.SelectedItem
        {
            get
            {
                return SelectedItem;
            }

        }

        object IDataGrid.Condition
        {
            get
            {
                return Condition;
            }

        }


        public event Action<Object> ItemsSourceChange;
        public void ChangeItemsSource()
        {
            if (ExcuteItemsSourceChange != null)
            {
                ExcuteItemsSourceChange();
            }
        }
        public string DataGridName { get; set; }

        /// <summary>
        /// 英文名,类型,修改值
        /// </summary>
        public Action<Object, Object, Object> TextChange { get; set; }

        public Action<Object, Object, Object, Object, Object> TextLostFocus { get; set; }

        public Action<Object> AllCheck { get; set; }


        public List<Object> _DetailSource;
        public List<Object> DetailSource
        {
            get { return _DetailSource; }
            set
            {
                _DetailSource = value;
                OnPropertyChanged();
            }
        }

        public bool _IsBusyDetail;
        public bool IsBusyDetail
        {
            get { return _IsBusyDetail; }
            set
            {
                _IsBusyDetail = value;
                OnPropertyChanged();
            }
        }

        public string _DetailVisibility;
        public string DetailVisibility
        {
            get { return _DetailVisibility; }
            set
            {
                _DetailVisibility = value;
                OnPropertyChanged();
            }
        }


        public Action ExcuteItemsSourceChange { get; set; }
        #endregion
    }
}
