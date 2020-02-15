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


namespace WpfWindowsLib {


  /// <summary>
  /// If this CheckBox is placed in a Window inherited from CheckedWindow, it reports automatically 
  /// any value change to that parent Window.
  /// </summary>
  public class CheckedCheckBox: CheckBox {

    #region Properties
    //      ----------

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
      //guarantees that the control's values and IsRequired are assigned, if both are used in XAML
      checkedCheckBox.IChecker.IsRequiredChanged(checkedCheckBox.IsRequired, isAvailable: checkedCheckBox.IsChecked.HasValue);
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
    /// Provides the ICheck functionality to CheckedCheckBox
    /// </summary>
    public IChecker<bool?> IChecker { get; }
    #endregion


    #region Constructor
    //      -----------

    /// <summary>
    /// Default constructor
    /// </summary>
    public CheckedCheckBox() {
      IChecker = new IChecker<bool?>(this);
    }
    #endregion


    #region Initialisation
    //      --------------

    bool isInitialising = true;


    protected override void OnInitialized(EventArgs e) {
      IChecker.OnInitialized(initValue: IsChecked, IsRequired, isAvailble: IsChecked.HasValue);
      isInitialising = false;
      //add event handlers only once XAML values are processed, i.e in OnInitialized. 
      Indeterminate += checkedCheckBox_Checked;
      Checked += checkedCheckBox_Checked;
      Unchecked += checkedCheckBox_Checked;
      base.OnInitialized(e);
    }


    /// <summary>
    /// Sets IsChecked and IsRequired from code behind. If isRequired is null, IsRequired keeps its value.
    /// </summary>
    public void Initialise(bool? isChecked = null, bool? isRequired = null) {
      isInitialising = true;
      IsChecked = isChecked;
      IsRequired = isRequired??IsRequired;
      IChecker.Initialise(initValue: isChecked, IsRequired, isAvailble: IsChecked.HasValue);
      isInitialising = false;
    }
    #endregion


    #region Event Handlers
    //      --------------

    private void checkedCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e) {
      if (isInitialising) return;

      IChecker.ValueChanged(IsChecked, IsChecked.HasValue);
    }
    #endregion
  }
}
