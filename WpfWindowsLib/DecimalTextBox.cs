/**************************************************************************************

WpfWindowsLib.DecimalTextBox
============================

TextBox accepting only decimal values between Min and Max and implementing ICheck

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
  /// A TextBox accepting only decimal values, which have to be between Min and Max. If it is placed in a Window 
  /// inheriting from CheckedWindow, it reports automatically any value change to that parent Window.
  /// </summary>
  public class DecimalTextBox: CheckedTextBox {

    #region Properties
    //      ----------

    /// <summary>
    /// The control's value. Returns null if Text is not a decimal, i.e. empty.
    /// </summary>
    public decimal? DecimalValue {
      get { return decimalValue; }
      set {
        decimalValue = value;
        if (value is null) {
          Text = "";
          return;
        }

        Text = Math.Round(value.Value, Decimals).ToString(DecimalFormat);
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
      var decimalTextBox = (DecimalTextBox)d;
      if (decimalTextBox.isInitialising) return;

      //when Min is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and Min are assigned, if both are used in XAML
      if (decimalTextBox.Min>decimalTextBox.Max) {
        throw new Exception($"Error DecimalTextBox: Min {decimalTextBox.Min} must be <= Max {decimalTextBox.Max}. " +
          "Use Initialise() to change both at the same time.");
      }
      if (decimalTextBox.DecimalValue!=null && decimalTextBox.DecimalValue<decimalTextBox.Min) {
        throw new Exception($"Error DecimalTextBox: DecimalValue {decimalTextBox.DecimalValue} must be >= {decimalTextBox.Min} (Min).");
      }
    }


    /// <summary>
    /// DecimalTextBox accepts only a DecimalValue greater equal than Min 
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
      var decimalTextBox = (DecimalTextBox)d;
      if (decimalTextBox.isInitialising) return;

      //when Max is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and Max are assigned, if both are used in XAML
      if (decimalTextBox.Min>decimalTextBox.Max) {
        throw new Exception($"Error DecimalTextBox: Min {decimalTextBox.Min} must be <= Max {decimalTextBox.Max}. " +
          "Use Initialise() to change both at the same time.");
      }
      if (decimalTextBox.DecimalValue!=null && decimalTextBox.DecimalValue>decimalTextBox.Max) {
        throw new Exception($"Error DecimalTextBox: DecimalValue {decimalTextBox.DecimalValue} must be <= {decimalTextBox.Max} (Max).");
      }
    }


    /// <summary>
    /// DecimalTextBox accepts only a DecimalValue smaller equal than Max 
    /// </summary>
    public decimal Max {
      get { return (decimal)GetValue(MaxProperty); }
      set { SetValue(MaxProperty, value); }
    }
    #endregion


    #region Decimals property

    public static readonly DependencyProperty DecimalsProperty = DependencyProperty.Register(
      "Decimals",
      typeof(int),
      typeof(DecimalTextBox),
      new FrameworkPropertyMetadata(2,
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onDecimalsChanged)
      )
    );


    const int maxDecimals = 29;


    private static void onDecimalsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var decimalTextBox = (DecimalTextBox)d;
      if (decimalTextBox.Decimals<0 || decimalTextBox.Decimals>maxDecimals) {
        throw new Exception($"Error DecimalTextBox: Decimals {decimalTextBox.Decimals} must be between 0 and {maxDecimals}. ");
      }
    }


    /// <summary>
    /// DecimalTextBox rounds Text to number of fractional digits after the decimal point according to Decimals 
    /// </summary>
    public int Decimals {
      get { return (int)GetValue(DecimalsProperty); }
      set { SetValue(DecimalsProperty, value); }
    }
    #endregion



    #region DecimalFormat property

    public static readonly DependencyProperty DecimalFormatProperty = DependencyProperty.Register(
      "DecimalFormat",
      typeof(string),
      typeof(CheckedTextBox),
      new FrameworkPropertyMetadata("",
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onDecimalFormatChanged) 
      )
    );


    private static void onDecimalFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var decimalTextBox = (DecimalTextBox)d;
      try {
        123.45m.ToString(decimalTextBox.DecimalFormat);
      } catch (Exception) {

        throw new Exception($"Illegal DecimalFormat '{decimalTextBox.DecimalFormat}'.");
      }
    }


    /// <summary>
    /// Needs the user to provide this control with a value ? 
    /// </summary>
    public string DecimalFormat {
      get { return (string)GetValue(DecimalFormatProperty); }
      set { SetValue(DecimalFormatProperty, value); }
    }
    #endregion

    /// <summary>
    /// Gets called to display an error message when user has keyed in an invalid decimal, but wants to 
    /// move the keyboard focus from this DecimalTextBox. 
    /// </summary>
    public static Action<DecimalTextBox> ShowNotValidError = ShowNotValidErrorDefault;

    /// <summary>
    /// Gets called to display an error message when user has keyed in an decimal smaller than Min, but wants 
    /// to move the keyboard focus from this DecimalTextBox. 
    /// </summary>
    public static Action<DecimalTextBox> ShowSmallerMinError = ShowSmallerMinErrorDefault;

    /// <summary>
    /// Gets called to display an error message when user has keyed in an decimal bigger than Max, but wants 
    /// to move the keyboard focus from this DecimalTextBox. 
    /// </summary>
    public static Action<DecimalTextBox> ShowBiggerMaxError = ShowBiggerMaxErrorDefault;
    #endregion


    #region Initialisation
    //      --------------

    bool isInitialising = true;


    protected override void OnTextBoxInitialized() {
      //verify the values set in XAML
      if (Min>Max) throw new Exception($"Error DecimalTextBox: Min {Min} must be <= Max {Max}.");
      if (Text.Length==0) {
        decimalValue = null;
      } else {
        decimalValue = Math.Round(decimal.Parse(Text), Decimals); //Decimals is already validated, throw exception here if Parse is not possible
        if (decimalValue<Min) throw new Exception($"Error DecimalTextBox: DecimalValue {DecimalValue} must be >= {Min} (Min).");
        if (decimalValue>Max) throw new Exception($"Error DecimalTextBox: DecimalValue {DecimalValue} must be <= {Max} (Max).");
        //
        Text = decimalValue?.ToString(DecimalFormat); //DecimalFormat doesn't need to get validated, any string is valid
      }
      isInitialising = false;
    }


    /// <summary>
    /// Sets initial value from code behind. If isRequired is true, user needs to change the value before saving is possible. 
    /// If min or max are null, Min or Max value gets not changed.
    /// </summary>
    public virtual void Initialise(
      decimal? value = null, 
      bool? isRequired = false, 
      decimal? min = null, 
      decimal? max = null,
      int? decimals = null,
      string? format = null) 
    {
      decimal newMin = min??Min;
      decimal newMax = max??Max;
      if (newMin>newMax) throw new Exception($"Error DecimalTextBox.Initialise(): Min {newMin} must be <= Max {newMax}.");
      if (value!=null) {
        if (value<newMin) throw new Exception($"Error DecimalTextBox.Initialise(): DecimalValue {value} must be >= {newMin} (Min).");
        if (value>newMax) throw new Exception($"Error DecimalTextBox.Initialise(): DecimalValue {value} must be <= {newMax} (Max).");
      }

      if (decimals.HasValue) {
        Decimals = decimals.Value; //validates
      }
      if (format!=null) {
        DecimalFormat = format; //validates
      }
      isInitialising = true;
      Min = newMin;
      Max = newMax;
      if (value is null) {
        decimalValue = null;
      } else {
        decimalValue =  Math.Round(value.Value, Decimals); ;
      }
      isInitialising = false;

      base.Initialise(decimalValue?.ToString(DecimalFormat), isRequired);
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
        } else if (c=='.') {
          //'.' is only allowed if no decimal point is entered already
          if (GetTextWithoutSelection().Contains('.')) {
            e.Handled = true;
          }
        } else if (!(c>='0' && c<='9')) {
          e.Handled = true;
        }
      }
      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      if (Text.Length==0) {
        decimalValue = null;
        return;
      }

      if (!decimal.TryParse(Text, out decimal newValue)) {
        ShowNotValidError(this);
        e.Handled = true;
      } else if(newValue<Min) {
        ShowSmallerMinError(this);
        e.Handled = true;
      } else if (newValue>Max) {
        ShowBiggerMaxError(this);
        e.Handled = true;
      } else {
        DecimalValue = Math.Round(newValue, Decimals);//will also format Text
      }
      base.OnPreviewLostKeyboardFocus(e);
    }


    public static void ShowNotValidErrorDefault(DecimalTextBox decimalTextBox) {
      MessageBox.Show($"{decimalTextBox.Text} is not a valid decimal.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }


    public static void ShowSmallerMinErrorDefault(DecimalTextBox decimalTextBox) {
      MessageBox.Show($"{decimalTextBox.Text} must be >= {decimalTextBox.Min} (Min).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }


    public static void ShowBiggerMaxErrorDefault(DecimalTextBox decimalTextBox) {
      MessageBox.Show($"{decimalTextBox.Text} must be <= {decimalTextBox.Max} (Max).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    #endregion
  }
}