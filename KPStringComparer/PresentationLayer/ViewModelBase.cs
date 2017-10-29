using System;

namespace KPStringComparer.PresentationLayer
{
    /// <summary>
    /// Base class for all view models.
    /// </summary>
    public class ViewModelBase : NotifyPropertyChanged
    {
        /// <summary>
        /// Name of view model.
        /// </summary>
        public String DisplayName { get; set; }
    }
}
