using KPStringComparer.PresentationLayer;
using System.Collections.Generic;
using System.Linq;
using Xceed.Wpf.Toolkit;

namespace KPStringComparer.DataAccessLayer.DTO
{
    /// <summary>
    /// DTO for strings input.
    /// </summary>
    public class StringDataObject : NotifyPropertyChanged
    {
        private string _sourceString;
        private string _subString;
        private IEnumerable<ViewString> _mainString;
        private int _currentStep;
        private int _maximumProgressBar;
        private int _precentProgressBar;
        private int _numberOfOccurrences = 0;
        private int _currentChar = 0;

        /// <summary>
        /// Pair source string and sub string.
        /// </summary>
        public IEnumerable<ViewString> MainString
        {
            get => _mainString;
            set
            {
                _mainString = value;
                MaximumProgressBar = MainString.Count();
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Searching string.
        /// </summary>
        public string SourceString
        {
            get => _sourceString;
            set
            {
                if (!value.Contains("@"))
                {
                    _sourceString = value;
                    Convert();
                    OnPropertyChanged();
                }
                else
                {
                    MessageBox.Show("Symbol @ is reservated, please dont use it");
                }
            }
        }
        /// <summary>
        /// Search pattern.
        /// </summary>
        public string SubString
        {
            get => _subString;
            set
            {
                if (!value.Contains("@"))
                { 
                _subString = value;
                Convert();
                OnPropertyChanged();
                }
                else
                {
                    MessageBox.Show("Symbol @ is reservated, please dont use it");
                }
            }
        }
        /// <summary>
        /// Max items in progress bar
        /// </summary>
        public int MaximumProgressBar
        {
            get => _maximumProgressBar;
            set
            {
                _maximumProgressBar = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Save current step for progress bar
        /// </summary>
        public int CurrentStep
        {
            get => _currentStep;
            set
            {
                _currentStep = value;
                PrecentProgressBar = (int)((double)CurrentStep / MaximumProgressBar * 100);
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Precent for progress bar
        /// </summary>
        public int PrecentProgressBar
        {
            get => _precentProgressBar;
            set
            {
                _precentProgressBar = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Display count of occurrences
        /// </summary>
        public int NumberOfOccurrences
        {
            get => _numberOfOccurrences;
            set
            {
                _numberOfOccurrences = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Identified current selected char
        /// </summary>
        public int CurrentChar
        {
            get => _currentChar;
            set
            {
                _currentChar = value;
                OnPropertyChanged();
            }
        }

        private void Convert()
        {
            var viewStrings = new List<ViewString>();
            ViewString viewString;

            foreach (var item in SubString?? "")
            {
                viewString = new ViewString(item, false);
                viewStrings.Add(viewString);
            }

            if (!string.IsNullOrEmpty(SourceString) && !string.IsNullOrEmpty(SubString))
            {
                viewString = new ViewString('@', false);
                viewStrings.Add(viewString);
            }

            foreach (var item in SourceString?? "")
            {
                viewString = new ViewString(item, false);
                viewStrings.Add(viewString);
            }
            
            //Pair local variable with global
            MainString = new List<ViewString>(viewStrings);
        }
    }
}
