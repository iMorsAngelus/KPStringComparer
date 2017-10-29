using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KPStringComparer.DataAccessLayer.DTO;

namespace KPStringComparer.BusinessLogicLayer
{
    /// <summary>
    /// Boyer moore search.
    /// </summary>
    class BmSearch
    {
        #region private fields

        private StringDataObject _text;
        private int _patternLength;
        private int _textLength;
        private string _pattern;
        private string _mainString;
        private int[] _badCharacterShift;
        private int[] _suffixes;
        private int[] _goodSuffixShift;

        #endregion

        public event EventHandler AlgorythmFinished;

        /// <summary>
        /// Searching function
        /// </summary>
        /// <param name="text">Input strings.</param>
        /// <param name="playBackSpeed">Alghorithm speed.</param>
        /// <returns>New input string instance with async changed border and prefix.</returns>
        public StringDataObject BoyerMooreMatch(StringDataObject text, int playBackSpeed)
        {
            _text = text;

            Task.Factory.StartNew(() =>
            {
                //Visualize
                _text.CurrentStep = 0;
                //Algorithm main branch
                Prepare();

                /* Searching */
                var index = 0;
                while (index <= _textLength - _patternLength)
                {
                    //Algorithm main branch
                    int unmatched = _patternLength - 1;

                    //Visualize
                    _text.CurrentChar = unmatched + index + _patternLength + 1;
                    _text.MainString.ElementAt(unmatched + index + _patternLength + 1).IsBorder = true;

                    //Algorithm main branch
                    while (unmatched >= 0 && _pattern[unmatched] == _mainString[unmatched + index])
                    {
                        //Visualize
                        SetStepBorder(unmatched, index);
                        //Algorithm main branch
                        --unmatched;
                        //Progress bar
                        _text.CurrentStep = index + (_patternLength - unmatched) + 1;
                        Thread.Sleep(playBackSpeed * 100);
                    }

                    if (unmatched < 0)
                    {
                        //Visualize
                        ++_text.NumberOfOccurrences;
                        //Algorithm main branch
                        index += _goodSuffixShift[0];
                    }
                    else
                    {
                        //Algorithm main branch
                        _text.MainString.ElementAt(unmatched + index + _patternLength + 1).Prefix = 0;
                        //Visualize
                        Thread.Sleep(playBackSpeed * 100);
                        var startIndex = index + _patternLength + unmatched;
                        ClearBorder(startIndex, startIndex + (_patternLength - unmatched) + 1);
                        //Algorithm main branch
                        index += Math.Max(_goodSuffixShift[unmatched],
                            _badCharacterShift[_mainString[unmatched + index]] - _patternLength + 1 + unmatched);
                    }
                    //Visualize
                    ClearBorder(0, _patternLength);
                    //Progress bar
                    _text.CurrentStep = index + _patternLength + 1;
                }
                //Visualize
                _text.CurrentStep = _text.MaximumProgressBar;
                OnAlgorythmFinished();
            });

            return _text;
        }

        private void Prepare()
        {
            //Algorithm main branch
            //Initialize
            _pattern = _text.SubString;
            _mainString = _text.SourceString;

            _patternLength = _pattern.Length;
            _textLength = _mainString.Length;

            _badCharacterShift = BuildBadCharacterShift(_pattern);
            _suffixes = FindSuffixes(_pattern);
            _goodSuffixShift = BuildGoodSuffixShift(_pattern, _suffixes);

            //Visualize
            for (int i = 0; i < _patternLength; i++)
                _text.MainString.ElementAt(i).Prefix = (i + 1);
            for (int i = _patternLength; i < _text.MainString.Count(); i++)
                _text.MainString.ElementAt(i).Prefix = -1;
        }

        private void ClearBorder(int startIndex, int n)
        {
            for (int i = startIndex; i < n; i++)
            {
                _text.MainString.ElementAt(i).IsBorder = false;
            }
        }

        private void SetStepBorder(int unmatched, int index)
        {
            var patternLength = _text.SubString.Length;
            if (_text.MainString.ElementAt(unmatched + index + patternLength + 1).Prefix < 1)
                _text.MainString.ElementAt(unmatched + index + patternLength + 1).Prefix = unmatched + 1;
            _text.MainString.ElementAt(unmatched).IsBorder = true;
            _text.MainString.ElementAt(unmatched + index + patternLength + 1).IsBorder = true;
            _text.CurrentChar = unmatched + index + patternLength + 1;
        }

        private int[] BuildBadCharacterShift(string pattern)
        {
            int[] badCharacterShift = new int[256];

            for (int c = 0; c < badCharacterShift.Length; ++c)
                badCharacterShift[c] = pattern.Length;
            for (int i = 0; i < pattern.Length - 1; ++i)
                badCharacterShift[pattern[i]] = pattern.Length - i - 1;

            return badCharacterShift;
        }

        private int[] FindSuffixes(string pattern)
        {
            int f = 0, g;

            int patternLength = pattern.Length;
            int[] suffixes = new int[pattern.Length + 1];

            suffixes[patternLength - 1] = patternLength;
            g = patternLength - 1;
            for (int i = patternLength - 2; i >= 0; --i)
            {
                if (i > g && suffixes[i + patternLength - 1 - f] < i - g)
                    suffixes[i] = suffixes[i + patternLength - 1 - f];
                else
                {
                    if (i < g)
                        g = i;
                    f = i;
                    while (g >= 0 && (pattern[g] == pattern[g + patternLength - 1 - f]))
                        --g;
                    suffixes[i] = f - g;
                }
            }

            return suffixes;
        }

        private int[] BuildGoodSuffixShift(string pattern, int[] suff)
        {
            int patternLength = pattern.Length;
            int[] goodSuffixShift = new int[pattern.Length + 1];

            for (int i = 0; i < patternLength; ++i)
                goodSuffixShift[i] = patternLength;
            int j = 0;
            for (int i = patternLength - 1; i >= -1; --i)
                if (i == -1 || suff[i] == i + 1)
                    for (; j < patternLength - 1 - i; ++j)
                        if (goodSuffixShift[j] == patternLength)
                            goodSuffixShift[j] = patternLength - 1 - i;
            for (int i = 0; i <= patternLength - 2; ++i)
                goodSuffixShift[patternLength - 1 - suff[i]] = patternLength - 1 - i;

            return goodSuffixShift;
        }

        protected virtual void OnAlgorythmFinished()
        {
            AlgorythmFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}
