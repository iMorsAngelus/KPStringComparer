using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using KPStringComparer.BusinessLogicLayer;
using KPStringComparer.DataAccessLayer.DTO;
using KPStringComparer.PresentationLayer.Command;

namespace KPStringComparer.PresentationLayer.ViewModel
{
    /// <summary>
    /// String comparer view model.
    /// </summary>
    class StringCompareViewModel : ViewModelBase
    {
        #region private fields

        private readonly BmSearch _bmSearch;
        private readonly KmpSearch _kmpSearch;
        private StringDataObject _inputStrings;
        private ActionCommand _kmpClickCommand;
        private ActionCommand _bmClickCommand;
        private ActionCommand _selectionItemChangedCommand;
        private int _playBackSpeed = 5;
        private Visibility _isProgressBarVisible = Visibility.Collapsed;

        #endregion
        /// <summary>
        /// Show / hide progress bar
        /// </summary>
        public Visibility IsProgressBarVisible
        {
            get => _isProgressBarVisible;
            set
            {
                _isProgressBarVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Input strings from UI.
        /// </summary>
        public StringDataObject InputStrings
        {
            get => _inputStrings;
            set
            {
                _inputStrings = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Speed for alhorithm.
        /// </summary>
        public int PlayBackSpeed
        {
            get => _playBackSpeed;
            set
            {
                if (value >= 0 && value <= 25)
                {
                    _playBackSpeed = value;
                    OnPropertyChanged();
                }
                else MessageBox.Show("Wrong input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Command for start KMP searching.
        /// </summary>
        public ActionCommand KmpClickCommand
        {
            get
            {
                if (_kmpClickCommand == null)
                    _kmpClickCommand = new ActionCommand(
                        p => KmpSearchStart()
                    );
                return _kmpClickCommand;
            }
        }

        /// <summary>
        /// Command for start BM searching.
        /// </summary>
        public ActionCommand BmClickCommand
        {
            get
            {
                if (_bmClickCommand == null)
                    _bmClickCommand = new ActionCommand(
                        p => BmSearchStart()
                    );
                return _bmClickCommand;
            }
        }
        /// <summary>
        /// Command for handle item changed event.
        /// </summary>
        public ActionCommand SelectionItemChangedCommand
        {
            get
            {
                if (_selectionItemChangedCommand == null)
                    _selectionItemChangedCommand = new ActionCommand(
                        SelectionItemChanged
                    );
                return _selectionItemChangedCommand;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StringCompareViewModel"/> class.        
        /// </summary>
        public StringCompareViewModel()
        {
            this.DisplayName = "String compare";
            //ababasdfabababasdfbasa
            //abasdfsdabsa
            _inputStrings = new StringDataObject();
            _kmpSearch = new KmpSearch();
            _bmSearch = new BmSearch();

            //Events method
            _kmpSearch.AlgorythmFinished += ProgressBarHide;
            _bmSearch.AlgorythmFinished += ProgressBarHide;
        }

        private void BorderAndPrefixClear()
        {
            InputStrings.NumberOfOccurrences = 0;
            foreach (var item in InputStrings.MainString)
            {
                item.IsBorder = false;
                item.Prefix = 0;
            }
        }

        private void BmSearchStart()
        {
            IsProgressBarVisible = Visibility.Visible;
            BorderAndPrefixClear();

            InputStrings = _bmSearch.BoyerMooreMatch(InputStrings, PlayBackSpeed);
        }

        private void KmpSearchStart()
        {
            IsProgressBarVisible = Visibility.Visible;
            BorderAndPrefixClear();

            InputStrings = _kmpSearch.KmpMatch(InputStrings, PlayBackSpeed);
        }

        private void ProgressBarHide(object sender, EventArgs e)
        {
            //Delay in main thread
            SpinWait.SpinUntil(() =>
            {
                Thread.Sleep(100);
                return false;
            }, 5);
            IsProgressBarVisible = Visibility.Collapsed;
        }

        private void SelectionItemChanged(object sender)
        {
            var listBox = sender as ListBox;
            listBox?.ScrollIntoView(listBox.SelectedItem);
        }
    }
}
