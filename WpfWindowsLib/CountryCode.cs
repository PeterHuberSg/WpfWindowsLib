/**************************************************************************************

WpfWindowsLib.Country
=====================

Mostly dialing codes related information about countries.

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WpfWindowsLib {

  #region Country Class
  //      =============

  /// <summary>
  /// Holds some data for every country
  /// </summary>
  public class Country {
    /// <summary>
    /// Country name
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// International dialing code for country. If more than one, they are separated by ", ".
    /// </summary>
    public readonly string Codes;
    /// <summary>
    /// Length of international dialing code
    /// </summary>
    public readonly int CodeLength;
    /// <summary>
    /// 2 letters country name abbreviation.
    /// </summary>
    public readonly string Abbreviation2;
    /// <summary>
    /// 3 letters country name abbreviation.
    /// </summary>
    public readonly string Abbreviation3;

    /// <summary>
    /// Constructor
    /// </summary>
    public Country(string name, string codes, string abbreviation2, string abbreviation3) {
      Name = name;
      Codes = codes;
      Abbreviation2 = abbreviation2;
      Abbreviation3 = abbreviation3;
      var codesItems = codes.Split(", ");
      foreach (var code in codesItems) {
        if (CodeLength==0) {
          CodeLength = code.Length;
        } else {
          if (CodeLength!=code.Length) throw new Exception();
        }
      }
    }

    public override string ToString() {
      return $"Name: {Name}; Codes: {Codes}; Abbr2: {Abbreviation2}; Abbr3: {Abbreviation3};";
    }
  }
  #endregion


  #region Country Code Class
  //      ==================

  /// <summary>
  /// Helper methods to find country based on phone number and formatting the phone number.
  /// </summary>
  public static class CountryCode {

    #region Properties
    //      ----------

    /// <summary>
    /// Number of digits a local phone code (including area code) can maximal have. If more digits are provided, the
    /// number is considered to be an international dialing code.
    /// </summary>
    public static int MaxLengthLocalCode = 10; //2 area code and 8 local digits

    /// <summary>
    /// LocalFormat holds the function being used for formatting a local phone number (i.e. not an international dialing code).
    /// </summary>
    public static Func<string, string?>? LocalFormat = LocalFormatNo0Area2;


    /// <summary>
    /// Holds all countries sorted by Abbreviation3
    /// </summary>
    public static readonly SortedDictionary<string, Country> ByAbbreviation3 = new SortedDictionary<string, Country>();


    /// <summary>
    /// CountryCode.ByPhone holds like a dictionary all countries, indexed by their country code. But the storage is very
    /// different. The very first digit of the country code points to a DigitInfo. The second digit is used as index into
    /// DigitInfo.Digits, which returns another DigitInfo. Once all digits of a country code are used, the country is found.<para/>
    /// Examples:<para/> 
    /// country code 1: byPhone[1].Country is US<para/>
    /// country code 1236: byPhone[1].Digits[2].Digits[3].Digits[6].Country is Canada<para/>
    /// country code 1235: byPhone[1].Digits[2].Digits[3].Digits[5].Country is null. Since byPhone[1].Country is US, also 1235
    /// is US, because no other country was found in the later digits
    /// </summary>
    public static readonly DigitInfo?[] ByPhone = new DigitInfo?[10];
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Used in CountryCode.ByPhone to store some information for each digit 
    /// </summary>
    public class DigitInfo {
      /// <summary>
      /// Digit value
      /// </summary>
      public char Digit;
      /// <summary>
      /// If not null, the number of previous digits and this digit define the country code
      /// </summary>
      public Country? Country;
      /// <summary>
      /// An array[0..9]. If an array cell holds a DigitInfo, the corresponding digit is used in some country codes.
      /// </summary>
      public DigitInfo?[]? Digits;

      /// <summary>
      /// Constructor
      /// </summary>
      public DigitInfo(char digit) {
        Digit = digit;
      }


      public override string ToString() {
        var digits = "";
        if (Digits!=null) {
          foreach (var digit in Digits) {
            if (digit!=null) {
              digits += digit.Digit;
            }
          }
        }
        return $"Digit: {Digit}; Country: {Country?.Name}, {Country?.Codes}; Digits: {digits};";
      }
    }


    private static void add(string name, string code, string abbreviation2, string abbreviation3) {
      var country = new Country(name, code, abbreviation2, abbreviation3);
      ByAbbreviation3.Add(country.Abbreviation3, country);
    }


    /// <summary>
    /// default static constructor
    /// </summary>
    static CountryCode() {
      add("Afghanistan", "93", "AF", "AFG");
      add("Albania", "355", "AL", "ALB");
      add("Algeria", "213", "DZ", "DZA");
      add("American Samoa", "1684", "AS", "ASM");
      add("Andorra", "376", "AD", "AND");
      add("Angola", "244", "AO", "AGO");
      add("Anguilla", "1264", "AI", "AIA");
      add("Antarctica", "672", "AQ", "ATA");
      add("Antigua and Barbuda", "1268", "AG", "ATG");
      add("Argentina", "54", "AR", "ARG");
      add("Armenia", "374", "AM", "ARM");
      add("Aruba", "297", "AW", "ABW");
      add("Australia", "61", "AU", "AUS");
      add("Austria", "43", "AT", "AUT");
      add("Azerbaijan", "994", "AZ", "AZE");
      add("Bahamas", "1242", "BS", "BHS");
      add("Bahrain", "973", "BH", "BHR");
      add("Bangladesh", "880", "BD", "BGD");
      add("Barbados", "1246", "BB", "BRB");
      add("Belarus", "375", "BY", "BLR");
      add("Belgium", "32", "BE", "BEL");
      add("Belize", "501", "BZ", "BLZ");
      add("Benin", "229", "BJ", "BEN");
      add("Bermuda", "1441", "BM", "BMU");
      add("Bhutan", "975", "BT", "BTN");
      add("Bolivia", "591", "BO", "BOL");
      add("Bosnia and Herzegovina", "387", "BA", "BIH");
      add("Botswana", "267", "BW", "BWA");
      add("Brazil", "55", "BR", "BRA");
      add("British Indian Ocean Territory", "246", "IO", "IOT");
      add("British Virgin Islands", "1284", "VG", "VGB");
      add("Brunei", "673", "BN", "BRN");
      add("Bulgaria", "359", "BG", "BGR");
      add("Burkina Faso", "226", "BF", "BFA");
      add("Burundi", "257", "BI", "BDI");
      add("Cambodia", "855", "KH", "KHM");
      add("Cameroon", "237", "CM", "CMR");
      add("Canada", "1204, 1226, 1236, 1249, 1289, 1306, 1364, 1365, 1367, 1358, 1403, 1416, 1418, 1431, 1437, 1438, 1474, 1506, 2514, 1519, 1579, 1581, 1587, 1604, 1613, 1639, 1647, 1672, 1705, 1709, 1778, 1780, 1782, 1807, 1819, 1825, 1879, 1899, 1902, 1905", "CA", "CAN");
      add("Cape Verde", "238", "CV", "CPV");
      add("Cayman Islands", "1345", "KY", "CYM");
      add("Central African Republic", "236", "CF", "CAF");
      add("Chad", "235", "TD", "TCD");
      add("Chile", "56", "CL", "CHL");
      add("China", "86", "CN", "CHN");
      add("Colombia", "57", "CO", "COL");
      add("Comoros", "269", "KM", "COM");
      add("Cook Islands", "682", "CK", "COK");
      add("Costa Rica", "506", "CR", "CRI");
      add("Croatia", "385", "HR", "HRV");
      add("Cuba", "53", "CU", "CUB");
      add("Cyprus", "357", "CY", "CYP");
      add("Czech Republic", "420", "CZ", "CZE");
      add("Democratic Republic of the Congo", "243", "CD", "COD");
      add("Denmark", "45", "DK", "DNK");
      add("Djibouti", "253", "DJ", "DJI");
      add("Dominica", "1767", "DM", "DMA");
      add("Dominican Republic", "1809, 1829, 1849", "DO", "DOM");
      add("East Timor", "670", "TL", "TLS");
      add("Ecuador", "593", "EC", "ECU");
      add("Egypt", "20", "EG", "EGY");
      add("El Salvador", "503", "SV", "SLV");
      add("Equatorial Guinea", "240", "GQ", "GNQ");
      add("Eritrea", "291", "ER", "ERI");
      add("Estonia", "372", "EE", "EST");
      add("Ethiopia", "251", "ET", "ETH");
      add("Falkland Islands", "500", "FK", "FLK");
      add("Faroe Islands", "298", "FO", "FRO");
      add("Fiji", "679", "FJ", "FJI");
      add("Finland", "358", "FI", "FIN");
      add("France", "33", "FR", "FRA");
      add("French Antilles", "590", "FA", "FAN");
      add("French Polynesia", "689", "PF", "PYF");
      add("Gabon", "241", "GA", "GAB");
      add("Gambia", "220", "GM", "GMB");
      add("Georgia", "995", "GE", "GEO");
      add("Germany", "49", "DE", "DEU");
      add("Ghana", "233", "GH", "GHA");
      add("Gibraltar", "350", "GI", "GIB");
      add("Greece", "30", "GR", "GRC");
      add("Greenland", "299", "GL", "GRL");
      add("Grenada", "1473", "GD", "GRD");
      add("Guam", "1671", "GU", "GUM");
      add("Guatemala", "502", "GT", "GTM");
      add("Guernsey", "441481", "GG", "GGY");
      add("Guinea", "224", "GN", "GIN");
      add("Guinea-Bissau", "245", "GW", "GNB");
      add("Guyana", "592", "GY", "GUY");
      add("Haiti", "509", "HT", "HTI");
      add("Honduras", "504", "HN", "HND");
      add("Hong Kong", "852", "HK", "HKG");
      add("Hungary", "36", "HU", "HUN");
      add("Iceland", "354", "IS", "ISL");
      add("India", "91", "IN", "IND");
      add("Indonesia", "62", "ID", "IDN");
      add("Iran", "98", "IR", "IRN");
      add("Iraq", "964", "IQ", "IRQ");
      add("Ireland", "353", "IE", "IRL");
      add("Isle of Man", "441624", "IM", "IMN");
      add("Israel", "972", "IL", "ISR");
      add("Italy", "39", "IT", "ITA");
      add("Ivory Coast", "225", "CI", "CIV");
      add("Jamaica", "1876", "JM", "JAM");
      add("Japan", "81", "JP", "JPN");
      add("Jersey", "441534", "JE", "JEY");
      add("Jordan", "962", "JO", "JOR");
      add("Kazakhstan", "731, 732, 733", "KZ", "KAZ");
      add("Kenya", "254", "KE", "KEN");
      add("Kiribati", "686", "KI", "KIR");
      add("Kosovo", "383", "XK", "XKX");
      add("Kuwait", "965", "KW", "KWT");
      add("Kyrgyzstan", "996", "KG", "KGZ");
      add("Laos", "856", "LA", "LAO");
      add("Latvia", "371", "LV", "LVA");
      add("Lebanon", "961", "LB", "LBN");
      add("Lesotho", "266", "LS", "LSO");
      add("Liberia", "231", "LR", "LBR");
      add("Libya", "218", "LY", "LBY");
      add("Liechtenstein", "423", "LI", "LIE");
      add("Lithuania", "370", "LT", "LTU");
      add("Luxembourg", "352", "LU", "LUX");
      add("Macau", "853", "MO", "MAC");
      add("Macedonia", "389", "MK", "MKD");
      add("Madagascar", "261", "MG", "MDG");
      add("Malawi", "265", "MW", "MWI");
      add("Malaysia", "60", "MY", "MYS");
      add("Maldives", "960", "MV", "MDV");
      add("Mali", "223", "ML", "MLI");
      add("Malta", "356", "MT", "MLT");
      add("Marshall Islands", "692", "MH", "MHL");
      add("Mauritania", "222", "MR", "MRT");
      add("Mauritius", "230", "MU", "MUS");
      add("Mexico", "52", "MX", "MEX");
      add("Micronesia", "691", "FM", "FSM");
      add("Moldova", "373", "MD", "MDA");
      add("Monaco", "377", "MC", "MCO");
      add("Mongolia", "976", "MN", "MNG");
      add("Montenegro", "382", "ME", "MNE");
      add("Montserrat", "1664", "MS", "MSR");
      add("Morocco", "212", "MA", "MAR");
      add("Mozambique", "258", "MZ", "MOZ");
      add("Myanmar", "95", "MM", "MMR");
      add("Namibia", "264", "NA", "NAM");
      add("Nauru", "674", "NR", "NRU");
      add("Nepal", "977", "NP", "NPL");
      add("Netherlands", "31", "NL", "NLD");
      add("Netherlands Antilles", "599", "AN", "ANT");
      add("New Caledonia", "687", "NC", "NCL");
      add("New Zealand", "64", "NZ", "NZL");
      add("Nicaragua", "505", "NI", "NIC");
      add("Niger", "227", "NE", "NER");
      add("Nigeria", "234", "NG", "NGA");
      add("Niue", "683", "NU", "NIU");
      add("North Korea", "850", "KP", "PRK");
      add("Northern Mariana Islands", "1670", "MP", "MNP");
      add("Norway", "47", "NO", "NOR");
      add("Oman", "968", "OM", "OMN");
      add("Pakistan", "92", "PK", "PAK");
      add("Palau", "680", "PW", "PLW");
      add("Palestine", "970", "PS", "PSE");
      add("Panama", "507", "PA", "PAN");
      add("Papua New Guinea", "675", "PG", "PNG");
      add("Paraguay", "595", "PY", "PRY");
      add("Peru", "51", "PE", "PER");
      add("Philippines", "63", "PH", "PHL");
      add("Poland", "48", "PL", "POL");
      add("Portugal", "351", "PT", "PRT");
      add("Puerto Rico", "1787, 1939", "PR", "PRI");
      add("Qatar", "974", "QA", "QAT");
      add("Republic of the Congo", "242", "CG", "COG");
      add("Reunion / Mayotte", "262", "RE", "REU");
      add("Romania", "40", "RO", "ROU");
      add("Russia", "7", "RU", "RUS");
      add("Rwanda", "250", "RW", "RWA");
      add("Saint Helena", "290", "SH", "SHN");
      add("Saint Kitts and Nevis", "1869", "KN", "KNA");
      add("Saint Lucia", "1758", "LC", "LCA");
      add("Saint Pierre and Miquelon", "508", "PM", "SPM");
      add("Saint Vincent and the Grenadines", "1784", "VC", "VCT");
      add("Samoa", "685", "WS", "WSM");
      add("San Marino", "378", "SM", "SMR");
      add("Sao Tome and Principe", "239", "ST", "STP");
      add("Saudi Arabia", "966", "SA", "SAU");
      add("Senegal", "221", "SN", "SEN");
      add("Serbia", "381", "RS", "SRB");
      add("Seychelles", "248", "SC", "SYC");
      add("Sierra Leone", "232", "SL", "SLE");
      add("Singapore", "65", "SG", "SGP");
      add("Sint Maarten", "1721", "SX", "SXM");
      add("Slovakia", "421", "SK", "SVK");
      add("Slovenia", "386", "SI", "SVN");
      add("Solomon Islands", "677", "SB", "SLB");
      add("Somalia", "252", "SO", "SOM");
      add("South Africa", "27", "ZA", "ZAF");
      add("South Korea", "82", "KR", "KOR");
      add("South Sudan", "211", "SS", "SSD");
      add("Spain", "34", "ES", "ESP");
      add("Sri Lanka", "94", "LK", "LKA");
      add("Sudan", "249", "SD", "SDN");
      add("Suriname", "597", "SR", "SUR");
      add("Swaziland", "268", "SZ", "SWZ");
      add("Sweden", "46", "SE", "SWE");
      add("Switzerland", "41", "CH", "CHE");
      add("Syria", "963", "SY", "SYR");
      add("Taiwan", "886", "TW", "TWN");
      add("Tajikistan", "992", "TJ", "TJK");
      add("Tanzania", "255", "TZ", "TZA");
      add("Thailand", "66", "TH", "THA");
      add("Togo", "228", "TG", "TGO");
      add("Tokelau", "690", "TK", "TKL");
      add("Tonga", "676", "TO", "TON");
      add("Trinidad and Tobago", "1868", "TT", "TTO");
      add("Tunisia", "216", "TN", "TUN");
      add("Turkey", "90", "TR", "TUR");
      add("Turkmenistan", "993", "TM", "TKM");
      add("Turks and Caicos Islands", "1649", "TC", "TCA");
      add("Tuvalu", "688", "TV", "TUV");
      add("U.S. Virgin Islands", "1340", "VI", "VIR");
      add("Uganda", "256", "UG", "UGA");
      add("Ukraine", "380", "UA", "UKR");
      add("United Arab Emirates", "971", "AE", "ARE");
      add("United Kingdom", "44", "GB", "GBR");
      add("United States", "1", "US", "USA");
      add("Uruguay", "598", "UY", "URY");
      add("Uzbekistan", "998", "UZ", "UZB");
      add("Vanuatu", "678", "VU", "VUT");
      add("Vatican", "379", "VA", "VAT");
      add("Venezuela", "58", "VE", "VEN");
      add("Vietnam", "84", "VN", "VNM");
      add("Wallis and Futuna", "681", "WF", "WLF");
      add("Yemen", "967", "YE", "YEM");
      add("Zambia", "260", "ZM", "ZMB");
      add("Zimbabwe", "263", "ZW", "ZWE");

      DigitInfo root = new DigitInfo(' ') {
        Digits = ByPhone
      };
      foreach (var country in ByAbbreviation3.Values) {
        var codes = country.Codes.Split(", ");
        foreach (var code in codes) {
          DigitInfo parentDigitInfos = root;
          DigitInfo? thisDigitInfo = null;
          foreach (var digit in code) {
            if (parentDigitInfos.Digits is null) {
              parentDigitInfos.Digits = new DigitInfo[10];
            }
            thisDigitInfo = parentDigitInfos.Digits[digit-'0'];
            if (thisDigitInfo is null) {
              thisDigitInfo = new DigitInfo(digit);
              parentDigitInfos.Digits[digit-'0'] = thisDigitInfo;
            }
            parentDigitInfos = thisDigitInfo;
          }
          if (thisDigitInfo!.Country!=null) throw new Exception();

          thisDigitInfo!.Country = country;
        }
      }
    }


    /// <summary>
    /// Formats a phone number. If phoneNumber starts with '+' or "00" or is longer than MaxLengthLocalCode its
    /// formatted like an international dialing code, i.e. '+' country-code local-code. If it is not international,
    /// LocalFormat() is called for formatting.
    /// </summary>
    public static string Format(string phoneNumber) {
      if (phoneNumber.Length==0) return phoneNumber;

      var isInternational = phoneNumber[0]=='+';
      var isLocal = false;
      var startIndex = 0;
      if (phoneNumber.Length<6) return phoneNumber;

      if (phoneNumber[0]=='0') {
        if (phoneNumber[1]=='0') {
          isInternational = true;
          startIndex = 2;
        } else {
          //'0': sign for trunk call, remove it
          isLocal = true;
          startIndex = 1;
        }
      }
      Span<Char> digits = stackalloc char[phoneNumber.Length];
      extract(phoneNumber, digits, startIndex, out int length);

      if (!isLocal && length>MaxLengthLocalCode) {
        isInternational = true;
      }
      if (!isInternational) {
        if (LocalFormat is null) {
          return digits[..length].ToString();
        }
        return LocalFormat(digits[..length].ToString())??phoneNumber;//unfortunately, Span cannot be used in generics Func<Span<char>, string> LocalFormat
      }

      var country = GetCountry(digits[0..length]);
      if (country is null) return phoneNumber;

      const int formatCharsLength = 2; //some space for '+' and a blank
      Span<Char> formattedDigits = stackalloc char[length + formatCharsLength];
      formattedDigits[0] = '+';
      var formattedDigitsIndex = 1;
      for (int digitIndex = 0; digitIndex < length; digitIndex++) {
        if (digitIndex==country.CodeLength) {
          formattedDigits[formattedDigitsIndex++] = ' ';
        }
        formattedDigits[formattedDigitsIndex++] = digits[digitIndex];
      }
      return formattedDigits.ToString();
    }


    /// <summary>
    /// Local phone number format for countries with 2 digits for area code (no leading '0'). Returns
    /// null when digits has not 8 digits.
    /// </summary>
    public static string? LocalFormatNo0Area2(string digits) {
      if (digits.Length<8) return null;
      if (digits.Length==8) return digits[..2] + "-" + digits[2..5] + " " + digits[5..];
      if (digits.Length==9) return digits[..2] + "-" + digits[2..5] + " " + digits[5..7] + " " + digits[7..];
      if (digits.Length==10) return digits[..2] + "-" + digits[2..6] + " " + digits[6..];
      return null;
    }


    /// <summary>
    /// Local phone number format for countries with '0' followed by 2 digits for area code. Returns
    /// null when digits has not 8 digits.
    /// </summary>
    public static string? LocalFormat0Area2(string digits) {
      if (digits.Length<8) return null;
      if (digits.Length==8) return "0" + digits[..2] + "-" + digits[2..5] + " " + digits[5..];
      if (digits.Length==9) return "0" + digits[..2] + "-" + digits[2..5] + " " + digits[5..7] + " " + digits[7..];
      if (digits.Length==10) return "0" + digits[..2] + "-" + digits[2..6] + " " + digits[6..];
      return null;
    }


    /// <summary>
    /// Local phone number format for countries with 3 digits for area code (no leading '0'). Returns
    /// null when digits has not 8 digits.
    /// </summary>
    public static string? LocalFormatNo0Area3(string digits) {
      if (digits.Length<9) return null;
      if (digits.Length==9) return digits[..3] + "-" + digits[3..6] + " " + digits[6..];
      if (digits.Length==10) return digits[..3] + "-" + digits[3..6] + " " + digits[6..8] + " " + digits[8..];
      if (digits.Length==11) return digits[..3] + "-" + digits[3..7] + " " + digits[7..];
      return null;
    }


    /// <summary>
    /// Local phone number format for countries with '0' followed by 3 digits for area code. Returns
    /// null when digits has not 8 digits.
    /// </summary>
    public static string? LocalFormat0Area3(string digits) {
      if (digits.Length<9) return null;
      if (digits.Length==9) return "0" + digits[..3] + "-" + digits[3..6] + " " + digits[6..];
      if (digits.Length==10) return "0" + digits[..3] + "-" + digits[3..6] + " " + digits[6..8] + " " + digits[8..];
      if (digits.Length==11) return "0" + digits[..3] + "-" + digits[3..7] + " " + digits[7..];
      return null;
    }


    /// <summary>
    /// Local phone number format for countries with exactly 8 digits and no '0' as prefix for area code. Returns
    /// null when digits has not 8 digits.
    /// </summary>
    public static string? LocalFormat8Digits(string digits) {
      if (digits.Length!=MaxLengthLocalCode) return null;

      return digits.Insert(4, " ");
    }


    private static void extract(string phoneNumber, Span<char> digits, int startIndex, out int length) {
      length = 0;
      for (int charIndex = startIndex; charIndex < phoneNumber.Length; charIndex++) {
        var digitChar = phoneNumber[charIndex];
        if (digitChar>='0' && digitChar<='9') {
          digits[length++] = digitChar;
        }
      }
    }


    /// <summary>
    /// Returns the Country based on an international dialing code.
    /// </summary>
    public static Country? GetCountry(string phoneNumber) {
      var startIndex = 0;
      if (phoneNumber.Length>=2 && phoneNumber[0]=='0' &&  phoneNumber[1]=='0') {
        startIndex = 2;
      }
      if (string.IsNullOrWhiteSpace(phoneNumber)) return null;
      Span<Char> digits = stackalloc char[phoneNumber.Length];
      extract(phoneNumber, digits, startIndex, out int length);
      return GetCountry(digits[0..length]);
    }


    /// <summary>
    /// Returns the Country based on an international dialing code.
    /// </summary>
    public static Country? GetCountry(ReadOnlySpan<char> phoneNumber) {
      if (phoneNumber.Length==0) return null;

      var isFirstDigit = true;
      DigitInfo? digitInfo = null;
      Country? country = null;
      foreach (var digitChar in phoneNumber) {
        var digitIndex = digitChar - '0';
        if (isFirstDigit) {
          isFirstDigit = false;
          digitInfo = ByPhone[digitIndex];
        } else {
          if (digitInfo!.Digits is null) return country;

          digitInfo = digitInfo.Digits[digitIndex];
        }
        if (digitInfo is null) return country;

        country = digitInfo.Country??country;
      }
      return country;
    }
    #endregion
  }
  #endregion
}
