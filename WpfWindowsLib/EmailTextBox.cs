using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace WpfWindowsLib {


  public class EmailTextBox: CheckedTextBox {

    static readonly EmailAddressAttribute emailValidator = new EmailAddressAttribute();

    public static bool IsValidEmail(string text) {
      return emailValidator.IsValid(text);
    }


    public bool IstNoetig;


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
        MessageBox.Show($"Invalid Emailaddress: '{Text}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      base.OnPreviewLostKeyboardFocus(e);
    }


    public bool Verify() {
      if (!IstNoetig && Text.Length==0) {
        Background = Brushes.White;
        return true;
      }
      if (IsValidEmail(Text)) {
        Background = Brushes.White;
        return true;
      } else {
        Background = Brushes.LightGoldenrodYellow;
        return false;
      }

    }
  }
}
