/**************************************************************************************

WpfWindowsLib.CheckedTextBox
============================

TextBox implementing ICheck

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace WpfWindowsLib {


  /// <summary>
  /// If this TextBox is placed in a Window inherited from CheckedWindow, it reports automatically 
  /// any value change.
  /// </summary>
  public class CheckedTextBox: TextBox, ICheck {

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


    /// <summary>
    /// Background of control after initialisation. Useful for inheriting class which needs to change
    /// the background to highlight the control and then wants to change back
    /// </summary>
    protected Brush? DefaultBackground { get; private set;}

    #endregion


    #region Initialisation
    //      --------------

    string? initText;

    /// <summary>
    /// Called from Windows constructor to set the initial value and to indicate
    /// if the user is required to enter a value
    /// </summary>
    public virtual void Init(string? text="", bool isRequired = false) {
      if (text==null) text = "";

      initText = text;
      Text = text;
      IsRequired = isRequired;
      DefaultBackground = Background;
      TextChanged += checkedTextBox_TextChanged;
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
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Called from CheckedWindow after a save, sets Text as initial value
    /// </summary>
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
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void showAvailability() {
      if (!IsRequired) return;

      if (IsAvailable) {
        Background = DefaultBackground;
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
          Background = DefaultBackground;
        }
      }
    }
    #endregion
  }
}