/**************************************************************************************

WpfWindowsLib.EmailTextBox
==========================

TextBox accepting only email addresses and implementing ICheck. 
The user can only key in characters like 'a' .. 'z', '0' .. '9', '.', '-' and '_'. More 
characters would be legal, but some email servers might not accept them. If more characters 
should be allowed, make a new class using the code here and change OnPreviewTextInput().

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
using System.Windows;
using System.Windows.Input;


namespace WpfWindowsLib {


  /// <summary>
  /// An TextBox accepting only email addresses. The user can only key in characters like 
  /// 'a' .. 'z', '0' .. '9', '.', '-' and '_'.If it is placed in a Window inherited from 
  /// CheckedWindow, it reports automatically any value change.
  /// </summary>
  public class EmailTextBox: CheckedTextBox {

    #region Methods
    //      -------

    static readonly EmailAddressAttribute emailValidator = new EmailAddressAttribute();

    /// <summary>
    /// Validates if text is a valid email address. Uses a regex expression from Microsoft too complicated for
    /// me to understand:
    /// pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
    /// it is also not 100% correct, for example it accepts '1@1.', which is probably not a valid email address because
    /// of the trailing '.'.
    /// </summary>
    public static bool IsValidEmail(string text) {
      return emailValidator.IsValid(text);
    }
    #endregion


    #region Initialisation
    //      --------------

    protected override void OnTextBoxInitialised() {
      //verify the values set in XAML
      if (Text.Length>0 && !IsValidEmail(Text)) {
        throw new Exception($"Error EmailTextBox: '{Text}' is not a valid email address).");
      }
    }


    /// <summary>
    /// Sets initial value from code behind. If isRequired is true, user needs to change the value before saving is possible. 
    /// If min or max are null, Min or Max value gets not changed.
    /// </summary>
    public override void Initialise(string? text = null, bool? isRequired = false) {

      if (!string.IsNullOrEmpty(text) && !IsValidEmail(text)) {
        throw new Exception($"Error EmailTextBox.Initialise(): '{text}' is not a valid email address).");
      }
      base.Initialise(text, isRequired);
    }
    #endregion


    #region Overrides
    //      ---------

    protected override void OnPreviewKeyDown(KeyEventArgs e) {
      e.Handled = e.Key == Key.Space;//forbid " " here, because it doesn't show up in OnPreviewTextInput

      base.OnPreviewKeyDown(e);
    }


    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      var handled = false;
      var hasAt = Text.Contains('@');
      foreach (var c in e.Text) {
        if (c=='@') {
          handled |= hasAt;
          hasAt = true;
        } else {
          handled |= !((c>='a' && c<='z') || (c>='A' && c<='Z') || (c>='0' && c<='9') || c=='.' || c=='-' || c=='_');
        }
      }
      e.Handled= handled;
      base.OnPreviewTextInput(e); 
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length>0) {
        if (!IsValidEmail(Text)) {
          e.Handled = true;
          MessageBox.Show($"Invalid Email-address: '{Text}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
    #endregion
  }
}
