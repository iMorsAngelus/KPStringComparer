using KPStringComparer.PresentationLayer;

namespace KPStringComparer.DataAccessLayer.DTO
{
    /// <summary>
    /// View strings elements
    /// </summary>
    public class ViewString : NotifyPropertyChanged
    {
        private char _char;
        private bool _isBorder;
        private int _prefix;

        /// <summary>
        /// Symbhol from sub or main strings
        /// </summary>
        public char Char
        {
            get => _char;
            set
            {
                _char = value; 
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// A value indicating whether to draw a border.
        /// </summary>
        public bool IsBorder
        {
            get => _isBorder;
            set
            {
                _isBorder = value; 
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Prefix index, searching result
        /// </summary>
        public int Prefix
        {
            get => _prefix;
            set
            {
                _prefix = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewString"/> class.
        /// </summary>
        /// <param name="ch">Character.</param>
        /// <param name="isBorder">Border value.</param>
        public ViewString(char ch, bool isBorder)
        {
            Char = ch;
            IsBorder = isBorder;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewString"/> class.
        /// </summary>
        public ViewString()
        {}
    }
}