/**************************************************************************************

WpfWindowsLib.IChecker
======================

IChecker implements ICheck. A Control having an IChecker needs very little code to support ICheck.

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
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfWindowsLib {

  /// <summary>
  /// Implements ICheck. A Control having an IChecker needs very little code to support ICheck.
  /// </summary>
  /// <typeparam name="TValue">A control let's the user input a specific type. TextBox: string, DatePicker: DateTime, ...</typeparam>
  public class IChecker<TValue>: ICheck {

    //https://devblogs.microsoft.com/dotnet/try-out-nullable-reference-types/
    //https://docs.microsoft.com/en-us/dotnet/csharp/nullable-attributes

    /**************************************************************************************
    The control owning IChecker interacts with XAML and the user. IChecker implements the
    functionality of ICheck.

    If the control's value or IsRequired changes, the control calls IChecker to update
    HasChanged and IsAvailable.
    
    Control.onIsRequiredChanged() => if (!isInitialising) IChecker.IsRequiredChanged()
    Control.OnInitialized() => IChecker.OnInitialized()
    Control.Initialise() => IChecker.Initialise()
    Control value changed event => if (!isInitialising) IChecker.ValueChanged()

    IChecker changes the Background of the ownerControl to display the state of HasChanged
    and IsAvailable. This might not work for some control. For that case, the control
    can assign its own activities to     OnChangeBackground, OnClearBackground, 
    OnSetBackground and OnResetBackground
    **************************************************************************************/


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
    /// Needs the user to change the initial value of the control ?
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
    /// Value set in XAML, by Initialise() or ResetHasChanged()
    /// </summary>
    [MaybeNull]
    public TValue InitValue { get; private set; }


    /// <summary>
    /// Presently displayed value in ownerControl
    /// </summary>
    [MaybeNull]
    public TValue ActualValue { get; private set; }


    /// <summary>
    /// Assign Brush to control's Background. Should be changed when a different control than ownerControl needs to be used.
    /// </summary>
    public Action<Brush> OnChangeBackground;


    /// <summary>
    /// Call control.ClearValue(Control.BackgroundProperty). Should be overwritten when a different control than ownerControl needs 
    /// to be used. 
    /// </summary>
    public Action OnClearBackground;


    /// <summary>
    /// Remember control's Background (=oldBackground) and assign the Brush to it. Should be changed when a different control 
    /// than ownerControl needs to be used.
    /// </summary>
    public Action<Brush> OnSetBackground;


    /// <summary>
    /// Assign oldBackground to control's Background. Should be changed when a different controlthan ownerControl needs to be used.
    /// </summary>
    public Action OnResetBackground;
    #endregion


    #region Constructor
    //      -----------

    readonly Control ownerControl;


    public IChecker(Control control) {
      ownerControl = control;
      InitValue = default!;
      ActualValue = default!;

      OnChangeBackground = DefaultOnChangeBackground;
      OnClearBackground = DefaultOnClearBackground;
      OnSetBackground = DefaultOnSetBackground;
      OnResetBackground = DefaultOnResetBackground;
    }
    #endregion


    #region Initialisation
    //      --------------

    public void OnInitialized(TValue initValue, bool isRequired, bool isAvailable) {
      InitValue = initValue;
      ActualValue = initValue;
      IsRequired = isRequired;

      //HasChanged is already and stays false here

      if (IsRequired) {
        IsAvailable = isAvailable;
        if (!IsAvailable) {
          //a changed background needs only to be displayed here if control is required, but not available
          showAvailability();
        }
      } else {
        //if no user input is required, the control is always available
        IsAvailable = true;
      }

      CheckedWindow.Register(this, ownerControl); //updates CheckedWindow.IsAvailable, no need to raise IsAvailableEvent    
    }


    /// <summary>
    /// Sets IsChecked and IsRequired from code behind. If isChangeIsChecked is false, IsChecked keeps its value. If 
    /// isRequired is null, IsRequired keeps its value.
    /// </summary>
    public void Initialise(TValue initValue, bool isRequired, bool isAvailable) {
      InitValue = initValue;
      ActualValue = initValue;
      IsRequired = isRequired;

      if (HasChanged) {
        HasChanged = false;
        HasChangedEvent?.Invoke();
      }

      var newIsAvailable = !IsRequired||isAvailable;
      if (IsAvailable!=newIsAvailable) {
        IsAvailable = newIsAvailable;
        showAvailability();
        IsAvailableEvent?.Invoke();
      }
    }


    /// <summary>
    /// Called from CheckedWindow after a save, sets the present value as initial value
    /// </summary>
    public void ResetHasChanged() {
      InitValue = ActualValue;
      HasChanged = false;
    }
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Called be ownerControl when IsRequired has changed.
    /// </summary>
    public void IsRequiredChanged(bool isRequired, bool isAvailable) {
      //if (IsRequired==isRequired) return;

      IsRequired = isRequired;
      var oldIsAvailable = IsAvailable;
      if (IsRequired) {
        IsAvailable = isAvailable;
      } else {
        IsAvailable = true;
      }
      if (oldIsAvailable!=IsAvailable) {
        showAvailability();
        IsAvailableEvent?.Invoke();
      }
    }


    /// <summary>
    /// Called be ownerControl when the value of the control has changed
    /// </summary>
    public void ValueChanged(TValue value, bool isAvailable) {
      if (value?.Equals(ActualValue)??((ActualValue is null))) throw new Exception();

      ActualValue = value;
      var hasChanged = !InitValue?.Equals(ActualValue)??(!(ActualValue is null));
      if (HasChanged != hasChanged) {
        HasChanged = hasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        if (IsAvailable!=isAvailable) {
          IsAvailable = isAvailable;
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void showAvailability() {
      if (IsAvailable) {
        OnClearBackground();
      } else {
        OnChangeBackground(Styling.RequiredBrush);
      }
    }


    /// <summary>
    /// Change the background color of this control if the user has changed its value
    /// </summary>
    public void ShowChanged(bool isChanged) {
      if (HasChanged) {
        if (isChanged) {
          OnSetBackground(Styling.HasChangedBackgroundBrush);
        } else {
          OnResetBackground();
        }
      }
    }


    /// <summary>
    /// Default version for OnChangeBackground.
    /// </summary>
    public void DefaultOnChangeBackground(Brush backgroundBrush) {
      ownerControl.Background = backgroundBrush!;
    }


    /// <summary>
    /// Default version for OnClearBackground.
    /// </summary>
    public void DefaultOnClearBackground() {
      ownerControl.ClearValue(Control.BackgroundProperty);
    }


    Brush? oldBackground;


    /// <summary>
    /// Default version for OnSetBackground.
    /// </summary>
    public void DefaultOnSetBackground(Brush backgroundBrush) {
      oldBackground = ownerControl.Background;
      ownerControl.Background = backgroundBrush!;
    }


    /// <summary>
    /// Default version for OnResetBackground.
    /// </summary>
    public void DefaultOnResetBackground() {
      ownerControl.Background = oldBackground!;
    }
    #endregion
  }
}
