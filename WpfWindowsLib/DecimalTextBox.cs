using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfWindowsLib {
  public class DecimalTextBox: CheckedTextBox {


    public decimal? Wert {
      get { return wert; }
      set {
        wert = value;
        Text = value?.ToString("N")??"";
      }
    }
    private decimal? wert;


    public virtual void Init(decimal? wert, bool isRequired = false) {
      Wert = wert;
      Init(Text, isRequired);
    }


    protected override void OnPreviewKeyDown(KeyEventArgs e) {
      e.Handled = e.Key == Key.Space;//forbid " " here, because it doesn't show up in OnPreviewTextInput
      base.OnPreviewKeyDown(e);
    }
    //private void DecimalTextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
    //  var hasPunkt = Text.Contains('.');
    //  e.Handled = 
    //    !(e.Key >= Key.D0 && e.Key <= Key.D9 || 
    //    e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
    //    (!hasPunkt && (e.Key == Key.Decimal ||e.Key == Key.OemPeriod)) ||
    //    e.Key == Key.Back || e.Key == Key.Delete ||
    //    e.Key == Key.Left || e.Key == Key.Right ||
    //    e.Key == Key.Tab);
    //}


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
        Wert = null;
        return;
      }

      if (decimal.TryParse(Text, out decimal result)) {
        Wert = result;
      } else {
        MessageBox.Show($"{Text} ist keine gültige Zahl.", "Ungültige Zahl", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
  }
}
