using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Artisan.Toolkit
{
    /// <summary>
    /// 表示一个支持属性变更通知的对象。
    /// </summary>
    public class NotifyPropertyObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// 当属性变更将要变更时发生。
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// 当属性变更后发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 引发<see cref="PropertyChanged"/>事件。
        /// </summary>
        /// <param name="propertyName">发生变更的属性名，为<c>null</c>将自动采用调用方名。</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 引发<see cref="PropertyChanged"/>事件。
        /// </summary>
        /// <param name="propertyName">将要发生变更的属性名，为<c>null</c>将自动采用调用方名。</param>
        protected virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// 设置属性值，并提供相应的属性通知。
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="newValue">属性值</param>
        /// <param name="propertyName">将要发生设置的属性名，为<c>null</c>将自动采用调用方名。</param>
        //protected virtual void UpdateProperty<T>(T value, [CallerMemberName] string propertyName = null)
        //{
        //    PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName);
        //    T oldValue = (T)propertyInfo.GetValue(this);
        //    if (!Equals(oldValue, value))
        //    {
        //        OnPropertyChanging(propertyName);
        //        propertyInfo.SetValue(this, value);
        //        OnPropertyChanged(propertyName);
        //    }
        //}
        protected virtual void UpdateProperty<T>(ref T propertyValue,T newValue,[CallerMemberName] string propertyName=null)
        {
            if(!Equals(propertyValue,newValue))
            {
                RaisePropertyChanging(propertyName);
                propertyValue = newValue;
                RaisePropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// 引发<see cref="PropertyChanged"/>事件。
        /// </summary>
        /// <param name="propertyName">发生变更的属性名，为<c>null</c>将自动采用调用方名。</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
