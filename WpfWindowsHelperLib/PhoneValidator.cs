/**************************************************************************************

WpfWindowsLib.PhoneValidator
============================

Helper class checking if phone number is valid. Does not require WPF.

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
using System.Text;


namespace WpfWindowsLib {


  /// <summary>
  /// Helper class checking if phone number is valid. Does not require WPF.
  /// </summary>
  public static class PhoneValidator {

    #region Properties
    //      ----------

    /// <summary>
    /// Characters that user can input for phone numbers, additionally to the digits '0' to '9' and a
    /// '+' as first character.
    /// </summary>
    public static char[] LegalSpecialCharacters = {'-', '/'};


    /// <summary>
    /// ValidateUserInput gets called every time a user has keyed in a character, except when he enters a blank. Assign
    /// a different method to change how user input gets validated.
    /// </summary>
    public static Func<char /*inputChar*/, int /*inputPos*/, string /*partialPhoneNumber*/, bool> ValidateUserInput = ValidateUserInputDefault;


    /// <summary>
    /// Delegate for ValidatePhoneNumber, which validates that phoneNumber is a valid phone number. When returning, 
    /// compactNumber might have been reformatted.
    /// </summary>
    public delegate bool ValidatePhoneNumberDelegate(string? phoneNumber, out string? compactNumber);


    /// <Summary>
    /// Validates that phoneNumber is a valid phone number. When returning, compactNumber might have been updated.
    /// </summary>
    public static ValidatePhoneNumberDelegate ValidatePhoneNumber = ValidatePhoneNumberDefault;
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Returns true if inputChar can be entered at inputPos into partialPhoneNumber, which is the phone 
    /// number the user has entered so far.
    /// </summary>
    public static bool ValidateUserInputDefault(char inputChar, int inputPos, string partialPhoneNumber) {
      //if user enters a blank, DefaultValidateUserInput does not get called

      if (inputChar>='0' && inputChar<='9') return true;

      if (inputChar=='+') return inputPos==0 && !partialPhoneNumber.Contains('+');

      foreach (var legalChar in LegalSpecialCharacters) {
        if (legalChar==inputChar) return true;
      }

      return false;
    }


    /// <Summary>
    /// Validates that phoneNumber is a valid phone number. On returning, special characters like '+', ' ' and LegalSpecialCharacters
    /// are removed from compactNumber
    /// </summary>
    public static bool ValidatePhoneNumberDefault(string? phoneNumber, out string? compactNumber) {
      if (string.IsNullOrEmpty(phoneNumber)) {
        compactNumber = null;
        return true;
      }

      var isFirstChar = true;
      var compactNumberSB = new StringBuilder();
      foreach (var phoneChar in phoneNumber) {
        if (phoneChar==' ') {
          continue;
        }
        if (phoneChar>='0' && phoneChar<='9'|| isFirstChar && phoneChar=='+') {
          compactNumberSB.Append(phoneChar);
        } else {
          var isLegal = false;
          foreach (var legalChar in LegalSpecialCharacters) {
            if (legalChar==phoneChar) {
              isLegal = true;
              break;
            }
          }
          if (!isLegal) {
            compactNumber = null;
            return false;
          }
        }
        if (isFirstChar) {
          isFirstChar = false;
        }
      }
      compactNumber = compactNumberSB.ToString();
      return true;
    }
    #endregion
  }
}
