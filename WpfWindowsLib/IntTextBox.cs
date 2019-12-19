using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfWindowsLib {


  public class IntTextBox: CheckedTextBox {


    public int? Wert {
      get { return wert; }
      set {
        wert = value;
        Text = value.ToString();
      }
    }
    private int? wert;


    public virtual void Init(int? wert, bool isRequired = false) {
      Wert = wert;
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
        Wert = null;
        return;
      }

      if (int.TryParse(Text, out int result)) {
        Wert = result;
      } else {
        MessageBox.Show($"{Text} ist keine gültige Zahl.", "Ungültige Zahl", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
  }
}
