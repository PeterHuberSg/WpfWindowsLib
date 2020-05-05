/**************************************************************************************

WpfWindowsLib.ICheck
====================

Interface implemented by controls signaling when their content has changed from the
initiated value. This information is needed to enable or disable the save button.

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.Windows.Controls;

namespace WpfWindowsLib {


  public interface ICheck {

    /// <summary>
    /// Has the user changed the initial value of the control ?
    /// </summary>
    bool HasChanged { get; }

    /// <summary>
    /// Needs the user to change the initial value of the control ?
    /// </summary>
    bool IsRequired { get; }

    /// <summary>
    /// Has the user changed the initial value of the required control ?
    /// </summary>
    bool IsAvailable { get; }

    /// <summary>
    /// Control implementing ICheck
    /// </summary>
    Control Control { get; }

    /// <summary>
    /// Raised when the user changes the initial value of the control or when the user undoes any change and
    /// enters the initial value again.
    /// </summary>
    event Action  HasChangedEvent;

    /// <summary>
    /// Raised when the user changes the initial value of the required value control or when the user undoes 
    /// any change and enters the initial value again.
    /// </summary>
    event Action  IsAvailableEvent;

    /// <summary>
    /// Tells the control to use the present value as initial value.
    /// </summary>
    void ResetHasChanged();

    /// <summary>
    /// Changes the background color of the control if its value is now different than the initial value 
    /// and isChanged is true. If isChanged is false, the background color gets displayed from when the
    /// control got initialised.
    /// </summary>
    /// <param name="isChanged"></param>
    void ShowChanged(bool isChanged);
  }
}
