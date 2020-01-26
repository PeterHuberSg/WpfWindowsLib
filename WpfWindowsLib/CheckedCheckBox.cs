/**************************************************************************************

WpfWindowsLib.CheckedCheckBox
=============================

CheckBox implementing ICheck

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
using System.Windows.Media;


namespace WpfWindowsLib {


  /// <summary>
  /// If this CheckBox is placed in a Window inherited from CheckedWindow, it reports automatically 
  /// any value change.
  /// </summary>
  public class CheckedCheckBox: CheckBox, ICheck {

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

    bool? initValue;
    Brush? defaultBackground;


    /// <summary>
    /// Called from Windows constructor to set the initial value and to indicate
    /// if the user is required to enter a value
    /// </summary>
    public void Init(bool? checkValue = null, bool isRequired = false) {

      initValue = checkValue;
      IsChecked = checkValue;
      IsRequired = isRequired;
      defaultBackground = Background;
      Checked += checkedCheckBox_Checked;
      Unchecked += checkedCheckBox_Checked;
      if (isRequired) {
        IsAvailable = checkValue.HasValue;
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
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Called from CheckedWindow after a save, sets the present value as initial value
    /// </summary>
    public void ResetHasChanged() {
      initValue = IsChecked;
      HasChanged = false;
    }


    private void checkedCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e) {
      var newHasChanged = initValue!=IsChecked;
      if (HasChanged != newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = IsChecked.HasValue;
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


    /// <summary>
    /// Change the background color of this control if the user has changed its value
    /// </summary>
    public void ShowChanged(bool isChanged) {
      if (HasChanged) {
        if (isChanged) {
          Background = Styling.HasChangedBackgroundBrush;
        } else {
          Background = defaultBackground;
        }
      }
    }
    #endregion
  }
}
