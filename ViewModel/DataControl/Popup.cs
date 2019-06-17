using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.DataControl
{
    public class Popup<T> : Control<T>
    {
        private bool _isopen;
        public bool IsOpen
        {
            get { return _isopen; }
            set
            {
                this._isopen = value;
                OnPropertyChanged();
            }
        }

        private bool _staysopen;
        public bool StaysOpen
        {
            //get { return _staysopen; }
            get { return true; }
            set
            {
                this._staysopen = value;
                OnPropertyChanged();
            }
        }

    }
}
