/**************************************************************************************

TestVisibility.MainWindow
=========================

Tests controls when their Visibility is first collapsed and then visible

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


namespace TestVisibility {


  #region Constructor
  //      -----------

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: CheckedWindow {

    public MainWindow() {
      InitializeComponent();

      string[] comboBoxValues = {"", "Changed"};
      EmptyComboBox.ItemsSource = comboBoxValues;
      EmptyComboBox.Initialise(selectedIndex: null, isRequired: false);
      RequiredComboBox.ItemsSource = comboBoxValues;
      RequiredComboBox.Initialise(selectedIndex: null, isRequired: true);
      ChangedComboBox.ItemsSource = comboBoxValues;
      ChangedComboBox.Initialise(selectedIndex: null, isRequired: false);
      ChangedComboBox.SelectedIndex = 1;

      MakeVisibleButton.Click += makeVisibleButton_Click;
      SaveButton.Click += saveButton_Click;
      updateSaveButtonIsEnabled();
    }
    #endregion


    #region Event Handlers
    //      --------------

    private void autoCompleteBoxButton_Click(object sender, RoutedEventArgs e) {
      throw new System.NotImplementedException();
    }


    private void makeVisibleButton_Click(object sender, RoutedEventArgs e) {
      foreach (var iCheck in IChecks) {
        iCheck.Control.Visibility = Visibility.Visible;
      }
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
