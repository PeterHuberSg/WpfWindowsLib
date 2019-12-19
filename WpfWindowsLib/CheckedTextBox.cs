using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfWindowsLib {


  public class CheckedTextBox: TextBox, ICheck {

    public bool HasChanged { get; private set; }
    public event Action?  HasChangedEvent;
    public bool IsRequired { get; private set; }
    public bool IsAvailable { get; private set; }
    public event Action?  IsAvailableEvent;


    string? initText;


    public virtual void Init(string text, bool isRequired = false) {
      if (text==null) text = "";

      initText = text;
      Text = text;
      IsRequired = isRequired;
      TextChanged += checkedTextBox_TextChanged;
      if (isRequired) {
        IsAvailable = Text.Length>0;
        setBackground(IsAvailable);
      }

      FrameworkElement element = this;
      do {
        element = (FrameworkElement)element.Parent;
        if (element==null) break;
        if (element is CheckedWindow window) {
          window.Register(this);
          break;
        }
      } while (true);
    }


    public void ResetHasChanged() {
      initText = Text;
      HasChanged = false;
    }


    private void checkedTextBox_TextChanged(object sender, TextChangedEventArgs e) {
      var newHasChanged = initText!=Text;
      if (HasChanged != newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = Text.Length>0;
        if (IsAvailable!=newIsAvailable) {
          IsAvailable = newIsAvailable;
          setBackground(newIsAvailable);
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void setBackground(bool isAvailable) {
      if (IsAvailable) {
        Background = Brushes.White;
      } else {
        Background = Styling.RequiredBrush;
      }
    }


    Brush? oldBackgroundBrush;


    public void ShowChanged(bool isChanged) {
      if (HasChanged) {
        if (isChanged) {
          oldBackgroundBrush = Background;
          Background = Styling.HasChangedBackgroundBrush;
        } else {
          Background = oldBackgroundBrush;
        }
      }
    }
  }
}
