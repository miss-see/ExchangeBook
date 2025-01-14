﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel 
{
    public class TextBox<T> : Control<T>
    {
        public Action<T> TextChangeCallBack = null;

        private T _Text;
        public T Text {
            get { return _Text; }
            set
            {
                _Text = value;
                if (TextChangeCallBack != null)
                {
                    TextChangeCallBack(_Text);
                }
                OnPropertyChanged();
            }
        } 
       
    }
}
