using System;
using System.Collections.Generic;

namespace WordProcessor9000
{
    internal class ColoredSubstringList
    {
        private readonly List<ColoredSubstring> substrings;

        public ColoredSubstringList(ColoredSubstring cs)
        {
            substrings = new List<ColoredSubstring>();
            substrings.Add(cs);
        }

        public void add(ColoredSubstring newCs)
        {
            Console.WriteLine("");
            Console.WriteLine("Inserting new CS: ");
            newCs.PrintMe();
            int startIndexNew = newCs.StartIndex;
            int endIndexNew = newCs.EndIndex;

            int i = 0;
            int j = 0;

            for (i = 0; i < substrings.Count; i++)
            {
                ColoredSubstring cs = substrings[i];

                // Case 1.a start index is equal another. 
                if (startIndexNew == cs.StartIndex)
                {
                    if (endIndexNew < cs.EndIndex)
                    {
                        substrings[i] = new ColoredSubstring(endIndexNew, cs.EndIndex, cs.ForegroundColor,
                            cs.BackgroundColor);
                        substrings.Insert(i, newCs);
                    } // Replace substring if end indices are equal
                    else if (endIndexNew == cs.EndIndex)
                    {
                        substrings[i] = newCs;
                    }
                    else
                    {
                        // endIndexNew > cs.EndIndex
                        for (j = i + 1; j < substrings.Count; j++)
                        {
                            ColoredSubstring cs2 = substrings[j];
                            if (endIndexNew == cs2.EndIndex)
                            {
                                substrings[i] = newCs;
                                substrings.Remove(cs2);
                                break;
                            }
                            if (endIndexNew < cs2.EndIndex)
                            {
                                substrings.Insert(i, newCs);
                                substrings[j] = new ColoredSubstring(endIndexNew, cs2.EndIndex, cs2.ForegroundColor,
                                    cs2.BackgroundColor);
                                substrings.Remove(cs2);
                                break;
                            } // endIndexNew > cs2.EndIndex
                            substrings.RemoveAt(j);
                            j--;
                        }
                    }
                } // Case 1.b Start is inside another.
                else if (startIndexNew > cs.StartIndex && startIndexNew <= cs.EndIndex)
                {
                    if (endIndexNew <= cs.EndIndex)
                    {
                        substrings[i] = new ColoredSubstring(cs.StartIndex, startIndexNew, cs.ForegroundColor,
                            cs.BackgroundColor);
                        substrings.Insert(i + 1, newCs);
                        if (endIndexNew + 1 < cs.EndIndex)
                        {
                            substrings.Insert(i + 2,
                                new ColoredSubstring(endIndexNew, cs.EndIndex, cs.ForegroundColor,
                                    cs.BackgroundColor));
                        }
                    }
                    else
                    {
                        // if endIndexNew > cs.EndIndex
                        for (j = i + 1; j < substrings.Count; j++)
                        {
                            ColoredSubstring cs2 = substrings[j];
                            if (endIndexNew < cs2.EndIndex)
                            {
                                substrings[i] = new ColoredSubstring(cs.StartIndex, startIndexNew,
                                    cs.ForegroundColor, cs.BackgroundColor);
                                substrings.Insert(i + 1, newCs);
                                substrings[j + 1] = new ColoredSubstring(endIndexNew, cs2.EndIndex,
                                    cs2.ForegroundColor, cs2.BackgroundColor);
                                break;
                            }
                            substrings.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
        }

        public void printArray()
        {
            Console.WriteLine("");
            Console.WriteLine("---------- Print ------------");
            foreach (ColoredSubstring cs in substrings)
            {
                cs.PrintMe();
            }

            Console.WriteLine("-----------------------------");
        }

    }
}