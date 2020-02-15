/**************************************************************************************

WpfWindowsLibTest.TestCountryCode
=================================

Test for CountryCode

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfWindowsLib {


  [TestClass]
  public class CountryCodeTest {


    [TestMethod]
    public void TestCountryCode_GetCountry() {
      Assert.IsNull(CountryCode.GetCountry("0"));
      Assert.IsNull(CountryCode.GetCountry("00"));
      Assert.IsNull(CountryCode.GetCountry("01"));
      Assert.AreEqual("US", CountryCode.GetCountry("001")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("1")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("+1")!.Abbreviation2);

      Assert.AreEqual("US", CountryCode.GetCountry("0012")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("12")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("+12")!.Abbreviation2);

      Assert.AreEqual("US", CountryCode.GetCountry("00123")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("123")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("+123")!.Abbreviation2);

      Assert.AreEqual("US", CountryCode.GetCountry("001234")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("1234")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("+1234")!.Abbreviation2);

      Assert.AreEqual("US", CountryCode.GetCountry("00123456789")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("123456789")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("+123456789")!.Abbreviation2);

      Assert.AreEqual("US", CountryCode.GetCountry("00120")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("120")!.Abbreviation2);
      Assert.AreEqual("US", CountryCode.GetCountry("+120")!.Abbreviation2);

      Assert.AreEqual("CA", CountryCode.GetCountry("001204")!.Abbreviation2);
      Assert.AreEqual("CA", CountryCode.GetCountry("1204")!.Abbreviation2);
      Assert.AreEqual("CA", CountryCode.GetCountry("+1204")!.Abbreviation2);

      Assert.AreEqual("CA", CountryCode.GetCountry("0012041")!.Abbreviation2);
      Assert.AreEqual("CA", CountryCode.GetCountry("12041")!.Abbreviation2);
      Assert.AreEqual("CA", CountryCode.GetCountry("+12041")!.Abbreviation2);

      Assert.AreEqual("CA", CountryCode.GetCountry("00120412345678")!.Abbreviation2);
      Assert.AreEqual("CA", CountryCode.GetCountry("120412345678")!.Abbreviation2);
      Assert.AreEqual("CA", CountryCode.GetCountry("+120412345678")!.Abbreviation2);

      Assert.AreEqual("SG", CountryCode.GetCountry("0065")!.Abbreviation2);
      Assert.AreEqual("SG", CountryCode.GetCountry("65")!.Abbreviation2);
      Assert.AreEqual("SG", CountryCode.GetCountry("+65")!.Abbreviation2);

      Assert.AreEqual("SG", CountryCode.GetCountry("00651")!.Abbreviation2);
      Assert.AreEqual("SG", CountryCode.GetCountry("651")!.Abbreviation2);
      Assert.AreEqual("SG", CountryCode.GetCountry("+651")!.Abbreviation2);

      Assert.AreEqual("ZW", CountryCode.GetCountry("00263")!.Abbreviation2);
      Assert.AreEqual("ZW", CountryCode.GetCountry("263")!.Abbreviation2);
      Assert.AreEqual("ZW", CountryCode.GetCountry("+263")!.Abbreviation2);

      Assert.AreEqual("ZW", CountryCode.GetCountry("002631")!.Abbreviation2);
      Assert.AreEqual("ZW", CountryCode.GetCountry("2631")!.Abbreviation2);
      Assert.AreEqual("ZW", CountryCode.GetCountry("+2631")!.Abbreviation2);
    }


    [TestMethod]
    public void TestCountryCode_Format() {
      CountryCode.LocalFormat = CountryCode.LocalFormatNo0Area2; //need to reset default values because other tests might have changed them
      CountryCode.MaxLengthLocalCode = 9;
      Assert.AreEqual("", CountryCode.Format(""));
      Assert.AreEqual("0", CountryCode.Format("0"));
      Assert.AreEqual("+0", CountryCode.Format("+0"));
      Assert.AreEqual("+1 234567890", CountryCode.Format("(1) 23-456 78 90"));
      Assert.AreEqual("+1 234567890", CountryCode.Format("001 23 45 67890"));
      Assert.AreEqual("+1 234567890", CountryCode.Format("001234567890"));
      Assert.AreEqual("+1 234567890", CountryCode.Format("1234567890"));
      Assert.AreEqual("+1 234567890", CountryCode.Format("+1 234567890"));

      Assert.AreEqual("23-456 78 90", CountryCode.Format("(23) 456 78 90"));
      Assert.AreEqual("23-456 78 90", CountryCode.Format("234567890"));
      Assert.AreEqual("23-456 78 90", CountryCode.Format("23-456 78 90"));
      Assert.AreEqual("23-456 789", CountryCode.Format("0234 56789"));
      Assert.AreEqual("0234 5678", CountryCode.Format("0234 5678"));

      CountryCode.LocalFormat = CountryCode.LocalFormat0Area2;
      Assert.AreEqual("+1 234567890", CountryCode.Format("1234567890"));
      Assert.AreEqual("023-456 78 90", CountryCode.Format("(23) 456 78 90"));
      Assert.AreEqual("023-456 78 90", CountryCode.Format("234567890"));
      Assert.AreEqual("023-456 78 90", CountryCode.Format("023-456 78 90"));
      Assert.AreEqual("023-456 789", CountryCode.Format("0234 56789"));
      Assert.AreEqual("0234 5678", CountryCode.Format("0234 5678"));

      CountryCode.LocalFormat = CountryCode.LocalFormat0Area3;
      CountryCode.MaxLengthLocalCode = 10;
      Assert.AreEqual("0234-567 89 01", CountryCode.Format("(234) 567 89 01"));
      Assert.AreEqual("0234-567 89 01", CountryCode.Format("2345678901"));
      Assert.AreEqual("0234-567 89 01", CountryCode.Format("234-567 8901"));
      Assert.AreEqual("0234-567 890", CountryCode.Format("02345 67890"));
      Assert.AreEqual("0234 56789", CountryCode.Format("0234 56789"));

      CountryCode.LocalFormat = CountryCode.LocalFormat8Digits;
      CountryCode.MaxLengthLocalCode = 8;
      Assert.AreEqual("1234 5678", CountryCode.Format("1234 5678"));
      Assert.AreEqual("1234 5678", CountryCode.Format("12345678"));
    }
  }
}