using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace WpfWindowsLib {


  public class CheckedDatePicker: DatePicker, ICheck {

    public bool HasChanged { get; private set; }
    public event Action?  HasChangedEvent;
    public bool IsRequired { get; private set; }
    public bool IsAvailable { get; private set; }
    public event Action?  IsAvailableEvent;


    DateTime? initDate;
    Brush? defaultBackground;



    public virtual void Init(DateTime? date = null, bool isRequired = false) {
      initDate = date;
      SelectedDate = date;
      IsRequired = isRequired;
      defaultBackground = Background;
      SelectedDateChanged += CheckedDatePicker_SelectedDateChanged;
      if (isRequired) {
        IsAvailable = Text.Length>0;
        showAvailability();
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
      initDate = SelectedDate;
      HasChanged = false;
    }


    private void CheckedDatePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs e) {
      var newHasChanged = initDate!=SelectedDate;
      if (HasChanged!=newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = SelectedDate != null;
        if (IsAvailable!=newIsAvailable) {
          IsAvailable = newIsAvailable;
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void showAvailability() {
      if (!IsRequired) return;

      if (IsAvailable) {
        Background = defaultBackground;
      } else {
        Background = Styling.RequiredBrush;
      }
    }


    public void ShowChanged(bool isChanged) {
      if (HasChanged) {
        if (isChanged) {
          Background = Styling.HasChangedBackgroundBrush;
        } else {
          Background = defaultBackground;
        }
      }
    }
  }
}
