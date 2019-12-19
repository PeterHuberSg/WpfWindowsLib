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


    public override void Init(string text, bool isRequired = false) {
      base.Init(ToFormatedPhoneString(text?.Replace(" ", "")), isRequired);
    }


    public string? TelefonNrOhneLeerzeichen;


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
          MessageBox.Show($"Die erste Ziffer sollte eine 0 sein in '{Text}'.", "Ungültige Telefonnummer", MessageBoxButton.OK, MessageBoxImage.Error);
          e.Handled = true;
        } else {
          TelefonNrOhneLeerzeichen = Text.Replace(" ", "");
          if (TelefonNrOhneLeerzeichen.Length!=10) {
            MessageBox.Show($"Die Telefonnummer '{Text}' sollte 10 Ziffern haben.", "Ungültige Telefonnummer", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
          } else {
            format(TelefonNrOhneLeerzeichen);
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

