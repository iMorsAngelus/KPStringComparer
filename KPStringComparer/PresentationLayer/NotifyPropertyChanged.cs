using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KPStringComparer.PresentationLayer
{
    /// <summary>
    /// Class, who implement INotifyPropertyChanged interface.
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Property changed event
        /// </summary>
        /// <param name="propertyName">Name of called property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
