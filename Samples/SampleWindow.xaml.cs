using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfWindowsLib;

namespace Samples {
  /// <summary>
  /// Interaction logic for AaaaWindow.xaml
  /// </summary>
  public partial class SampleWindow: CheckedWindow {
    public SampleWindow() {
      InitializeComponent();

      //write some code here to display data coming from a database, etc.
      SaveButton.Click += saveButton_Click;
      updateSaveButtonIsEnabled();
    }

    private void saveButton_Click(object sender, RoutedEventArgs e) {
      //write some code here to save the data the user has entered
      this.ResetHasChanged();
      updateSaveButtonIsEnabled();
    }

    private void updateSaveButtonIsEnabled() {
      if (SaveButton!=null) {
        SaveButton.IsEnabled = HasICheckChanged && IsAvailable;
      }
    }
  }
}
