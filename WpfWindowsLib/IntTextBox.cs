/**************************************************************************************

WpfWindowsLib.IntTextBox
========================

TextBox accepting only integer values and implementing ICheck

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


  public class IntTextBox: CheckedTextBox {


    public int? IntValue {
      get { return intValue; }
      set {
        intValue = value;
        Text = value.ToString();
      }
    }
    private int? intValue;


    public virtual void Init(int? wert = null, bool isRequired = false) {
      IntValue = wert;
      Init(Text, isRequired);
    }


    protected override void OnPreviewKeyDown(KeyEventArgs e) {
      e.Handled = e.Key == Key.Space;//forbid " " here, because it doesn't show up in OnPreviewTextInput
      base.OnPreviewKeyDown(e);
    }


    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      var handled = false;
      foreach (var c in e.Text) {
        handled |= !(c>='0' && c<='9');
      }
      e.Handled= handled;
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length==0) {
        IntValue = null;
        return;
      }

      if (int.TryParse(Text, out int result)) {
        IntValue = result;
      } else {
        MessageBox.Show($"{Text} is not a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
  }
}
