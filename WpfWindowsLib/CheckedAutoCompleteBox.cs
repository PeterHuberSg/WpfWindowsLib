/**************************************************************************************

WpfWindowsLib.CheckedAutoCompleteBox
====================================

TextBox with auto suggestions based on user input. Implements ICheck

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace WpfWindowsLib {


  public class CheckedAutoCompleteBox: AutoCompleteBox, ICheck {

    public bool HasChanged { get; private set; }
    public event Action?  HasChangedEvent;
    public bool IsRequired { get; private set; }
    public bool IsAvailable { get; private set; }
    public event Action?  IsAvailableEvent;


    object? initSelectedItem = null;
    Brush? defaultBackground;


    public virtual void Init(bool isRequired = false) {
      initSelectedItem = SelectedItem;
      IsRequired = isRequired;
      defaultBackground = Background;
      this.SelectionChanged += checkedAutoCompleteBox_SelectionChanged;
      if (isRequired) {
        IsAvailable = SelectedItem!=null;
        showAvailability();
      }

      FrameworkElement element = this;
      do {
        element = (FrameworkElement)element.Parent;
        if (element==null) break;

        if (element is CheckedWindow window) {
          window.Register(this);
          break;
        }
      } while (true);
    }


    public void ResetHasChanged() {
      initSelectedItem = SelectedItem;
      HasChanged = false;
    }


    private void checkedAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
      var newHasChanged = initSelectedItem!=SelectedItem;
      if (HasChanged != newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = SelectedItem!=null;
        if (IsAvailable!=newIsAvailable) {
          IsAvailable = newIsAvailable;
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void showAvailability() {
      if (!IsRequired) return;

      if (IsAvailable) {
        Background = defaultBackground;
      } else {
        Background = Styling.RequiredBrush;
      }
    }


    public void ShowChanged(bool isChanged) {
      if (HasChanged) {
        if (isChanged) {
          Background = Styling.HasChangedBackgroundBrush;
        } else {
          Background = defaultBackground;
        }
      }
    }
  }
}