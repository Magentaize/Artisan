using System.Collections.Generic;
using System.ComponentModel;

namespace Edi.UWP.Helpers
{
    public class WrapperBase<T>
    {
        public WrapperBase(T content)
        {
            Content = content;
        }

        public T Content { get; set; }
    }

    /// <summary>
    /// SelectorWrapper wrapps an object whih Selected property
    /// Usually used when binding the object to a UI element with a extended CheckBox or RadioRutton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectorWrapper<T> : WrapperBase<T>, INotifyPropertyChanged
    {
        public SelectorWrapper(T content)
            : base(content)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        public static void ResetSelections(IEnumerable<SelectorWrapper<T>> selectorWrapperList)
        {
            if (selectorWrapperList != null)
            {
                foreach (SelectorWrapper<T> brSel in selectorWrapperList)
                {
                    brSel.IsSelected = false;
                }
            }
        }
    }
}
