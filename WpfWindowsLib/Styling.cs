/**************************************************************************************

WpfWindowsLib.Styling
=====================

Shared Brushes for controls in WpfWindowsLib

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


  /// <summary>
  /// Shared Brushes for controls in WpfWindowsLib
  /// </summary>
  public static class Styling {

    #region Forms
    //      -----

    /// <summary>
    /// Background color for a required control whose initial value the user has not changed yet.
    /// </summary>
    //public static readonly Brush RequiredBrush = Brushes.LightGoldenrodYellow;
    public static Brush RequiredBrush = Brushes.Khaki;

    /// <summary>
    /// Background color for a control whose initial value the user has changed. Used to show
    /// the user why he should not just close the form, but save it first.
    /// </summary>
    public static Brush HasChangedBackgroundBrush = Brushes.LightGreen;

    /// <summary>
    /// Background color for a control where the user made a mistake
    /// </summary>
    public static Brush ErrorBrush = Brushes.PapayaWhip;

    /// <summary>
    /// Background color for a panel control at the top or bottom of the form, containing controls
    /// like Labels, TextBoxes and Buttons.
    /// </summary>
    public static Brush PanelBackgroundBrush = Brushes.Gainsboro;
    #endregion
  }
}
