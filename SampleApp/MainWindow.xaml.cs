/**************************************************************************************

CheckedControlSample.MainWindow
===============================

Shows how the various checked Controls can be used

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
using WpfWindowsLib;


namespace CheckedControlSample {


  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: CheckedWindow {


    #region Constructor
    //      -----------

    /// <summary>
    /// Default constructor
    /// </summary>
    public MainWindow() {
      InitializeComponent();

      SaveButton.Click += saveButton_Click;

      string?[] nullChangedValues = {null, "Changed"};
      NormalAutoCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      NormalAutoCompleteBox.ItemsSource = nullChangedValues;
      NormalAutoCompleteBox.Init();
      RequiredAutoCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      RequiredAutoCompleteBox.ItemsSource = nullChangedValues;
      RequiredAutoCompleteBox.Init(isRequired: true);
      ChangedCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      ChangedCompleteBox.ItemsSource = nullChangedValues;
      ChangedCompleteBox.Init();
      ChangedCompleteBox.Text = "Changed";

      NormalCheckBox.Init();
      RequiredCheckBox.Init( isRequired: true);
      ChangedCheckBox.Init();
      ChangedCheckBox.IsChecked = true;

      string?[] emptyChangedValues = {"", "Changed"};
      NormalComboBox.ItemsSource = emptyChangedValues;
      NormalComboBox.Init();
      RequiredComboBox.ItemsSource = emptyChangedValues;
      RequiredComboBox.Init(isRequired: true);
      ChangedComboBox.ItemsSource = emptyChangedValues;
      ChangedComboBox.Init();
      ChangedComboBox.SelectedIndex = 1;

      NormalDatePicker.Init();
      RequiredDatePicker.Init(isRequired: true);
      ChangedDatePicker.Init();
      ChangedDatePicker.SelectedDate = DateTime.Now;

      NormalTextBox.Init();
      RequiredTextBox.Init(isRequired: true);
      ChangedTextBox.Init();
      ChangedTextBox.Text = "Changed";

      NormalDecimalTextBox.Init();
      RequiredDecimalTextBox.Init(isRequired: true);
      ChangedDecimalTextBox.Init();
      ChangedDecimalTextBox.DecimalValue = 123.45m;

      NormalEmailTextBox.Init();
      RequiredEmailTextBox.Init(isRequired: true);
      ChangedEmailTextBox.Init();
      ChangedEmailTextBox.Text = "Changed@nowhere.com";

      NormalIntTextBox.Init();
      RequiredIntTextBox.Init(isRequired: true);
      ChangedIntTextBox.Init();
      ChangedIntTextBox.IntValue = 123;
    }
    #endregion


    #region Events
    //      ------

    protected override void OnICheckChanged(bool hasChanged) {
      updateSaveButtonIsEnabled();
    }


    private void updateSaveButtonIsEnabled() {
      SaveButton.IsEnabled = HasICheckChanged && IsAvailable;
    }


    protected override void OnIsAvailableChanged(bool isAvailable) {
      updateSaveButtonIsEnabled();
    }


    private void saveButton_Click(object sender, RoutedEventArgs e) {
      this.ResetHasChanged();
      updateSaveButtonIsEnabled();
      MessageBox.Show("Data would now be saved.", "Save data", MessageBoxButton.OK, MessageBoxImage.Information);
      //signal that data has changed. user needs to change anything before saving is possible again
    }
    #endregion
  }
}