using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordProcessor9000;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUrlRegex()
        {
            String url1 =
                "http://feeds.reuters.com/~r/reuters/topNews/~3/ptoAzETqy3w/us-usa-neilarmstrong-idUSBRE87O0B020120825";
            String url2 =
                "https://feeds.reuters.com/~r/reuters/topNews/~3/ptoAzETqy3w/us-usa-neilarmstrong-idUSBRE87O0B020120825";
            String url3 =
                "http://www.strøm.dk/";
            String url4 =
                "ftp://www.strøm.dk/";

            Assert.IsTrue(CompiledRegexes.URL.Match(url1).Success);
            Assert.IsTrue(CompiledRegexes.URL.Match(url2).Success);
            Assert.IsTrue(CompiledRegexes.URL.Match(url3).Success);
            Assert.IsTrue(CompiledRegexes.URL.Match(url4).Success);
        }

        [TestMethod]
        public void TestDateRegex()
        {
            String date1 = "Sat, 25 Aug 2012";
            String date2 = "Sun, 26 Aug 1999";
            String date3 = "Tue, 1 Jan 2014";
            String date4 = "Wed, 01 June 2012";


            Assert.IsTrue(CompiledRegexes.Date.Match(date1).Success);
            Assert.IsTrue(CompiledRegexes.Date.Match(date2).Success);
            Assert.IsTrue(CompiledRegexes.Date.Match(date3).Success);
            Assert.IsTrue(CompiledRegexes.Date.Match(date4).Success);

        }
    }
}