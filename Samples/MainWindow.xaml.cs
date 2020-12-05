/**************************************************************************************

Samples.MainWindow
==================

Start Window showing all Controls with ICheck functionality

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
using WpfWindowsLib;


namespace Samples {

  #region Constructor
  //      -----------

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: CheckedWindow {

    public MainWindow() {
      InitializeComponent();

      CheckedCheckBoxButton.Click += checkedCheckBoxButton_Click;
      ChangedCheckBox.IsChecked = true;

      CheckedComboBoxButton.Click += checkedComboBoxButton_Click;
      CheckedEditComboBoxButton.Click += checkedEditComboBoxButton_Click;
      string[] comboBoxValues = {"", "Changed"};
      EmptyComboBox.ItemsSource = comboBoxValues;
      EmptyComboBox.Initialise(selectedIndex: null, isRequired: false);
      RequiredComboBox.ItemsSource = comboBoxValues;
      RequiredComboBox.Initialise(selectedIndex: null, isRequired: true);
      ChangedComboBox.ItemsSource = comboBoxValues;
      ChangedComboBox.Initialise(selectedIndex: null, isRequired: false);
      ChangedComboBox.SelectedIndex = 1;

      EmptyEditComboBox.ItemsSource = comboBoxValues;
      EmptyEditComboBox.Initialise(text: null, selectedIndex: null, isRequired: false);
      RequiredEditComboBox.ItemsSource = comboBoxValues;
      RequiredEditComboBox.Initialise(text: null, selectedIndex: null, isRequired: true);
      ChangedEditComboBox.ItemsSource = comboBoxValues;
      ChangedEditComboBox.Initialise(text: null, selectedIndex: null, isRequired: false);
      ChangedEditComboBox.SelectedIndex = 1;

      CheckedDatePickerButton.Click += checkedDatePickerButton_Click;
      ChangedDatePicker.SelectedDate = DateTime.Now.Date;

      CheckedTextBoxButton.Click += checkedTextBoxButton_Click;
      ChangedTextBox.Text = "Changed";

      DecimalTextBoxButton.Click += decimalTextBoxButton_Click;
      ChangedDecimalTextBox.Text = "1.23";

      IntTextBoxButton.Click += intTextBoxButton_Click;
      ChangedIntTextBox.Text = "123";

      EmailTextBoxButton.Click += emailTextBoxButton_Click;
      ChangedEmailTextBox.Text = "abc@de.ef";

      PhoneTextBoxButton.Click += phoneTextBoxButton_Click;
      ChangedPhoneTextBox.Text= "123456789";

      SaveButton.Click += saveButton_Click;
      updateSaveButtonIsEnabled();
    }
    #endregion


    #region Event Handlers
    //      --------------

    private void autoCompleteBoxButton_Click(object sender, RoutedEventArgs e) {
      throw new System.NotImplementedException();
    }


    private void checkedCheckBoxButton_Click(object sender, RoutedEventArgs e) {
      new CheckedCheckBoxWindow { Owner=this }.Show();
    }


    private void checkedComboBoxButton_Click(object sender, RoutedEventArgs e) {
      new CheckedComboBoxWindow { Owner=this }.Show();
    }


    private void checkedEditComboBoxButton_Click(object sender, RoutedEventArgs e) {
      new CheckedEditComboBoxWindow { Owner=this }.Show();
    }


    private void checkedDatePickerButton_Click(object sender, RoutedEventArgs e) {
      new CheckedDatePickerWindow { Owner=this }.Show();
    }


    private void checkedTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new CheckedTextBoxWindow { Owner=this }.Show();
    }


    private void decimalTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new DecimalTextBoxWindow { Owner=this }.Show();
    }


    private void intTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new IntTextBoxWindow { Owner=this }.Show();
    }


    private void emailTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new EmailTextBoxWindow { Owner=this }.Show();
    }


    private void phoneTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new PhoneTextBoxWindow { Owner=this }.Show();
    }


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
