using System;
using System.Collections;
using System.Collections.Generic;

namespace WordProcessor9000
{
    /// <summary>
    /// This class represents a sorted list of colored substrings that represent indices of another string.
    /// The ColoredSubstringList is used to easily color a string at various indices with different colors.
    /// The last added ColoredSubstring has highest precedence and overrules all previous inserted 
    /// ColoredSubstrings.
    /// 
    /// The algorithm for the Add method works as follows:
    ///     Each substring contains the values [x y C1 C2] where x is start index, 
    ///     y is end index, C1 is foreground color and C2 is background color.
    /// 
    /// 1.  If the substring has equal indices as another substring overwrite that substring.
    /// 
    /// 2.  If substring A is inside substring B split substring B around substring A.
    ///     Example_1:
    /// 
    ///             Contents before adding
    ///         [ 0  100  C1  C2] - substring B
    /// 
    ///             insert substring A [20 40 C3 C4]
    /// 
    ///             Contents after adding
    ///         [ 0   20  C1  C2] - substring Bsplit1
    ///         [20   40  C3  C4] - substring A
    ///         [40  100  C1  C2] - substring Bsplit2
    /// 
    /// 3.  If substring A starts inside substring B and ends in another substring D 
    ///     remove all substrings {C...Cn} between B and D and modify indices on 
    ///     substring B and D according to substring A.
    ///     Example_1:
    /// 
    ///             Contents before adding
    ///         [  0   10  C1  C2] - substring B
    ///         [ 10  100  C3  C4] - substring C1
    ///         [100  150  C1  C2] - substring C2
    ///         [150  250  C3  C4] - substring C3
    ///         [250  400  C5  C6] - substring D
    /// 
    ///             insert substring A [5 300 C7 C8]
    /// 
    ///             Contents after adding
    ///         [  0    5  C1  C2] - substring B'
    ///         [  5  300  C7  C8] - substring A
    ///         [300  400  C5  C6] - substring D'
    /// </summary>
    internal class ColoredSubstringList : IEnumerable<ColoredSubstring>
    {
        private readonly List<ColoredSubstring> _substrings;

        /// <summary>
        ///     Instantiate the SubstringList. Always instantiate the list with the full string.
        ///     In our case instantiate the list with the contents of the imported text document.
        /// </summary>
        /// <param name="cs">The full string you want to split into various substrings.</param>
        public ColoredSubstringList(ColoredSubstring cs)
        {
            _substrings = new List<ColoredSubstring>();
            _substrings.Add(cs);
        }

        /// <summary>
        ///     Add a new ColoredSubstring to the list of substrings.  
        /// </summary>
        /// <param name="newCs">The ColoredSubstring to insert into the list of substrings</param>
        public void Add(ColoredSubstring newCs)
        {
            int startIndexNew = newCs.StartIndex;
            int endIndexNew = newCs.EndIndex;

            int i = 0;
            int j = 0;

            for (i = 0; i < _substrings.Count; i++)
            {
                ColoredSubstring cs = _substrings[i];

                // Case 1.a start index is equal another start index. 
                if (startIndexNew == cs.StartIndex)
                {
                    if (endIndexNew < cs.EndIndex)
                    {
                        _substrings[i] = new ColoredSubstring(endIndexNew, cs.EndIndex, cs.ForegroundColor,
                            cs.BackgroundColor);
                        _substrings.Insert(i, newCs);
                    }
                    else if (endIndexNew == cs.EndIndex) // Replace substring if end indices are equal
                    {
                        _substrings[i] = newCs;
                    }
                    else // endIndexNew > cs.EndIndex
                    {
                        
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
                } // Case 1.b Start is inside a substring.
                else if (startIndexNew > cs.StartIndex && startIndexNew <= cs.EndIndex)
                {
                    if (endIndexNew <= cs.EndIndex)
                    {
                        _substrings[i] = new ColoredSubstring(cs.StartIndex, startIndexNew, cs.ForegroundColor,
                            cs.BackgroundColor);
                        _substrings.Insert(i + 1, newCs);
                        if (endIndexNew < cs.EndIndex)
                        {
                            _substrings.Insert(i + 2,
                                new ColoredSubstring(endIndexNew, cs.EndIndex, cs.ForegroundColor,
                                    cs.BackgroundColor));
                        }
                    }
                    else
                    {
                        // if endIndexNew > cs.EndIndex .. Start is inside one substring but End is inside another substring.
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
                            _substrings.RemoveAt(j); // Remove substrings that are between the two start and end substrings.
                            j--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Write the contents of the list to the console.
        /// </summary>
        public void Print()
        {
            Console.WriteLine("------------- Substrings --------------");
            foreach (ColoredSubstring cs in _substrings)
            {
                Console.WriteLine(cs);
            }
            Console.WriteLine("---------------------------------------");
        }


        // The implementation of IEnumerable is borrowed from here:
        // http://stackoverflow.com/questions/13135443/how-to-make-the-class-as-an-ienumerable-in-c
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