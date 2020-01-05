using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace WpfWindowsLib {


  public class CheckedWindow: Window {
    public IEnumerable<ICheck> IChecks { get { return iChecks; } }
    readonly List<ICheck> iChecks;
    public bool HasICheckChanged { get; protected set; }
    public virtual void OnICheckChanged(bool hasChanged) { }


    public IEnumerable<ICheck> Requireds { get { return requireds; } }
    readonly List<ICheck> requireds;
    public bool IsAvailable { get; protected set; }
    public virtual void OnIsAvailableChanged(bool isAvailable) { }



    public CheckedWindow() {
      iChecks = new List<ICheck>();
      requireds = new List<ICheck>();
      IsAvailable = true;
      Closing += checkedWindow_Closing;
      Closed += checkedWindow_Closed;
    }


    private void checkedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      if (HasICheckChanged) {
        MessageBoxResult result;
        ShowChanged(true);
        result = MessageBox.Show("Press OK to close the Window without saving the data, Cancel to continue with data entry.",
        "Closing the Window ? Data has changed.", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
        ShowChanged(false);
        if (result==MessageBoxResult.OK) return;

        e.Cancel = true;
      }
    }


    private void checkedWindow_Closed(object? sender, EventArgs e) {
      Owner?.Activate();
    }


    public void Register(ICheck iCheck) {
      if (iCheck==null) return;

      iChecks.Add(iCheck);
      iCheck.HasChangedEvent += iCheck_HasChangedEvent;
      if (iCheck.IsRequired) {
        requireds.Add(iCheck);
        iCheck.IsAvailableEvent += iCheck_IsAvailableEvent;
        IsAvailable = IsAvailable && iCheck.IsAvailable;
      }
    }


    public void ResetHasChanged() {
      foreach (var iCheck in iChecks) {
        iCheck.ResetHasChanged();
      }
      HasICheckChanged = false;
    }


    public void ShowChanged(bool isChanged) {
      foreach (var iCheck in iChecks) {
        iCheck.ShowChanged(isChanged);
      }
    }


    private void iCheck_HasChangedEvent() {
      foreach (var iCheck in iChecks) {
        if (iCheck.HasChanged) {
          HasICheckChanged = true;
          OnICheckChanged(true);
          return;
        }
      };
      HasICheckChanged = false;
      OnICheckChanged(false);
    }


    private void iCheck_IsAvailableEvent() {
      foreach (var iCheck in requireds) {
        if (!iCheck.IsAvailable) {
          IsAvailable = false;
          OnIsAvailableChanged(false);
          return;
        }
      };
      IsAvailable = true;
      OnIsAvailableChanged(true);
    }

  }
}
