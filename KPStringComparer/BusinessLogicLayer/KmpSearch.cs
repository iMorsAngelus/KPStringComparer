using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KPStringComparer.DataAccessLayer.DTO;

namespace KPStringComparer.BusinessLogicLayer
{
    /// <summary>
    /// Knuth-morris-pratt search.
    /// </summary>
    public class KmpSearch
    {
        private StringDataObject _text;

        public event EventHandler AlgorythmFinished;

        /// <summary>
        /// Searching function
        /// </summary>
        /// <param name="text">Input strings.</param>
        /// <param name="playBackSpeed">Alghorithm speed.</param>
        /// <returns>New input string instance with async changed border and prefix.</returns>
        public StringDataObject KmpMatch(StringDataObject text, int playBackSpeed)
        {
            _text = text;

            Task.Factory.StartNew(() =>
            {
                _text.CurrentStep = 0;
                var mainString = new string(_text.MainString
                    .Select(x => x.Char)
                    .ToArray<char>());
                //Initialize
                var n = mainString.Length;

                //Algorythm
                for (var i = 1; i < n; i++)
                {
                    //Algorithm main branch
                    _text.CurrentChar = (i < mainString.IndexOf('@')) ? mainString.IndexOf('@')+1 : i;
                    var j = _text.MainString.ElementAt(i - 1).Prefix;
                    //Vizualize
                    var prevJ = j;
                    SetStepBorder(i, j);

                    //Algorithm main branch
                    while ((j > 0) && (mainString[i] != mainString[j]))
                    {
                        //Vizualize
                        _text.MainString.ElementAt(j).IsBorder = false;
                        //Decrement substring index
                        j = _text.MainString.ElementAt(j - 1).Prefix;
                        //Vizualize
                        _text.MainString.ElementAt(j).IsBorder = true;
                        Thread.Sleep(playBackSpeed * 100);
                    }

                    //Vizualize
                    if (prevJ.Equals(j))
                    {
                        Thread.Sleep(playBackSpeed * 100);
                    }

                    //Vizualize
                    if (i == n - 1 || mainString[i] != mainString[j])
                    {
                        if (prevJ != mainString.IndexOf('@'))
                            ClearBorder(i - prevJ, i + prevJ);
                        ClearBorder(i, i + j + 1);
                    }
                    //Algorithm main branch
                    if (mainString[i] == mainString[j])
                    {
                        //Increment substring index
                        ++j;
                        //Vizualize
                        _text.MainString.ElementAt(j - 1).IsBorder = false;
                    }

                    //Algorithm main branch
                    _text.MainString.ElementAt(i).Prefix = j;
                    //Vizualize
                    _text.NumberOfOccurrences += (j == mainString.IndexOf('@')) ? 1 : 0;
                    _text.CurrentStep = i;
                }

                //Vizualize
                ClearBorder(0, mainString.IndexOf('@') + 1);
                //Progress bar
                _text.CurrentStep = _text.MaximumProgressBar;
                OnAlgorythmFinished();
            });

            return _text;
        }
        private void ClearBorder(int startIndex, int n)
        {
            for (int i = startIndex; i < n; i++)
            {
                _text.MainString.ElementAt(i).IsBorder = false;
            }
        }

        private void SetStepBorder(int i, int j)
        {
            //Vizualize
            if (i > _text.SubString.Length)
                _text.MainString.ElementAt(i).IsBorder = true;
            _text.MainString.ElementAt(j).IsBorder = true;
        }

        protected virtual void OnAlgorythmFinished()
        {
            AlgorythmFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}