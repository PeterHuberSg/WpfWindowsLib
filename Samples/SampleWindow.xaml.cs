using System.Windows;
using System.Windows.Media;
using WpfWindowsLib;

namespace Samples {
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
      SaveButton.IsEnabled = HasICheckChanged && IsAvailable;
    }

    protected override void OnICheckChanged() {
      updateSaveButtonIsEnabled();
    }

    protected override void OnIsAvailableChanged() {
      updateSaveButtonIsEnabled();
    }
  }
}
