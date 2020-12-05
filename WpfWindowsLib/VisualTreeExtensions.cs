/**************************************************************************************

WpfWindowsLib.VisualTreeExtensions
==================================

Extension method FindVisualParentOfType<T>

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System.Windows;
using System.Windows.Media;

namespace WpfWindowsLib {


  public static class VisualTreeExtensions {


    /// <summary>
    /// Travels up the visual tree in search of the first parent with the type T.
    /// </summary>
    public static T? FindVisualParentOfType<T>(this DependencyObject dependencyObject) where T : DependencyObject {
      do {
        dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
        if (dependencyObject is T parent) return parent;

      } while (dependencyObject is not null);

      return null;
    }
  }
}
