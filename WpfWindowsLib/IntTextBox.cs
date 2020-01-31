/**************************************************************************************

WpfWindowsLib.IntTextBox
========================

TextBox accepting only integer values and implementing ICheck

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
using System.Windows.Input;


namespace WpfWindowsLib {


  /// <summary>
  /// A TextBox accepting only integer values. If it is placed in a Window inherited from CheckedWindow, 
  /// it reports automatically any value change.
  /// </summary>
  public class IntTextBox: CheckedTextBox {

    #region Properties
    //      ----------

    /// <summary>
    /// The control's value
    /// </summary>
    //public int? IntValue {
    //  get { return intValue; }
    //  set {
    //    intValue = value;
    //    Text = value.ToString();
    //  }
    //}
    //private int? intValue;


    public static readonly DependencyProperty IntValueProperty = DependencyProperty.Register(
      "IntValue",
      typeof(int?),
      typeof(IntTextBox),
      new FrameworkPropertyMetadata(null,
          FrameworkPropertyMetadataOptions.AffectsRender,
          new PropertyChangedCallback(OnIntValueChanged)
      )
    );

    private static void OnIntValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var intTextBox = (IntTextBox)d;
      intTextBox.Text = intTextBox.Text.ToString();
    }

    /// <summary>
    /// The control's value
    /// </summary>
    public int? IntValue {
      get { return (int?)GetValue(IntValueProperty); }
      set { SetValue(IntValueProperty, value); }
    }
    #endregion


    #region Initialisation
    //      --------------

    /// <summary>
    /// Called from Windows constructor to set the initial value and to indicate
    /// if the user is required to enter a value
    /// </summary>
    public virtual void Initialise(int? wert = null, bool isRequired = false) {
      IntValue = wert;
      Initialise(Text, isRequired);
    }
    #endregion


    #region Methods
    //      -------

    protected override void OnPreviewKeyDown(KeyEventArgs e) {
      e.Handled = e.Key == Key.Space;//forbid " " here, because it doesn't show up in OnPreviewTextInput
      base.OnPreviewKeyDown(e);
    }


    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      var handled = false; 
      foreach (var c in e.Text) {
        handled |= !(c>='0' && c<='9');
      }
      e.Handled= handled;
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length==0) {
        IntValue = null;
        return;
      }

      if (int.TryParse(Text, out int result)) {
        IntValue = result;
      } else {
        MessageBox.Show($"{Text} is not a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
    #endregion
  }
}