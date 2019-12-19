using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfWindowsLib {


  public class CheckedComboBox: ComboBox, ICheck {

    public bool HasChanged { get; private set; }
    public event Action?  HasChangedEvent;
    public bool IsRequired { get; private set; }
    public bool IsAvailable { get; private set; }
    public event Action?  IsAvailableEvent;


    int initSelectedIndex = int.MinValue;
    int notSelectedIndex;


    public virtual void Init(bool isRequired = false, int notSelectedIndex = int.MinValue) {
      initSelectedIndex = SelectedIndex;
      this.notSelectedIndex = notSelectedIndex;
      IsRequired = isRequired;
      this.SelectionChanged += checkedComboBox_SelectionChanged;
      if (isRequired) {
        IsAvailable = SelectedIndex!=notSelectedIndex;
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
      initSelectedIndex = SelectedIndex;
      HasChanged = false;
    }


    private void checkedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
      var newHasChanged = initSelectedIndex!=SelectedIndex;
      if (HasChanged != newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = SelectedIndex!=notSelectedIndex;
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