using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordProcessor9000
{
    /// <summary>
    /// This class stores compiled regexes that are regularly used.
    /// </summary>
    class CompiledRegexes
    {

        /// <summary>
        /// This regex matches the format of the dates in the given example text file:
        /// "DDD, dd MMM(M) YYYY" 
        /// Where:
        ///  DDD    is the three letter abbreviation of the day's name
        ///  dd     is a one or two digit date
        ///  MMM(M) is the abbreviation of the Month
        ///  YYYY   is either 1xxx or 2xxx.
        /// </summary>
        public static readonly Regex Date = new Regex(
                    @"(?i:mon|tue|wed|thu|fri|sat|sun),\s\d\d?\s(?i:jan|feb|mar|apr|may|june|july|aug|sept|oct|nov|dec)\s(?:[12]\d{3})");


        /// <summary>
        /// This regex matches all URLs of form:
        ///  http(s) or ftp :// address
        /// Example:
        ///  http://www.feeds.reuters.com/~r/reuters/topNews/~3/ptoAzETqy3w/us-usa-neilarmstrong-idUSBRE87O0B020120825
        /// High ASCII characters are also supported. Live example is "http://www.strøm.dk".
        /// Allowed characters found here: http://tools.ietf.org/html/rfc3986#appendix-A
        /// </summary>
        public static readonly Regex URL =
                new Regex(
                    @"(?i:https?|ftp)://(?i:[\w+?\.\w+])+(?i:[\w\~\!\@\#\$%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");



        /// <summary>
        /// This regex matches user queries that are of the form 'keyword*' where '*' is the character literal.
        /// This is used by the user to find words that start with a keyword.
        /// </summary>
        public static readonly Regex StartsWith = new Regex(@"(\b\w*)\*+");
        /// <summary>
        /// Replace the match found by regex StartsWith with this regex.
        /// </summary>
        public static readonly Regex StartsWithReplacement = new Regex(@"\b$1\w*\b");


        /// <summary>
        /// This regex matches user queries that are of the form '*keyword' where '*' is the character literal.
        /// This is used by the user to find words that end with a keyword.
        /// </summary>
        public static readonly Regex EndsWith = new Regex(@"\*+(\w*\b)");
        /// <summary>
        /// Replace the match found by regex EndsWith with this regex.
        /// </summary>
        public static readonly Regex EndsWithReplacement = new Regex(@"\b\w*$1\b");
        

    }
}
