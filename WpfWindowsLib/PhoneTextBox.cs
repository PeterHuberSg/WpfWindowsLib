/**************************************************************************************

WpfWindowsLib.PhoneTextBox
==========================

TextBox accepting only phone numbers and implementing ICheck

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


  public class PhoneTextBox: CheckedTextBox {

    public static string ToFormatedPhoneString(string? s) {
      if (string.IsNullOrEmpty(s)) return "";

      return string.Concat(s[0], s[1], s[2], ' ', s[3], s[4], s[5], ' ', s[6], s[7], ' ', s[8], s[9]);
    }


    public override void Init(string? text, bool isRequired = false) {
      base.Init(ToFormatedPhoneString(text?.Replace(" ", "")), isRequired);
    }


    public string? TelefonNrWithoutBlanks;


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
          TelefonNrWithoutBlanks = Text.Replace(" ", "");
          if (TelefonNrWithoutBlanks.Length!=10) {
            MessageBox.Show($"The phone number '{Text}' should have 10 digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
          } else {
            format(TelefonNrWithoutBlanks);
          }
        }
      }
      base.OnPreviewLostKeyboardFocus(e);
    }


    public void Format() {
      format(Text.Replace(" ", ""));
    }


    private void format(string s) {
      if (s.Length==0) return;

      Text = ToFormatedPhoneString(s);
    }
  }
}

