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
        MessageBox.Show($"{Text} is not a vlid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
  }
}
