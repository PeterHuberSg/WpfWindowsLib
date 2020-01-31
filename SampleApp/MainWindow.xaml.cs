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
      //NormalAutoCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      //NormalAutoCompleteBox.ItemsSource = nullChangedValues;
      //NormalAutoCompleteBox.Init();
      //RequiredAutoCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      //RequiredAutoCompleteBox.ItemsSource = nullChangedValues;
      //RequiredAutoCompleteBox.Init(isRequired: true);
      //ChangedCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      //ChangedCompleteBox.ItemsSource = nullChangedValues;
      //ChangedCompleteBox.Init();
      //ChangedCompleteBox.Text = "Changed";

      //NormalCheckBox.Init();
      //RequiredCheckBox.Init( isRequired: true);
      //ChangedCheckBox.Init();
      //ChangedCheckBox.IsChecked = true;

      //string?[] emptyChangedValues = {"", "Changed"};
      //NormalComboBox.ItemsSource = emptyChangedValues;
      //NormalComboBox.Init();
      //RequiredComboBox.ItemsSource = emptyChangedValues;
      //RequiredComboBox.Init(isRequired: true);
      //ChangedComboBox.ItemsSource = emptyChangedValues;
      //ChangedComboBox.Init();
      //ChangedComboBox.SelectedIndex = 1;

      //NormalDatePicker.Init();
      //RequiredDatePicker.Init(isRequired: true);
      //ChangedDatePicker.Init();
      //ChangedDatePicker.SelectedDate = DateTime.Now;

      //NormalTextBox.Init();
      //RequiredTextBox.Init(isRequired: true);
      //ChangedTextBox.Init();
      //ChangedTextBox.Text = "Changed";

      ////NormalDecimalTextBox.Initialise();
      ////RequiredDecimalTextBox.Initialise(isRequired: true);
      ////ChangedDecimalTextBox.Initialise();
      //ChangedDecimalTextBox.DecimalValue = 123.45m;

      //NormalEmailTextBox.Initialise();
      //RequiredEmailTextBox.Initialise(isRequired: true);
      //ChangedEmailTextBox.Initialise();
      //ChangedEmailTextBox.Text = "Changed@nowhere.com";

      //NormalIntTextBox.Initialise();
      //RequiredIntTextBox.Initialise(isRequired: true);
      //ChangedIntTextBox.Initialise();
      //ChangedIntTextBox.IntValue = 123;

      updateSaveButtonIsEnabled();

      EmptyNullNotRButton.Click += emptyNullNotRButton_Click;
      XamlNullNotRButton.Click += xamlNullNotRButton_Click;
      RequiredNullNotRButton.Click += requiredNullNotRButton_Click;
      XamlReqNullNotRButton.Click += xamlReqNullNotRButton_Click;

      EmptyValueNotRButton.Click += emptyValueNotRButton_Click;
      XamlValueNotRButton.Click += xamlValueNotRButton_Click;
      RequiredValueNotRButton.Click += requiredValueNotRButton_Click;
      XamlReqValueNotRButton.Click += xamlReqValueNotRButton_Click;

      EmptyNullReqButton.Click += emptyNullReqButton_Click;
      XamlNullReqButton.Click += xamlNullReqButton_Click;
      RequiredNullReqButton.Click += requiredNullReqButton_Click;
      XamlReqNullReqButton.Click += xamlReqNullReqButton_Click;

      EmptyValueReqButton.Click += emptyValueReqButton_Click;
      XamlValueReqButton.Click += xamlValueReqButton_Click;
      RequiredValueReqButton.Click += requiredValueReqButton_Click;
      XamlReqValueReqButton.Click += xamlReqValueReqButton_Click;
    }


    private void emptyNullNotRButton_Click(object sender, RoutedEventArgs e) {
      EmptyTextBox.Initialise();
      EmptyDecimalTextBox.Initialise();
    }


    private void xamlNullNotRButton_Click(object sender, RoutedEventArgs e) {
      XamlTextBox.Initialise();
      XamlDecimalTextBox.Initialise();
    }


    private void requiredNullNotRButton_Click(object sender, RoutedEventArgs e) {
      RequiredTextBox.Initialise();
      RequiredDecimalTextBox.Initialise();
    }


    private void xamlReqNullNotRButton_Click(object sender, RoutedEventArgs e) {
      XamlRequTextBox.Initialise();
      XamlRequDecimalTextBox.Initialise();
    }


    private void emptyValueNotRButton_Click(object sender, RoutedEventArgs e) {
      EmptyTextBox.Initialise("value1");
      EmptyDecimalTextBox.Initialise(1.11m);
    }


    private void xamlValueNotRButton_Click(object sender, RoutedEventArgs e) {
      XamlTextBox.Initialise("value1");
      XamlDecimalTextBox.Initialise(1.11m);
    }


    private void requiredValueNotRButton_Click(object sender, RoutedEventArgs e) {
      RequiredTextBox.Initialise("value1");
      RequiredDecimalTextBox.Initialise(1.11m);
    }


    private void xamlReqValueNotRButton_Click(object sender, RoutedEventArgs e) {
      XamlRequTextBox.Initialise("value1");
      XamlRequDecimalTextBox.Initialise(1.11m);
    }


    private void emptyNullReqButton_Click(object sender, RoutedEventArgs e) {
      EmptyTextBox.Initialise(null, isRequired: true);
      EmptyDecimalTextBox.Initialise(null, isRequired: true);
    }


    private void xamlNullReqButton_Click(object sender, RoutedEventArgs e) {
      XamlTextBox.Initialise(null, isRequired: true);
      XamlDecimalTextBox.Initialise(null, isRequired: true);
    }


    private void requiredNullReqButton_Click(object sender, RoutedEventArgs e) {
      RequiredTextBox.Initialise(null, isRequired: true);
      RequiredDecimalTextBox.Initialise(null, isRequired: true);
    }


    private void xamlReqNullReqButton_Click(object sender, RoutedEventArgs e) {
      XamlRequTextBox.Initialise(null, isRequired: true);
      XamlRequDecimalTextBox.Initialise(null, isRequired: true);
    }


    private void emptyValueReqButton_Click(object sender, RoutedEventArgs e) {
      EmptyTextBox.Initialise("value2", isRequired: true);
      EmptyDecimalTextBox.Initialise(2.22m, isRequired: true);
    }


    private void xamlValueReqButton_Click(object sender, RoutedEventArgs e) {
      XamlTextBox.Initialise("value2", isRequired: true);
      XamlDecimalTextBox.Initialise(2.22m, isRequired: true);
    }


    private void requiredValueReqButton_Click(object sender, RoutedEventArgs e) {
      RequiredTextBox.Initialise("value2", isRequired: true);
      RequiredDecimalTextBox.Initialise(2.22m, isRequired: true);
    }


    private void xamlReqValueReqButton_Click(object sender, RoutedEventArgs e) {
      XamlRequTextBox.Initialise("value2", isRequired: true);
      XamlRequDecimalTextBox.Initialise(2.22m, isRequired: true);
    }
    #endregion


    #region Events
    //      ------

    private void saveButton_Click(object sender, RoutedEventArgs e) {
      this.ResetHasChanged();
      updateSaveButtonIsEnabled();
      MessageBox.Show("Data would now be saved.", "Save data", MessageBoxButton.OK, MessageBoxImage.Information);
      //signal that data has changed. user needs to change anything before saving is possible again
    }


    private void updateSaveButtonIsEnabled() {
      SaveButton.IsEnabled = HasICheckChanged && IsAvailable;
    }
    #endregion


    #region Overrides
    //      ---------
    protected override void OnICheckChanged() {
      updateSaveButtonIsEnabled();
    }


    protected override void OnIsAvailableChanged() {
      updateSaveButtonIsEnabled();
    }
    #endregion
  }
}