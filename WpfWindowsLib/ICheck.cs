using System;
using System.Collections.Generic;
using System.Text;

namespace WpfWindowsLib {


  public interface ICheck {

    bool HasChanged { get; }
    bool IsRequired { get; }
    bool IsAvailable { get; }

    event Action  HasChangedEvent;
    event Action  IsAvailableEvent;

    void ResetHasChanged();
    void ShowChanged(bool isChanged);
  }
}
