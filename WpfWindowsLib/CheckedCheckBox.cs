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


    #region IsRequired property
    public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
      "IsRequired",
      typeof(bool),
      typeof(CheckedCheckBox),
      new FrameworkPropertyMetadata(false,
          FrameworkPropertyMetadataOptions.AffectsRender,
          new PropertyChangedCallback(onIsRequiredChanged)
      )
    );


    private static void onIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var checkedCheckBox = (CheckedCheckBox)d;
      if (checkedCheckBox.isInitialising) return;

      //when IsRequired is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that IsChecked and IsRequired are assigned, if both are used in XAML
      var oldIsAvailable = checkedCheckBox.IsAvailable;
      if (checkedCheckBox.IsRequired) {
        checkedCheckBox.IsAvailable = checkedCheckBox.IsChecked.HasValue;
      } else {
        checkedCheckBox.IsAvailable = true;
      }

      if (oldIsAvailable!=checkedCheckBox.IsAvailable) {
        checkedCheckBox.showAvailability();
        checkedCheckBox.IsAvailableEvent?.Invoke();
      }
    }


    /// <summary>
    /// Needs the user to provide this control with a value ? 
    /// </summary>
    public bool IsRequired {
      get { return (bool)GetValue(IsRequiredProperty); }
      set { SetValue(IsRequiredProperty, value); }
    }
    #endregion


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
    bool isInitialising = true;


    protected override void OnInitialized(EventArgs e) {
      initValue = IsChecked;

      Indeterminate += checkedCheckBox_Checked;
      Checked += checkedCheckBox_Checked;
      Unchecked += checkedCheckBox_Checked;
      if (IsRequired) {
        IsAvailable = IsChecked.HasValue;
        if (!IsAvailable) {
          //a changed background needs only to be displayed if control is required, but not available
          showAvailability();
        }
      } else {
        //if no user input is required, the control is always available
        IsAvailable = true;
      }
      isInitialising = false;

      CheckedWindow.Register(this); //updates CheckedWindow.IsAvailable, no need to raise IsAvailableEvent

      base.OnInitialized(e);
    }


    /// <summary>
    /// Sets IsChecked and IsRequired from code behind. If isChangeIsChecked is false, IsChecked keeps its value. If 
    /// isRequired is null, IsRequired keeps its value.
    /// </summary>
    public void Initialise(bool isChangeIsChecked = false, bool? isChecked = null, bool? isRequired = null) {
      isInitialising = true;
      if (isChangeIsChecked) {
        initValue = isChecked;
        IsChecked = isChecked;
      }
      IsRequired = isRequired??IsRequired; 
      isInitialising = false;

      var newHasChanged = initValue!=IsChecked;
      if (HasChanged!=newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      var newIsAvailable = !IsRequired||IsChecked.HasValue;
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
      initValue = IsChecked;
      HasChanged = false;
    }
    #endregion


    #region Event Handlers
    //      --------------

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
    #endregion


    #region Methods
    //      -------

    private void showAvailability() {
      if (IsAvailable) {
        ClearValue(TextBox.BackgroundProperty);
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
          ClearValue(TextBox.BackgroundProperty);
        }
      }
    }
    #endregion
  }
}
