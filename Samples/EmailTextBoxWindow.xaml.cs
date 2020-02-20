/**************************************************************************************

Samples.EmailTextBoxWindow
==========================

Sample Window for EmailTextBox testing

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
  /// Interaction logic for EmailTextBoxWindow.xaml
  /// </summary>
  public partial class EmailTextBoxWindow: CheckedWindow {

    #region Constructor
    //      -----------

    /// <summary>
    /// Default constructor
    /// </summary>
    public EmailTextBoxWindow() {
      InitializeComponent();

      CharactersComboBox.SelectionChanged += charactersComboBox_SelectionChanged; ;
      InternationalCharsCheckBox.Click += internationalCharsCheckBox_Click; ;
      BlankAllowedCheckBox.Click += blankAllowedCheckBox_Click; ;
      InitialiseButton.Click += initialiseButton_Click;
      SaveButton.Click += saveButton_Click;
      updateSaveButtonIsEnabled();
    }


    private void charactersComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
      switch (CharactersComboBox.SelectedIndex) {
      case 0: EmailTextBox.SetDefaultAsciiSpecialChars(); break;
      case 1: EmailTextBox.SetExtendedAsciiSpecialChars(); break;
      case 2: EmailTextBox.SetExtendedQuotedAsciiSpecialChars(); break;
      default:throw new NotSupportedException();
      }
    }


    private void internationalCharsCheckBox_Click(object sender, RoutedEventArgs e) {
      EmailTextBox.IsInternationalCharSetAllowed = InternationalCharsCheckBox.IsChecked!.Value;
    }


    private void blankAllowedCheckBox_Click(object sender, RoutedEventArgs e) {
      EmailTextBox.IsBlankAllowed = BlankAllowedCheckBox.IsChecked!.Value;
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
        TestEmailTextBox.Initialise(
          InitialiseEmailAdrTextBox.Text,
          isRequired);
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
