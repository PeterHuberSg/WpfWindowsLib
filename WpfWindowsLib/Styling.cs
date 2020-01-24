/**************************************************************************************

WpfWindowsLib.Styling
=====================

Shared Brushes for controls implementing ICheck

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
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;


namespace WpfWindowsLib {


  public static class Styling {

    #region Forms
    //      -----

    public static readonly Brush RequiredBrush = Brushes.LightGoldenrodYellow;
    public static readonly Brush ErrorBrush = Brushes.PapayaWhip;

    public static readonly Brush HasChangedBackgroundBrush = Brushes.LightGreen;
    #endregion
  }
}
