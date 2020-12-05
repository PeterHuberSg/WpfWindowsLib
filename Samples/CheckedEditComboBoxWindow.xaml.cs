/**************************************************************************************

Samples.CheckedEditComboBoxWindow
=================================

Sample Window for CheckedEditComboBox testing

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


namespace Samples {


  /// <summary>
  /// Interaction logic for CheckedEditComboBoxWindow.xaml
  /// </summary>
  public partial class CheckedEditComboBoxWindow: CheckedWindow {

    #region Constructor
    //      -----------

    public CheckedEditComboBoxWindow() {
      InitializeComponent();

      TestCheckedEditComboBox.Loaded += testCheckedEditComboBox_Loaded;
      TestCheckedEditComboBox.SelectionChanged += testCheckedEditComboBox_SelectionChanged;
      InitialiseButton.Click += initialiseButton_Click;
      SaveButton.Click += saveButton_Click;
      updateSaveButtonIsEnabled();
    }
    #endregion


    #region Events
    //      ------

    private void testCheckedEditComboBox_Loaded(object sender, RoutedEventArgs e) {
      SelectedIndexTextBox.Text = TestCheckedEditComboBox.SelectedIndex.ToString();
    }


    private void testCheckedEditComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
      SelectedIndexTextBox.Text = TestCheckedEditComboBox.SelectedIndex.ToString();
    }


    private void initialiseButton_Click(object sender, RoutedEventArgs e) {
      try {
        string? newText;
        int? newSelected;
        if (InitialiseTextTextBox.Text.Length>0) {
          newText = InitialiseTextTextBox.Text;
          newSelected = null;
        } else {
          newText = null;
          switch (InitialiseSelectedComboBox.SelectedIndex) {
          case 0: newSelected = null; break;
          case 1: newSelected = 1; break;
          default:
            throw new NotSupportedException();
          }
        }

        bool? isRequired;
        switch (InitialiseIsRequriedComboBox.SelectedIndex) {
        case 0: isRequired = null; break;
        case 1: isRequired = false; break;
        case 2: isRequired = true; break;
        default:
          throw new NotSupportedException();
        }
        TestCheckedEditComboBox.Initialise(newText, newSelected, isRequired);
      } catch (Exception ex) {
        MessageBox.Show(ex.Message, "Exception occurred", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }


    private void saveButton_Click(object sender, RoutedEventArgs e) {
      ResetHasChanged();
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