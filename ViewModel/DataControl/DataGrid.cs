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
                OnPropertyChanged();
            }
        }


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

        private T _SelectedItem;
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
        public void SetFilter(Func<object, bool>  dataFilter)
        {
            try
            {
                DataFilter = dataFilter;
                _ItemsSourceView = CollectionViewSource.GetDefaultView(_ItemsSource);
                _ItemsSourceView.Filter = new Predicate<object>(DataFilter);
            }
            catch(Exception ex)
            {
               
            }
        }
        public List<PropertyInfo> typePropertylist = new List<PropertyInfo>();

        /// <summary>
        /// 原子操作上锁，防止操作未完成  又执行下一次操作
        /// </summary>
        private long usingResource = 0;

        List<FilterProperty> ComparePropertyList = new List<FilterProperty>();


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
