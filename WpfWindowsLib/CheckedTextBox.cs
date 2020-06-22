/**************************************************************************************

WpfWindowsLib.CheckedTextBox
============================

TextBox implementing ICheck

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
using System.Windows.Controls;


namespace WpfWindowsLib {


  /// <summary>
  /// If this TextBox is placed in a Window inheriting from CheckedWindow, it reports automatically 
  /// any Text change to that parent Window.
  /// </summary>
  public class CheckedTextBox: TextBox {

    #region Properties
    //      ----------

    /// <summary>
    /// TextBox automatically converts null to empty string. But if string? is required, use 
    /// TextNullable. It is null when Text is an empty string. 
    /// </summary>
    public string? TextNullable { get { return Text.Length==0 ? null : Text; } }


    #region IsRequired property
    public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
      "IsRequired",
      typeof(bool),
      typeof(CheckedTextBox),
      new FrameworkPropertyMetadata(false,
          FrameworkPropertyMetadataOptions.AffectsRender,
          new PropertyChangedCallback(onIsRequiredChanged)
      )
    );


    private static void onIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var textBox = (CheckedTextBox)d;
      if (textBox.isInitialising) return;

      //when IsRequired is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that Text and IsRequired are assigned, if both are used in XAML
      textBox.IChecker.IsRequiredChanged(textBox.IsRequired, isAvailable: textBox.Text.Length>0);
    }


    /// <summary>
    /// Needs the user to provide this control with a value ? 
    /// </summary>
    public bool IsRequired {
      get { return (bool)GetValue(IsRequiredProperty); }
      set { SetValue(IsRequiredProperty, value);}
    }
    #endregion


    /// <summary>
    /// Has the value of this control changed since it was initialised ?
    /// </summary>
    public bool HasChanged { get { return IChecker.HasChanged; } }


    /// <summary>
    /// Provides the ICheck functionality to CheckedTextBox
    /// </summary>
    public IChecker<string?> IChecker { get; }
    #endregion


    #region Constructor
    //      -----------

    public CheckedTextBox() {
      IChecker = new IChecker<string?>(this);
    }
    #endregion


    #region Initialisation
    //      --------------

    bool isInitialising = true;


    protected override void OnInitialized(EventArgs e) {
      //verify the values set in XAML
      if (MaxLength>0 && Text.Length>MaxLength) throw new Exception($"Error CheckedTextBox: Text '{Text}' must be shorter than MaxLength {MaxLength}.");

      IChecker.OnInitialized(initValue: Text, IsRequired, isAvailable: Text.Length>0);
      isInitialising = false;
      OnTextBoxInitialized();
      //add event handlers only once XAML values are processed, i.e in OnInitialized. 
      TextChanged += checkedTextBox_TextChanged;
      base.OnInitialized(e);
    }


    /// <summary>
    /// Inheritors might need to handle here Text content set in XAML
    /// </summary>
    protected virtual void OnTextBoxInitialized() {}


    /// <summary>
    /// Sets Text and IsRequired from code behind. If isRequired is null, IsRequired keeps its value.
    /// </summary>
    public virtual void Initialise(string? text = null, bool? isRequired = false) {
      isInitialising = true;
      var newText = text??"";
      if (MaxLength>0 && newText.Length>MaxLength) {
        throw new Exception($"Error CheckedTextBox.Initialise(): Text '{text}' must be shorter than MaxLength {MaxLength}.");
      }
      Text = newText;
      IsRequired = isRequired??IsRequired;
      IChecker.Initialise(initValue: newText, IsRequired, isAvailable: Text.Length>0);
      isInitialising = false;
    }
    #endregion


    #region Event Handlers
    //      --------------

    private void checkedTextBox_TextChanged(object sender, TextChangedEventArgs e) {
      if (isInitialising) return;

      IChecker.ValueChanged(Text, Text.Length>0);
    }
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Returns Text without the characters selected by user. Useful in OnPreviewTextInput()
    /// </summary>
    public string GetTextWithoutSelection() {
      if (SelectedText.Length==0) return Text;

      return Text[..CaretIndex] + Text[(CaretIndex+SelectedText.Length)..];
    }
    #endregion
  }
}