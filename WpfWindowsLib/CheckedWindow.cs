/**************************************************************************************

WpfWindowsLib.CheckedWindow
===========================

Window functionality checking automatically if any checked control has changed its content

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
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfWindowsLib {


  /// <summary>
  /// Inherit from CheckedWindow for data entry windows. Any control implementing ICheck will automatically
  /// register to the Window it is placed in and report its HasChanged and IsAvailable status to that
  /// Window. This information is needed to decide if the user can save the data (all required data is entered)
  /// or if the user can close the Window (all changed data has been saved).
  /// 
  /// Overwrite OnICheckChanged and OnIsAvailableChanged in your window to enable/disable the Save button. 
  /// checkedWindow_Closing warns user if he wants to close the Window but changed data is not saved yet.
  /// </summary>
  public class CheckedWindow: Window {

    #region Properties
    //      ----------

    //Data changed
    //------------

    /// <summary>
    /// All controls implementing ICheck on this window
    /// </summary>
    public IEnumerable<ICheck> IChecks { get { return iChecks; } }
    readonly HashSet<ICheck> iChecks; //HashSet is used in case a control registers wrongly twice

    /// <summary>
    /// True if any data has been changed, false if no data has been entered yet or all data is saved.
    /// </summary>
    public bool HasICheckChanged { get; protected set; }

    /// <summary>
    /// Gets called if HasICheckChanged has changed.
    /// </summary>
    protected virtual void OnICheckChanged() { }


    //Data required
    //-------------

    /// <summary>
    /// True if controls which require some data before data can be saved have some data.
    /// </summary>
    public bool IsAvailable { get; protected set; }

    /// <summary>
    /// Gets called if IsAvailable has changed.
    /// </summary>
    protected virtual void OnIsAvailableChanged() { }


    //Configuration
    //-------------

    /// <summary>
    /// Gets called to display a warning when user wants to close window but some data has be changed. On 
    /// returning true, window gets closed without saving the data. If returning false, the user can 
    /// continue editing. 
    /// </summary>
    public static Func<CheckedWindow, bool> AskUserIfNoSaving = AskUserIfNoSavingDefault;
    #endregion


    #region Constructor
    //      ----------

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CheckedWindow() {
      iChecks = new HashSet<ICheck>();
      HasICheckChanged = false;
      IsAvailable = true;
      Closing += checkedWindow_Closing;
      Closed += checkedWindow_Closed;
    }
    #endregion


    #region Methods
    //      -------

    private void checkedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      if (HasICheckChanged) {
        ShowChanged(true);
        e.Cancel = !AskUserIfNoSaving(this);
        ShowChanged(false);
      }
    }


    public static bool AskUserIfNoSavingDefault(CheckedWindow window) {
      var result = MessageBox.Show("Press OK to close the Window without saving the data, Cancel to continue with data entry.",
      "Closing the Window ? Data has changed.", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
      return result==MessageBoxResult.OK;
    }

    private void checkedWindow_Closed(object? sender, EventArgs e) {
      Owner?.Activate();
    }


    /// <summary>
    /// Called by a control implementing ICheck to find its parent CheckedWindow and register with it
    /// </summary>
    public static void Register(FrameworkElement element) {
      var startElement = element;
       do {
        if (element.Parent==null) {
          if (DesignerProperties.GetIsInDesignMode(startElement)) {
            break;
          } else {
            throw new Exception($"Cannot find parent CheckedWindow of {startElement.Name}.");
          }
        }
        element = (FrameworkElement)element.Parent;

        if (element is CheckedWindow window) {
          window.Register((ICheck)startElement);
          break;
        }
      } while (true);
    }


    public static void Register(ICheck iCheck, FrameworkElement element) {
      var startElement = element;
      do {
        if (element.Parent==null) {
          if (DesignerProperties.GetIsInDesignMode(startElement)) {
            break;
          } else {
            throw new Exception($"Cannot find parent CheckedWindow of {startElement.Name}.");
          }
        }
        element = (FrameworkElement)element.Parent;

        if (element is CheckedWindow window) {
          window.Register(iCheck);
          break;
        }
      } while (true);
    }


    /// <summary>
    /// A control implementing ICheck will find automatically the host CheckedWindow and register with it.
    /// </summary>
    public void Register(ICheck iCheck) {
      if (iCheck==null) return;

      if (iChecks.Contains(iCheck)) throw new Exception($"{Title} Window: Programming error, cannot register iCheck '{iCheck}' twice.");
      iChecks.Add(iCheck);
      iCheck.HasChangedEvent += iCheck_HasChangedEvent;
      iCheck.IsAvailableEvent += iCheck_IsAvailableEvent;
      if (iCheck.IsRequired && IsAvailable) {
        //requireds.Add(iCheck);
        IsAvailable = iCheck.IsAvailable;
      }
    }


    /// <summary>
    /// The inheriting window calls ResetHasChanged() after saving the data.
    /// </summary>
    public void ResetHasChanged() {
      foreach (var iCheck in iChecks) {
        iCheck.ResetHasChanged();
      }
      HasICheckChanged = false;
    }


    /// <summary>
    /// If isChanged is true, CheckedWindow will change the background color of all controls have
    /// changed data.
    /// </summary>
    public void ShowChanged(bool isChanged) {
      foreach (var iCheck in iChecks) {
        iCheck.ShowChanged(isChanged);
      }
    }


    private void iCheck_HasChangedEvent() {
      foreach (var iCheck in iChecks) {
        if (iCheck.HasChanged) {
          HasICheckChanged = true;
          OnICheckChanged();
          return;
        }
      };
      HasICheckChanged = false;
      OnICheckChanged();
    }


    private void iCheck_IsAvailableEvent() {
      foreach (var iCheck in iChecks) {
        if (!iCheck.IsAvailable) {
          IsAvailable = false;
          OnIsAvailableChanged();
          return;
        }
      };
      IsAvailable = true;
      OnIsAvailableChanged();
    }
    #endregion
  }
}
