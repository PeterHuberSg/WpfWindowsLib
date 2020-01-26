/**************************************************************************************

WpfWindowsLib.CheckedComboBox
=============================

ComboBox implementing ICheck

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace WpfWindowsLib {


  /// <summary>
  /// If this ComboBox is placed in a Window inherited from CheckedWindow, it reports automatically 
  /// any value change.
  /// </summary>
  public class CheckedComboBox: ComboBox, ICheck {

    #region Properties
    //      ----------

    /// <summary>
    /// Has the value of this control changed ?
    /// </summary>
    public bool HasChanged { get; private set; }

    /// <summary>
    /// Raised when control gets changed or the user undoes the change
    /// </summary>
    public event Action?  HasChangedEvent;

    /// <summary>
    /// Needs the user to provide this control with a value ?
    /// </summary>
    public bool IsRequired { get; private set; }

    /// <summary>
    /// Has the user provided a value ?
    /// </summary>
    public bool IsAvailable { get; private set; }

    /// <summary>
    /// The availability of the control has changed
    /// </summary>
    public event Action?  IsAvailableEvent;
    #endregion


    #region Initialisation
    //      --------------


    int initSelectedIndex = int.MinValue;
    int notSelectedIndex;


    /// <summary>
    /// Called from Windows constructor to set the initial value to notSelectedIndex and 
    /// to indicate if the user is required to enter a value
    /// </summary>
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

      Loaded += checkedComboBox_Loaded;
    }
    #endregion


    #region Methods
    //      -------

    Brush? defaultBackground;
    Border? comboBoxBorder;


    private void checkedComboBox_Loaded(object sender, RoutedEventArgs e) {
      //contentPresenter = (ContentPresenter)Template.FindName("contentPresenter", this);
      //textBlock = (TextBlock)VisualTreeHelper.GetChild(contentPresenter, 0);
      //defaultBackground = textBlock.Background;
      var toggleButton = (ToggleButton)Template.FindName("toggleButton", this);
      comboBoxBorder = (Border)VisualTreeHelper.GetChild(toggleButton, 0);
      defaultBackground = comboBoxBorder.Background;
      showAvailability();
    }


    /// <summary>
    /// Called from CheckedWindow after a save. Sets the SelectedIndex as not changed value
    /// </summary>
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


    /// <summary>
    /// Change the background color of this control if the user has changed its value
    /// </summary>
    public void ShowChanged(bool isShowChanged) {
      //the ComboBox contains a ToggleButton which contains a Border which paints the ComboBox' background
      if (HasChanged) {
        if (isShowChanged) {
          comboBoxBorder!.Background = Styling.HasChangedBackgroundBrush;
        } else {
          comboBoxBorder!.Background = defaultBackground;
        }
      }
    }
    #endregion
  }
}