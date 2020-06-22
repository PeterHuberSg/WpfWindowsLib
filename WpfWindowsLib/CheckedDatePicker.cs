/**************************************************************************************

WpfWindowsLib.DatePicker
========================

DatePicker implementing ICheck

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
  /// If this DatePicker is placed in a Window inherited from CheckedWindow, it reports automatically 
  /// any value change to that parent Window.
  /// </summary>
  public class CheckedDatePicker: DatePicker {

    #region Properties
    //      ----------

    #region IsRequired property
    public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
      "IsRequired",
      typeof(bool),
      typeof(CheckedDatePicker),
      new FrameworkPropertyMetadata(false,
          FrameworkPropertyMetadataOptions.AffectsRender,
          new PropertyChangedCallback(onIsRequiredChanged)
      )
    );


    private static void onIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var checkedDatePicker = (CheckedDatePicker)d;
      if (checkedDatePicker.isInitialising) return;

      //when IsRequired is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that the control's values and IsRequired are assigned, if both are used in XAML
      checkedDatePicker.IChecker.IsRequiredChanged(checkedDatePicker.IsRequired, isAvailable:checkedDatePicker.SelectedDate!=null);
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
    /// Has the value of this control changed since it was initialised ?
    /// </summary>
    public bool HasChanged { get { return IChecker.HasChanged; } }


    /// <summary>
    /// Provides the ICheck functionality to CheckedDatePicker
    /// </summary>
    public IChecker<DateTime?> IChecker { get; }
    #endregion


    #region Constructor
    //      -----------

    /// <summary>
    /// Default constructor
    /// </summary>
    public CheckedDatePicker() {
      IChecker = new IChecker<DateTime?>(this);
    }
    #endregion


    #region Initialisation
    //      --------------

    DateTime? initDate;
    bool isInitialising = true;


    protected override void OnInitialized(EventArgs e) {
      initDate = SelectedDate;

      IChecker.OnInitialized(initValue: SelectedDate, IsRequired, isAvailable: SelectedDate!=null);
      isInitialising = false;
      //add event handlers only once XAML values are processed, i.e in OnInitialized. 
      SelectedDateChanged += checkedDatePicker_SelectedDateChanged;
      base.OnInitialized(e);
    }


    /// <summary>
    /// Sets SelectedDate and IsRequired from code behind.<para/>
    /// If isRequired is null, IsRequired keeps its value.
    /// </summary>
    public virtual void Initialise(DateTime? date = null, bool? isRequired = null) {
      isInitialising = false;
      initDate = date;
      SelectedDate = date;
      if (isRequired.HasValue) {
        IsRequired = isRequired.Value;
      }
      IChecker.Initialise(initValue: date, IsRequired, isAvailable: date!=null);
      isInitialising = true;
    }
    #endregion


    #region Event Handlers
    //      --------------

    private void checkedDatePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs e) {
      if (isInitialising) return;

      IChecker.ValueChanged(SelectedDate, isAvailable: SelectedDate!=null) ;
    }
    #endregion
  }
}
