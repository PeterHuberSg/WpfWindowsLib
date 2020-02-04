/**************************************************************************************

Samples.CheckedTextBoxWindow
============================

Sample Window for CheckedTextBox testing

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


  /// <summary>
  /// Interaction logic for CheckedCheckBoxWindow.xaml
  /// </summary>
  public partial class CheckedCheckBoxWindow: CheckedWindow {

    #region Constructor
    //      -----------

    public CheckedCheckBoxWindow() {
      InitializeComponent();

      InitialiseButton.Click += initialiseButton_Click;
      SaveButton.Click += saveButton_Click;
      updateSaveButtonIsEnabled();
    }
    #endregion


    #region Events
    //      ------

    private void initialiseButton_Click(object sender, RoutedEventArgs e) {
      try {
        var isChange = true;
        bool? newSelected;
        switch (InitialiseSelectedComboBox.SelectedIndex) {
        case 0: isChange = false; newSelected = null; break;
        case 1: newSelected = null; break;
        case 2: newSelected = false; break;
        case 3: newSelected = true; break;
        default:
          throw new NotSupportedException();
        }
        bool? isRequired;
        switch (InitialiseIsRequriedComboBox.SelectedIndex) {
        case 0: isRequired = null; break;
        case 1: isRequired = false; break;
        case 2: isRequired = true; break;
        default:
          throw new NotSupportedException();
        }
        TestCheckedTextBox.Initialise(isChange, newSelected, isRequired);
      } catch (Exception ex) {
        MessageBox.Show(ex.Message, "Exception occurred", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }


    private void saveButton_Click(object sender, RoutedEventArgs e) {
      this.ResetHasChanged();
      updateSaveButtonIsEnabled();
      MessageBox.Show("Data would now be saved.", "Save data", MessageBoxButton.OK, MessageBoxImage.Information);
      //Signal that data has been saved. User needs to change something before saving is possible again
    }


    private void updateSaveButtonIsEnabled() {
      if (SaveButton!=null) {
        SaveButton.IsEnabled = HasICheckChanged && IsAvailable;
      }
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