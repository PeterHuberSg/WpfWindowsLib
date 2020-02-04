/**************************************************************************************

WpfWindowsLib.CheckedAutoCompleteBox
====================================

TextBox with auto suggestions based on user input. Implements ICheck

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
  /// A control that provides a text box for user input and a drop-down that contains 
  /// possible matches based on the input in the text box. If placed in a Window inherited
  /// from CheckedWindow, the control reports automatically any value change.
  /// </summary>
  public class CheckedAutoCompleteBox: AutoCompleteBox, ICheck {

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

    object? initSelectedItem = null;
    Brush? defaultBackground;


    /// <summary>
    /// Called from Windows constructor to set the SelectedItem as not changed value and to indicate
    /// if the user is required to enter a value
    /// </summary>
    public virtual void Init(bool isRequired = false) {
      initSelectedItem = SelectedItem;
      IsRequired = isRequired;
      defaultBackground = Background;
      this.SelectionChanged += checkedAutoCompleteBox_SelectionChanged;
      if (isRequired) {
        IsAvailable = SelectedItem!=null;
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
    /// Called from CheckedWindow after a save. Sets the SelectedItem as not changed value
    /// </summary>
    public void ResetHasChanged() {
      initSelectedItem = SelectedItem;
      HasChanged = false;
    }


    private void checkedAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
      var newHasChanged = initSelectedItem!=SelectedItem;
      if (HasChanged != newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = SelectedItem!=null;
        if (IsAvailable!=newIsAvailable) {
          IsAvailable = newIsAvailable;
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void showAvailability() {
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