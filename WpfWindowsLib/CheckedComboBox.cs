using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


    public virtual void Init(bool isRequired = false, int notSelectedIndex = -1) {
      initSelectedIndex = SelectedIndex;
      this.notSelectedIndex = notSelectedIndex;
      IsRequired = isRequired;
      this.SelectionChanged += checkedComboBox_SelectionChanged;
      if (isRequired) {
        IsAvailable = SelectedIndex!=notSelectedIndex;
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

      Loaded += CheckedComboBox_Loaded;
    }


    Brush? defaultBackground;
    Border? comboBoxBorder;


    private void CheckedComboBox_Loaded(object sender, RoutedEventArgs e) {
      //contentPresenter = (ContentPresenter)Template.FindName("contentPresenter", this);
      //textBlock = (TextBlock)VisualTreeHelper.GetChild(contentPresenter, 0);
      //defaultBackground = textBlock.Background;
      var toggleButton = (ToggleButton)Template.FindName("toggleButton", this);
      comboBoxBorder = (Border)VisualTreeHelper.GetChild(toggleButton, 0);
      defaultBackground = comboBoxBorder.Background;
      showAvailability();
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
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void showAvailability() {
      if (!IsRequired) return;

      if (IsAvailable) {
        comboBoxBorder!.Background = defaultBackground;
      } else {
        comboBoxBorder!.Background = Styling.RequiredBrush;
      }
    }


    public void ShowChanged(bool isShowChanged) {
      //the combobox contains a ToggleButton which contains a Border which paints the Combobox' background
      if (HasChanged) {
        if (isShowChanged) {
          comboBoxBorder!.Background = Styling.HasChangedBackgroundBrush;
        } else {
          comboBoxBorder!.Background = defaultBackground;
        }
      }
    }
  }
}