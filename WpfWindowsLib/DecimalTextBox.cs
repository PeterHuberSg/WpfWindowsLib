/**************************************************************************************

WpfWindowsLib.DecimalTextBox
============================

TextBox accepting only decimal values and implementing ICheck

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
  /// A TextBox accepting only decimal values. If it is placed in a Window inheriting from CheckedWindow, 
  /// it reports automatically any value change to that parent Window.
  /// </summary>
  public class DecimalTextBox: CheckedTextBox {

    #region Properties
    //      ----------

    /// <summary>
    /// The control's value. Returns null if Text is not a decimal.
    /// </summary>
    public decimal? DecimalValue {
      get { return decimalValue; }
      set {
        decimalValue = value;
        Text = value?.ToString("N")??"";
      }
    }
    private decimal? decimalValue;


    #region Min property
    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
      "Min",
      typeof(decimal),
      typeof(DecimalTextBox),
      new FrameworkPropertyMetadata(decimal.MinValue,
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onMinChanged)
      )
    );


    private static void onMinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var textBox = (DecimalTextBox)d;
      if (textBox.isInitialising) return;

      //when Min is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and Min are assigned, if both are used in XAML
      if (textBox.Min>textBox.Max) {
        throw new Exception($"Error DecimalTextBox: Min {textBox.Min} must be <= Max {textBox.Max}. " +
          "Use Initialise() to change both at the same time.");
      }
      if (textBox.DecimalValue!=null && textBox.DecimalValue<textBox.Min) {
        throw new Exception($"Error DecimalTextBox: DecimalValue {textBox.DecimalValue} must be >= {textBox.Min} (Min).");
      }
    }


    /// <summary>
    /// Only a DecimalValue greater equal Min is accepted 
    /// </summary>
    public decimal Min {
      get { return (decimal)GetValue(MinProperty); }
      set { SetValue(MinProperty, value); }
    }
    #endregion


    #region Max property
    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
      "Max",
      typeof(decimal),
      typeof(DecimalTextBox),
      new FrameworkPropertyMetadata(decimal.MaxValue,
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onMaxChanged)
      )
    );


    private static void onMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var textBox = (DecimalTextBox)d;
      if (textBox.isInitialising) return;

      //when Max is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and Max are assigned, if both are used in XAML
      if (textBox.Min>textBox.Max) {
        throw new Exception($"Error DecimalTextBox: Min {textBox.Min} must be <= Max {textBox.Max}. " +
          "Use Initialise() to change both at the same time.");
      }
      if (textBox.DecimalValue!=null && textBox.DecimalValue>textBox.Max) {
        throw new Exception($"Error DecimalTextBox: DecimalValue {textBox.DecimalValue} must be <= {textBox.Max} (Max).");
      }
    }


    /// <summary>
    /// Only a DecimalValue smaller equal than Max is accepted 
    /// </summary>
    public decimal Max {
      get { return (decimal)GetValue(MaxProperty); }
      set { SetValue(MaxProperty, value); }
    }
    #endregion

    #endregion


    #region Initialisation
    //      --------------

    bool isInitialising = true;


    protected override void OnTextBoxInitialised() {
      if (Min>Max) throw new Exception($"Error DecimalTextBox: Min {Min} must be <= Max {Max}.");
      if (Text.Length==0) {
        decimalValue = null;
      } else {
        decimalValue = decimal.Parse(Text); //throw exception here if Parse is not possible
        if (decimalValue<Min) throw new Exception($"Error DecimalTextBox: DecimalValue {DecimalValue} must be >= {Min} (Min).");
        if (decimalValue>Max) throw new Exception($"Error DecimalTextBox: DecimalValue {DecimalValue} must be <= {Max} (Max).");
      }
      isInitialising = false;
    }


    /// <summary>
    /// Sets initial value from code behind. If isRequired is true, user needs to change the value before saving is possible. 
    /// If min or max are null, Min or Max value gets not changed.
    /// </summary>
    public virtual void Initialise(decimal? value = null, bool? isRequired = false, decimal? min = null, decimal? max = null) {
      decimal newMin = min.HasValue? min.Value : Min;
      decimal newMax = max.HasValue? max.Value : Max;
      if (newMin>newMax) throw new Exception($"Error DecimalTextBox: Min {newMin} must be <= Max {newMax}.");
      if (value!=null) {
        if (value<newMin) throw new Exception($"Error DecimalTextBox: DecimalValue {value} must be >= {newMin} (Min).");
        if (value>newMax) throw new Exception($"Error DecimalTextBox: DecimalValue {value} must be <= {newMax} (Max).");
      }

      isInitialising = true;
      Min = newMin;
      Max = newMax;
      decimalValue = value;
      isInitialising = false;

      base.Initialise(value?.ToString("N")??"", isRequired);
    }
    #endregion


    #region Overrides
    //      ---------

    protected override void OnPreviewKeyDown(KeyEventArgs e) {
      e.Handled = e.Key == Key.Space;//forbid " " here, because it doesn't show up in OnPreviewTextInput
      base.OnPreviewKeyDown(e);
    }


    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      var isMinusFound = false;
      var isDecimalFound = false;
      foreach (var c in e.Text) {
        if (c=='-') {
          if (isMinusFound || SelectionStart!=0 || Text[SelectedText.Length..].Contains('-')) {
            e.Handled = true;
            break;
          }
          isMinusFound = true;

        } else if (c=='.') {
          if (!isDecimalFound) {
            if (SelectedText.Length==0) {
              isDecimalFound = Text.Contains('.');
            } else {
              if (SelectionStart==0) {
                isDecimalFound = Text[SelectedText.Length..].Contains('.');
              } else {
                isDecimalFound = Text[..(SelectionStart)].Contains('.');
                if (!isDecimalFound) {
                  isDecimalFound = Text[(SelectionStart + SelectedText.Length)..].Contains('.');
                }
              }
            }
          }
          if (isDecimalFound) {
            e.Handled = true;
            break;
          }
          isDecimalFound = true;
        } else if(!(c>='0' && c<='9')) {
          e.Handled = true;
          break;
        }
      }
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length==0) {
        decimalValue = null;
        return;
      }

      if (!decimal.TryParse(Text, out decimal result)) {
        MessageBox.Show($"{Text} is not a valid decimal.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else if(result<Min) {
        MessageBox.Show($"{Text} must be >= {Min} (Min).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else if (result>Max) {
        MessageBox.Show($"{Text} must be <= {Max} (Max).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else {
        DecimalValue = result;
      }
      base.OnPreviewLostKeyboardFocus(e);
    }
    #endregion
  }
}