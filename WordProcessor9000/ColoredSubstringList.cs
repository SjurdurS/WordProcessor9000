using System;
using System.Collections;
using System.Collections.Generic;

namespace WordProcessor9000
{
    internal class ColoredSubstringList : IEnumerable<ColoredSubstring>
    {
        private readonly List<ColoredSubstring> _substrings;

        public ColoredSubstringList(ColoredSubstring cs)
        {
            _substrings = new List<ColoredSubstring>();
            _substrings.Add(cs);
        }

        public void Add(ColoredSubstring newCs)
        {
            int startIndexNew = newCs.StartIndex;
            int endIndexNew = newCs.EndIndex;

            int i = 0;
            int j = 0;

            for (i = 0; i < _substrings.Count; i++)
            {
                ColoredSubstring cs = _substrings[i];

                // Case 1.a start index is equal another. 
                if (startIndexNew == cs.StartIndex)
                {
                    if (endIndexNew < cs.EndIndex)
                    {
                        _substrings[i] = new ColoredSubstring(endIndexNew, cs.EndIndex, cs.ForegroundColor,
                            cs.BackgroundColor);
                        _substrings.Insert(i, newCs);
                    } // Replace substring if end indices are equal
                    else if (endIndexNew == cs.EndIndex)
                    {
                        _substrings[i] = newCs;
                    }
                    else
                    {
                        // endIndexNew > cs.EndIndex
                        for (j = i + 1; j < _substrings.Count; j++)
                        {
                            ColoredSubstring cs2 = _substrings[j];
                            if (endIndexNew == cs2.EndIndex)
                            {
                                _substrings[i] = newCs;
                                _substrings.Remove(cs2);
                                break;
                            }
                            if (endIndexNew < cs2.EndIndex)
                            {
                                _substrings.Insert(i, newCs);
                                _substrings[j] = new ColoredSubstring(endIndexNew, cs2.EndIndex, cs2.ForegroundColor,
                                    cs2.BackgroundColor);
                                _substrings.Remove(cs2);
                                break;
                            } // endIndexNew > cs2.EndIndex
                            _substrings.RemoveAt(j);
                            j--;
                        }
                    }
                } // Case 1.b Start is inside another.
                else if (startIndexNew > cs.StartIndex && startIndexNew <= cs.EndIndex)
                {
                    if (endIndexNew <= cs.EndIndex)
                    {
                        _substrings[i] = new ColoredSubstring(cs.StartIndex, startIndexNew, cs.ForegroundColor,
                            cs.BackgroundColor);
                        _substrings.Insert(i + 1, newCs);
                        if (endIndexNew + 1 < cs.EndIndex)
                        {
                            _substrings.Insert(i + 2,
                                new ColoredSubstring(endIndexNew, cs.EndIndex, cs.ForegroundColor,
                                    cs.BackgroundColor));
                        }
                    }
                    else
                    {
                        // if endIndexNew > cs.EndIndex
                        for (j = i + 1; j < _substrings.Count; j++)
                        {
                            ColoredSubstring cs2 = _substrings[j];
                            if (endIndexNew < cs2.EndIndex)
                            {
                                _substrings[i] = new ColoredSubstring(cs.StartIndex, startIndexNew,
                                    cs.ForegroundColor, cs.BackgroundColor);
                                _substrings.Insert(i + 1, newCs);
                                _substrings[j + 1] = new ColoredSubstring(endIndexNew, cs2.EndIndex,
                                    cs2.ForegroundColor, cs2.BackgroundColor);
                                break;
                            }
                            _substrings.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
        }

        public void PrintArray()
        {
            Console.WriteLine("");
            Console.WriteLine("---------- Print ------------");
            foreach (ColoredSubstring cs in _substrings)
            {
                cs.PrintMe();
            }

            Console.WriteLine("-----------------------------");
        }


        // Taken from here http://stackoverflow.com/questions/13135443/how-to-make-the-class-as-an-ienumerable-in-c
        #region Implementation of IEnumerable

        public IEnumerator<ColoredSubstring> GetEnumerator()
        {
            return _substrings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}