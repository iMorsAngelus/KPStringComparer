using System.Collections.Generic;

namespace KPStringComparer.PresentationLayer.ViewModel
{
    /// <summary>
    /// Main view model, who contains all other.
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        #region private fields

        private List<ViewModelBase> _viewModelsList;

        #endregion

        /// <summary>
        /// ViewModelsList of applications.
        /// </summary>
        public List<ViewModelBase> ViewModelsList
        {
            get => _viewModelsList;
            set
            {
                _viewModelsList = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.        
        /// </summary>
        /// <param name="viewModel"></param>
        public MainWindowViewModel(ViewModelBase viewModel)
        {
            ViewModelsList = new List<ViewModelBase> {viewModel};
        }
    }
}
