/**************************************************************************************

WpfWindowsLib.EmailValidator
============================

Helper class checking if email address is valid. Does not require WPF.

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;


namespace WpfWindowsLib {


  /// <summary>
  /// Helper class checking if email address is valid. Does not require WPF.
  /// </summary>
  public static class EmailValidator {

    #region Properties
    //      ----------

    public const string AsciiSpecialCharsDefault = ".@-_+";


    /// <summary>
    /// Characters allowed in the local-part of the email address (before @ sign).
    /// </summary>
    public static string AsciiSpecialChars { get; set; } = AsciiSpecialCharsDefault;


    /// <summary>
    /// Allows only the special characters ".@-_+" in an email address
    /// </summary>
    public static void SetAsciiSpecialCharsDefault() {
      AsciiSpecialChars = AsciiSpecialCharsDefault;
    }


    /// <summary>
    /// Allows extended ASCII control characters ".@-_+!#$%&'*/=?^`{|}~" in an email address
    /// </summary>
    public static void SetExtendedAsciiSpecialChars() {
      AsciiSpecialChars = ".@-_+!#$%&'*/=?^`{|}~";
    }


    /// <summary>
    /// Allows extended ASCII and quoted control characters ".@-_+!#$%&'*/=?^`{|}~"(),:;<>[\]" in an email address
    /// </summary>
    public static void SetExtendedQuotedAsciiSpecialChars() {
      AsciiSpecialChars = ".@-_+!#$%&'*/=?^`{|}~\"(),:;<>[\\]";
    }


    /// <summary>
    /// is a blank ' ' allowed in the local-part of the email address ?
    /// </summary>
    public static bool IsBlankAllowed { get; set; } = false;


    /// <summary>
    /// is any character greater than UTF8 U+007F allowed ?
    /// </summary>
    public static bool IsInternationalCharSetAllowed { get; set; } = false;


    /// <summary>
    /// Validates if char is valid in the local-part of an email address
    /// </summary>
    public static Func<char, bool> IsValidEmailChar { get; set; } = IsValidEmailCharDefault;


    /// <summary>
    /// Validates if char is valid in the domain-part of an email address
    /// </summary>
    public static Func<char, bool> IsValidDnsChar { get; set; } = IsValidEmailDnsDefault;


    /// <summary>
    /// Validates complete email address
    /// </summary>
    public static Func<string, bool> IsValidEmail { get; set; } = IsValidEmailDefault;
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Validates if char is valid in the local-part of an email address
    /// </summary>
    public static bool IsValidEmailCharDefault(char emailChar) {
      if ((emailChar>='a' && emailChar<='z') ||
        (emailChar>='A' && emailChar<='Z') ||
        (emailChar>='0' && emailChar<='9')) {
        return true;
      }
      foreach (var specialChar in AsciiSpecialChars) {
        if (emailChar==specialChar) return true;
      }
      if (IsBlankAllowed && emailChar==' ') return true;

      if (IsInternationalCharSetAllowed && emailChar>0x7F) return true;

      return false;
    }


    /// <summary>
    /// Validates if char is valid in the domain-part of an email address
    /// </summary>
    public static bool IsValidEmailDnsDefault(char emailChar) {
      return (emailChar>='a' && emailChar<='z') ||
        (emailChar>='A' && emailChar<='Z') ||
        (emailChar>='0' && emailChar<='9') ||
        (emailChar=='.') ||
        (emailChar=='-');
    }


    static readonly EmailAddressAttribute emailValidatorMS = new EmailAddressAttribute();


    /// <summary>
    /// Validates if text is a valid email address. Uses a regex expression from Microsoft too complicated for
    /// me to understand:
    /// pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
    /// it is also not 100% correct, for example it accepts '1@1.', which is probably not a valid email address because
    /// of the trailing '.'.
    /// </summary>
    public static bool IsValidEmailMicrosoft(string address) {
      return emailValidatorMS.IsValid(address);
    }


    /// <summary>
    /// Simple test if email address local-part@domain-part follows some basic rules:<para/> 
    /// + has exactly one @ separating 2 strings<para/>
    /// + local-part is 1 to 64 characters long
    /// + has at least one dot in the domain-part<para/>
    /// + domain-part is 4 to 254-local-part.Lenght characters long
    /// + has at least four characters in the domain-part<para/>
    /// + has not 2 consecutive dots '.' in domain part
    /// + only legal characters
    /// </summary>
    public static bool IsValidEmailDefault(string address) {
      //inspired by
      //https://softwareengineering.stackexchange.com/questions/78353/how-far-should-one-take-e-mail-address-validation/78363#78363
      //@ testing
      var atIndex = address.IndexOf("@");
      if (atIndex==0 || address.LastIndexOf(".")<=atIndex) return false;//checks implicitly that '@' cannot be the last character

      //local-part testing
      var addressSpan = address.AsSpan();
      var localPart = addressSpan[..atIndex];
      if (localPart.Length>64) return false;

      foreach (var addressChar in localPart) {
        if (!IsValidEmailChar(addressChar)) return false;
      }

      //domain-part testing
      var domainPart = addressSpan[(atIndex+1)..];
      if (domainPart.Length<4 || domainPart.Length>255) return false;

      var isDot = false;
      foreach (var addressChar in domainPart) {
        if (!IsValidDnsChar(addressChar)) return false;
        if (isDot) {
          if (addressChar=='.') return false;

          isDot = false;
        } else {
          isDot = addressChar=='.';
        }
      }

      return true;
    }
    #endregion
  }
}
