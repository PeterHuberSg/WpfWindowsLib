/**************************************************************************************

WpfWindowsLib.EmailTextBox
==========================

TextBox accepting only email addresses and implementing ICheck

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
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace WpfWindowsLib {


  /// <summary>
  /// An TextBox accepting only email addresses. If it is placed in a Window inherited from CheckedWindow, 
  /// it reports automatically any value change.
  /// </summary>
  public class EmailTextBox: CheckedTextBox {


    static readonly EmailAddressAttribute emailValidator = new EmailAddressAttribute();

    /// <summary>
    /// Validates if text is a valid email address. Uses a regex expression from Microsoft too complicated for
    /// me to understand:
    /// const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
    /// const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
    /// </summary>
    public static bool IsValidEmail(string text) {
      return emailValidator.IsValid(text);
    }


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
          handled |= !((c>='a' && c<='z') || (c>='A' && c<='Z') || (c>='0' && c<='9') || c=='.' || c=='-');
        }
      }
      e.Handled= handled;
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (!Verify() && Text.Length>0) {
        e.Handled = true;
        MessageBox.Show($"Invalid Email-address: '{Text}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      base.OnPreviewLostKeyboardFocus(e);
    }


    /// <summary>
    /// can be used to verify that email address is valid. It is also called internally in OnPreviewLostKeyboardFocus 
    /// </summary>
    /// <returns></returns>
    public bool Verify() {
      if (!IsRequired && Text.Length==0) {
        Background = DefaultBackground;
        return true;
      }
      if (IsValidEmail(Text)) {
        Background = DefaultBackground;
        return true;
      } else {
        Background = Styling.ErrorBrush;
        return false;
      }
    }
  }
}
