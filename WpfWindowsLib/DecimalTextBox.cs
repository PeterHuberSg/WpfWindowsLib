/**************************************************************************************

WpfWindowsLib.DecimalTextBox
============================

TextBox accepting only decimal values and implementing ICheck

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


  public class DecimalTextBox: CheckedTextBox {


    public decimal? DecimalValue {
      get { return decimalValue; }
      set {
        decimalValue = value;
        Text = value?.ToString("N")??"";
      }
    }
    private decimal? decimalValue;


    public virtual void Init(decimal? wert = null, bool isRequired = false) {
      DecimalValue = wert;
      Init(Text, isRequired);
    }


    protected override void OnPreviewKeyDown(KeyEventArgs e) {
      e.Handled = e.Key == Key.Space;//forbid " " here, because it doesn't show up in OnPreviewTextInput
      base.OnPreviewKeyDown(e);
    }


    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      var handled = false;
      var hasDecimal = Text.Contains('.');
      foreach (var c in e.Text) {
        if (c=='.') {
          handled |= hasDecimal;
          hasDecimal = true;
        } else {
          handled |= !(c>='0' && c<='9');
        }
      }
      e.Handled= handled;
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length==0) {
        DecimalValue = null;
        return;
      }

      if (decimal.TryParse(Text, out decimal result)) {
        DecimalValue = result;
      } else {
        MessageBox.Show($"{Text} is not a valid decimal.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
  }
}
