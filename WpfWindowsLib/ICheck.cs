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
