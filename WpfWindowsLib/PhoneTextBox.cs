/**************************************************************************************

WpfWindowsLib.PhoneTextBox
==========================

TextBox accepting only phone numbers and implementing ICheck. Probably not useful for you,
but you can make your own control with the proper phone formatting. 

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
using System.Windows;
using System.Windows.Input;


namespace WpfWindowsLib {


  /// <summary>
  /// A TextBox accepting only phone number values. If it is placed in a Window inherited from CheckedWindow, 
  /// it reports automatically any value change.
  /// </summary>
  public class PhoneTextBox: CheckedTextBox {

    #region Properties
    //      ----------

    /// <summary>
    /// The user might use blanks when entering the number, which are removed in TelefonNrWithoutBlanks
    /// </summary>
    public string? PhoneNrWithoutBlanks { get; private set; }
    #endregion


    #region Initialisation
    //      --------------

    /// <summary>
    /// Called from Windows constructor to set the initial value and to indicate
    /// if the user is required to enter a value
    /// </summary>
    public override void Init(string? text, bool isRequired = false) {
      base.Init(ToFormatedPhoneString(text?.Replace(" ", "")), isRequired);
    }


    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      var handled = false;
      foreach (var c in e.Text) {
        handled |= !(c>='0' && c<='9');//" " is also allowed, but OnPreviewTextInput doesn't report it
      }
      e.Handled= handled;
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length>0) {
        if (Text[0]!='0') {
          MessageBox.Show($"The first digit should be a 0 '{Text}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          e.Handled = true;
        } else {
          PhoneNrWithoutBlanks = Text.Replace(" ", "");
          if (PhoneNrWithoutBlanks.Length!=10) {
            MessageBox.Show($"The phone number '{Text}' should have 10 digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
          } else {
            format(PhoneNrWithoutBlanks);
          }
        }
      }
      base.OnPreviewLostKeyboardFocus(e);
    }


    /// <summary>
    /// Formats the phone number entered by the user
    /// </summary>
    public void Format() {
      format(Text.Replace(" ", ""));
    }


    private void format(string s) {
      if (s.Length==0) return;

      Text = ToFormatedPhoneString(s);
    }


    /// <summary>
    /// Returns the string containing a phone number with properly placed strings
    /// </summary>
    public static string ToFormatedPhoneString(string? s) {
      if (string.IsNullOrEmpty(s)) return "";

      return string.Concat(s[0], s[1], s[2], ' ', s[3], s[4], s[5], ' ', s[6], s[7], ' ', s[8], s[9]);
    }
    #endregion
  }
}
