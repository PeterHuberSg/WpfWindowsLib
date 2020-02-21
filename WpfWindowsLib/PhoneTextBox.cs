/**************************************************************************************

WpfWindowsLib.PhoneTextBox
==========================

TextBox accepting only phone numbers and implementing ICheck. 

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
  /// A TextBox accepting only phone number values. If it is placed in a Window inherited from CheckedWindow, 
  /// it reports automatically any Text change to that parent Window.
  /// </summary>
  public class PhoneTextBox: CheckedTextBox {

    #region Properties
    //      ----------

    /// <summary>
    /// The user might use blanks when entering the number, which are removed in CompactNumber
    /// </summary>
    public string? CompactNumber { get; private set; }


    #region MinDigits property
    public static readonly DependencyProperty MinDigitsProperty = DependencyProperty.Register(
      "MinDigits",
      typeof(int),
      typeof(PhoneTextBox),
      new FrameworkPropertyMetadata(7,
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onMinDigitsChanged)
      )
    );


    private static void onMinDigitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var phoneTextBox = (PhoneTextBox)d;
      if (phoneTextBox.isInitialising) return;

      //when MinDigits is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and MinDigits are assigned, if both are used in XAML
      if (phoneTextBox.MinDigits>phoneTextBox.MaxDigits) {
        throw new Exception($"Error PhoneTextBox: MinDigits {phoneTextBox.MinDigits} must be <= Max {phoneTextBox.MaxDigits}. " +
          "Use Initialise() to change both at the same time.");
      }
      var count = phoneTextBox.CountDigits();
      if (count!=0 && count<phoneTextBox.MinDigits) {
        throw new Exception($"Error PhoneTextBox: Phone number {phoneTextBox.Text} should have at least MinDigits {phoneTextBox.MinDigits} digits.");
      }
    }


    /// <summary>
    /// PhoneTextBox accepts only phone numbers with at least MinDigits digits.
    /// </summary>
    public int MinDigits {
      get { return (int)GetValue(MinDigitsProperty); }
      set { SetValue(MinDigitsProperty, value); }
    }
    #endregion


    #region MaxDigits property
    public static readonly DependencyProperty MaxDigitsProperty = DependencyProperty.Register(
      "MaxDigits",
      typeof(int),
      typeof(PhoneTextBox),
      new FrameworkPropertyMetadata(15,
          FrameworkPropertyMetadataOptions.None,
          new PropertyChangedCallback(onMaxDigitsChanged)
      )
    );


    private static void onMaxDigitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var phoneTextBox = (PhoneTextBox)d;
      if (phoneTextBox.isInitialising) return;

      //when MaxDigits is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and MaxDigits are assigned, if both are used in XAML
      if (phoneTextBox.MinDigits>phoneTextBox.MaxDigits) {
        throw new Exception($"Error PhoneTextBox: MinDigits {phoneTextBox.MinDigits} must be <= MaxDigits {phoneTextBox.MaxDigits}. " +
          "Use Initialise() to change both at the same time.");
      }
      var count = phoneTextBox.CountDigits();
      if (count!=0 && count>phoneTextBox.MaxDigits) {
        throw new Exception($"Error PhoneTextBox: Phone number {phoneTextBox.Text} should have at most MaxDigits {phoneTextBox.MaxDigits} digits.");
      }
    }


    /// <summary>
    /// PhoneTextBox accepts only a DecimalValue smaller equal than MaxDigits 
    /// </summary>
    public int MaxDigits {
      get { return (int)GetValue(MaxDigitsProperty); }
      set { SetValue(MaxDigitsProperty, value); }
    }
    #endregion


    /// <summary>
    /// Characters that user can input for phone numbers, additionally to the digits '0' to '9' and a
    /// '+' as first character.
    /// </summary>
    public static char[] LegalSpecialCharacters = {'-', '/'};


    /// <summary>
    /// ValidateUserInput gets called every time a user has keyed in a character, except when he enters a blank. Assign
    /// a different method to change how user input gets validated.
    /// </summary>
    public Func<char /*inputChar*/, int /*inputPos*/, string /*partialPhoneNumber*/, bool> ValidateUserInput = DefaultValidateUserInput;


    /// <summary>
    /// Delegate for ValidatePhoneNumber, which validates that phoneNumber is a valid phone number. When returning, 
    /// compactNumber might have been reformatted.
    /// </summary>
    public delegate bool ValidatePhoneNumberDelegate(string? phoneNumber, out string? compactNumber);


    /// <Summary>
    /// Validates that phoneNumber is a valid phone number. When returning, compactNumber might have been reformatted.
    /// </summary>
    public ValidatePhoneNumberDelegate ValidatePhoneNumber = DefaultValidatePhoneNumber;
    #endregion


    #region Initialisation
    //      --------------

    // True while XAML code gets executed and during Initialise(). This prevents that each value change is validated
    // on its own. Instead, validation gets executed once all values are changed.
    bool isInitialising = true;


    protected override void OnTextBoxInitialized() {
      //verify the values set in XAML
      if (!ValidatePhoneNumber(Text, out var compactNumber)) {
        throw new Exception($"Error PhoneTextBox: Text {Text} is not a valid phone number."); 
      }
      Text = CountryCode.Format(Text)??Text;
      var count = CountDigits();
      if (count!=0) {
        if (count<MinDigits) {
          throw new Exception($"Error PhoneTextBox: Phone number {Text} should have at least MinDigits {MinDigits} digits.");
        }
        if (count>MaxDigits) {
          throw new Exception($"Error PhoneTextBox: Phone number {Text} should have at most MaxDigits {MaxDigits} digits.");
        }
      }

      CompactNumber = compactNumber;
    }


    /// <summary>
    /// Sets initial value from code behind. If isRequired is true, user needs to change the value before saving is possible.
    /// If minDigits or maxDigits are null, MinDigits or MaxDigits value gets not changed.
    /// </summary>
    public virtual void Initialise(string? text, bool? isRequired = null, int? minDigits = null, int? maxDigits = null) {
      if (!ValidatePhoneNumber(text, out var compactNumber)) {
        throw new Exception($"Error PhoneTextBox.Initialise(): Text {text} is not a valid phone number.");
      }
      var newMinDigits = minDigits??MinDigits;
      var newMaxDigits = maxDigits??MaxDigits;
      var count = CountDigits();
      if (count!=0) {
        if (count<newMinDigits) {
          throw new Exception($"Error PhoneTextBox.Initialise(): Phone number {Text} should have at least MinDigit {newMinDigits} digits.");
        }
        if (count>newMaxDigits) {
          throw new Exception($"Error PhoneTextBox.Initialise(): Phone number {Text} should have at most MaxDigits {newMaxDigits} digits.");
        }
      }
      isInitialising = true;
      MinDigits = newMinDigits;
      MaxDigits = newMaxDigits;
      CompactNumber = compactNumber;
      isInitialising = false;

      if (text!=null) {
        text = CountryCode.Format(text)??text;
      }
      base.Initialise(text, isRequired);
    }
    #endregion


    #region Overrides
    //      ---------

    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
      if (e.Text.Length>0) { //ctrl + key results in Text.Length==0
        if (e.Text.Length!=1) throw new NotSupportedException($"PhoneTextBox supports only ASCII code.");

        var inputChar = e.Text[0];
        var textWithoutSelection = GetTextWithoutSelection();
        if (!ValidateUserInput(inputChar, CaretIndex, textWithoutSelection) ||
          (inputChar>='0' && inputChar<='9' && CountDigits()>=MaxDigits)) {
          e.Handled= true;
        }
      }

      base.OnPreviewTextInput(e);
    }


    protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
      var count = CountDigits();
      if (!ValidatePhoneNumber(Text, out var compactNumber)) {
        MessageBox.Show($"Illegal phone number '{Text}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else if(count!=0 && count<MinDigits) { 
        MessageBox.Show($"Phone number '{Text}' should have at least {MinDigits} digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else if (count>MaxDigits) {
        MessageBox.Show($"Phone number '{Text}' should have at most {MaxDigits} digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      } else {
        CompactNumber = compactNumber;
      }

      Text = CountryCode.Format(Text)??Text;
      base.OnPreviewLostKeyboardFocus(e);
    }
    #endregion


    #region Validation
    //      ----------

    /// <summary>
    /// Returns true if inputChar can be entered at inputPos into partialPhoneNumber, which is the phone 
    /// number the user has entered so far.
    /// </summary>
    public static bool DefaultValidateUserInput(char inputChar, int inputPos, string partialPhoneNumber) {
      //if user enters a blank, DefaultValidateUserInput does not get called

      if (inputChar>='0' && inputChar<='9') return true;

      if (inputChar=='+') return inputPos==0 && !partialPhoneNumber.Contains('+');

      foreach (var legalChar in LegalSpecialCharacters) {
        if (legalChar==inputChar) return true;
      }

      return false;
    }


    /// <Summary>
    /// Validates that phoneNumber is a valid phone number. On returning, special characters like '+', ' ' and LegalSpecialCharacters
    /// are removed from compactNumber
    /// </summary>
    private static bool DefaultValidatePhoneNumber(string? phoneNumber, out string? compactNumber) {
      if (string.IsNullOrEmpty(phoneNumber)) {
        compactNumber = null;
        return true;
      }

      var isFirstChar = true;
      var compactNumberSB = new StringBuilder();
      foreach (var phoneChar in phoneNumber) {
        if (phoneChar==' ') {
          continue;
        }
        if (phoneChar>='0' && phoneChar<='9'|| isFirstChar && phoneChar=='+') {
          compactNumberSB.Append(phoneChar);
        } else {
          var isLegal = false;
          foreach (var legalChar in LegalSpecialCharacters) {
            if (legalChar==phoneChar) {
              isLegal = true;
              break;
            }
          }
          if (!isLegal) {
            compactNumber = null;
            return false;
          }
        }
        if (isFirstChar) {
          isFirstChar = false;
        }
      }
      compactNumber = compactNumberSB.ToString();
      return true;
    }
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Counts the number of digits in Text
    /// </summary>
    public int CountDigits() {
      var count = 0;
      foreach (var textChar in Text) {
        if (textChar>='0' && textChar<='9') {
          count++;
        }
      }
      return count;
    }
    #endregion
  }
}
