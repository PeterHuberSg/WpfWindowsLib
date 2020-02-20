/**************************************************************************************

Samples.PhoneTextBoxWindow
==========================

Sample Window for PhoneTextBox testing

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
  /// Interaction logic for PhoneTextBoxWindow.xaml
  /// </summary>
  public partial class PhoneTextBoxWindow: CheckedWindow {

    //https://github.com/google/libphonenumber

    #region Constructor
    //      -----------

    /// <summary>
    /// Default constructor
    /// </summary>
    public PhoneTextBoxWindow() {
      InitializeComponent();

      InitialiseButton.Click += initialiseButton_Click;
      LocalFormatComboBox.SelectionChanged += localFormatComboBox_SelectionChanged;
      SaveButton.Click += saveButton_Click;
      updateSaveButtonIsEnabled();
    }
    #endregion


    #region Events
    //      ------

    private void initialiseButton_Click(object sender, RoutedEventArgs e) {
      try {
        bool? isRequired = InitialiseIsRequriedComboBox.SelectedIndex switch
        {
          0 => null,
          1 => false,
          2 => true,
          _ => throw new NotSupportedException(),
        };
        TestPhoneTextBox.Initialise(
          InitialisePhoneValueTextBox.Text,
          isRequired,
          InitialiseMinDigitsTextBox.Text.Length==0 ? (int?)null : int.Parse(InitialiseMinDigitsTextBox.Text),
          InitialiseMaxDigitsTextBox.Text.Length==0 ? (int?)null : int.Parse(InitialiseMaxDigitsTextBox.Text));
      } catch (Exception ex) {
        MessageBox.Show(ex.Message, "Exception occurred", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }


    private void localFormatComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
      switch (LocalFormatComboBox.SelectedIndex) {
      case 0: CountryCode.LocalFormat = null; break;
      case 1: CountryCode.LocalFormat = CountryCode.LocalFormatNo0Area2; CountryCode.MaxLengthLocalCode=9; break;
      case 2: CountryCode.LocalFormat = CountryCode.LocalFormat0Area2; CountryCode.MaxLengthLocalCode=9; break;
      case 3: CountryCode.LocalFormat = CountryCode.LocalFormatNo0Area3; CountryCode.MaxLengthLocalCode=10; break;
      case 4: CountryCode.LocalFormat = CountryCode.LocalFormat0Area3; CountryCode.MaxLengthLocalCode=10; break;
      case 5: CountryCode.LocalFormat = CountryCode.LocalFormat8Digits; CountryCode.MaxLengthLocalCode=8; break;
      default: throw new NotImplementedException();
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
