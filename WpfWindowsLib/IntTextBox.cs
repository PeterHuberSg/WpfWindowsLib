/**************************************************************************************

WpfWindowsLib.IntTextBox
========================

TextBox accepting only integer values between Min and Max and implementing ICheck

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.Windows;
using System.Windows.Input;


namespace WpfWindowsLib {


  /// <summary>
  /// A TextBox accepting only integer values, which have to be between Min and Max. If it is placed in a Window 
  /// inheriting from CheckedWindow, it reports automatically any value change to that parent Window.
  /// </summary>
  public class IntTextBox: CheckedTextBox {

    #region Properties
    //      ----------


    /// <summary>
    /// The control's value. Returns null if Text is not a int, i.e. empty.
    /// </summary>
    public int? IntValue {
      get { return intValue; }
      set {
        intValue = value;
        Text = value?.ToString()??"";
      }
    }
    private int? intValue;


    #region Min property
    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
      "Min",
      typeof(int),
      typeof(IntTextBox),
      new FrameworkPropertyMetadata(int.MinValue,
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onMinChanged)
      )
    );


    private static void onMinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var intTextBox = (IntTextBox)d;
      if (intTextBox.isInitialising) return;

      //when Min is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and Min are assigned, if both are used in XAML
      if (intTextBox.Min>intTextBox.Max) {
        throw new Exception($"Error IntTextBox: Min {intTextBox.Min} must be <= Max {intTextBox.Max}. " +
          "Use Initialise() to change both at the same time.");
      }
      if (intTextBox.IntValue!=null && intTextBox.IntValue<intTextBox.Min) {
        throw new Exception($"Error IntTextBox: IntValue {intTextBox.IntValue} must be >= {intTextBox.Min} (Min).");
      }
    }


    /// <summary>
    /// IntTextBox accepts only an IntValue greater equal than Min 
    /// </summary>
    public int Min {
      get { return (int)GetValue(MinProperty); }
      set { SetValue(MinProperty, value); }
    }
    #endregion


    #region Max property
    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
      "Max",
      typeof(int),
      typeof(IntTextBox),
      new FrameworkPropertyMetadata(int.MaxValue,
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onMaxChanged)
      )
    );


    private static void onMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var intTextBox = (IntTextBox)d;
      if (intTextBox.isInitialising) return;

      //when Max is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and Max are assigned, if both are used in XAML
      if (intTextBox.Min>intTextBox.Max) {
        throw new Exception($"Error IntTextBox: Min {intTextBox.Min} must be <= Max {intTextBox.Max}. " +
          "Use Initialise() to change both at the same time.");
      }
      if (intTextBox.IntValue!=null && intTextBox.IntValue>intTextBox.Max) {
        throw new Exception($"Error IntTextBox: IntValue {intTextBox.IntValue} must be <= {intTextBox.Max} (Max).");
      }
    }


    /// <summary>
    /// IntTextBox accepts only an IntValue smaller equal than Max 
    /// </summary>
    public int Max {
      get { return (int)GetValue(MaxProperty); }
      set { SetValue(MaxProperty, value); }
    }
    #endregion
    #endregion


    #region Initialisation
    //      --------------

    bool isInitialising = true;


    protected override void OnTextBoxInitialized() {
      //verify the values set in XAML
      if (Min>Max) throw new Exception($"Error IntTextBox: Min {Min} must be <= Max {Max}.");
      if (Text.Length==0) {
        intValue = null;
      } else {
        intValue = int.Parse(Text); //throw exception here if Parse is not possible
        if (intValue<Min) throw new Exception($"Error IntTextBox: IntValue {IntValue} must be >= {Min} (Min).");
        if (intValue>Max) throw new Exception($"Error IntTextBox: IntValue {IntValue} must be <= {Max} (Max).");
      }
      isInitialising = false;
    }


    /// <summary>
    /// Sets initial value from code behind. If isRequired is true, user needs to change the value before saving is possible. 
    /// If min or max are null, Min or Max value gets not changed.
    /// </summary>
    public virtual void Initialise(int? value = null, bool? isRequired = false, int? min = null, int? max = null) {
      int newMin = min??Min;
      int newMax = max??Max;
      if (newMin>newMax) throw new Exception($"Error IntTextBox: Min {newMin} must be <= Max {newMax}.");
      if (value!=null) {
        if (value<newMin) throw new Exception($"Error IntTextBox: IntValue {value} must be >= {newMin} (Min).");
        if (value>newMax) throw new Exception($"Error IntTextBox: IntValue {value} must be <= {newMax} (Max).");
      }

      isInitialising = true;
      Min = newMin;
      Max = newMax;
      intValue = value;
      isInitialising = false;

      base.Initialise(value?.ToString()??"", isRequired);
    }
    #endregion


    #region Overrides
    //      ---------

    protected override void OnPreviewKeyDown(KeyEventArgs e) {
      e.Handled = e.Key == Key.Space;//forbid " " here, because it doesn't show up in OnPreviewTextInput
      base.OnPreviewKeyDown(e);
    }


    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      if (e.Text.Length>0) { //ctrl + key results in Text.Length==0
        if (e.Text.Length!=1) throw new NotSupportedException($"DecimalTextBox supports only ASCII code.");

        var c = e.Text[0];
        if (c=='-') {
          //'-' is only allowed if cursor is at position 0 and no '-' is entered already
          if (CaretIndex!=0 || Text[SelectedText.Length..].Contains('-')) {
            e.Handled = true;
          }

        } else if (!(c>='0' && c<='9')) {
          e.Handled = true;
        }
      }

      foreach (var c in e.Text) {
      }
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length==0) {
        intValue = null;
        return;
      }

      if (!int.TryParse(Text, out int result)) {
        MessageBox.Show($"{Text} is not a valid int.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else if (result<Min) {
        MessageBox.Show($"{Text} must be >= {Min} (Min).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else if (result>Max) {
        MessageBox.Show($"{Text} must be <= {Max} (Max).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else {
        IntValue = result;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
    #endregion
  }
}