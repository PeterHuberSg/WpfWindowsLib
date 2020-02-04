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
using System.Windows.Media;


namespace WpfWindowsLib {


  /// <summary>
  /// If this TextBox is placed in a Window inheriting from CheckedWindow, it reports automatically 
  /// any value change to that parent Window.
  /// </summary>
  public class CheckedTextBox: TextBox, ICheck {

    #region Properties
    //      ----------

    /// <summary>
    /// TextBox automatically converts null to empty string. But if string? is required, use 
    /// TextNullable. It is null when Text is an empty string. 
    /// </summary>
    public string? TextNullable { get { return Text.Length==0 ? null : Text; } }


    /// <summary>
    /// Has the value of this control changed ?
    /// </summary>
    public bool HasChanged { get; private set; }


    /// <summary>
    /// Raised when control gets changed or the user undoes the change
    /// </summary>
    public event Action?  HasChangedEvent;


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
      var oldIsAvailable = textBox.IsAvailable;
      if (textBox.IsRequired) {
        textBox.IsAvailable = textBox.Text.Length>0;
      } else {
        textBox.IsAvailable = true;
      }

      if (oldIsAvailable!=textBox.IsAvailable) {
        textBox.showAvailability();
        textBox.IsAvailableEvent?.Invoke();
      }
    }


    /// <summary>
    /// Needs the user to provide this control with a value ? 
    /// </summary>
    public bool IsRequired {
      get { return (bool)GetValue(IsRequiredProperty); }
      set { SetValue(IsRequiredProperty, value); }
    }
    #endregion


    /// <summary>
    /// Has the user provided a value ?
    /// </summary>
    public bool IsAvailable { get; private set; }


    /// <summary>
    /// The availability of the control has changed
    /// </summary>
    public event Action?  IsAvailableEvent;
    #endregion


    #region Initialisation
    //      --------------

    string initText = "";  //TextBox converts automatically null to an empty string. Don't allow null here neither
    bool isInitialising = true;


    protected override void OnInitialized(EventArgs e) {
      //verify the values set in XAML
      if (MaxLength>0 && Text.Length>MaxLength) throw new Exception($"Error CheckedTextBox: Text '{Text}' must be shorter than MaxLength {MaxLength}.");

      OnTextBoxInitialised();
      initText = Text;
      TextChanged += checkedTextBox_TextChanged;
      if (IsRequired) {
        IsAvailable = Text.Length>0;
        if (!IsAvailable) {
          //a changed background needs only to be displayed if control is required, but not available
          showAvailability();
        }
      } else {
        //if no user input is required, the control is always available
        IsAvailable = true;
      }
      isInitialising = false;

      CheckedWindow.Register(this); //updates CheckedWindow.IsAvailable, no need to raise IsAvailableEvent

      base.OnInitialized(e);
    }


    /// <summary>
    /// Inheritors might need to handle here Text content set in XAML
    /// </summary>
    protected virtual void OnTextBoxInitialised() {}


    /// <summary>
    /// Sets Text and IsRequired from code behind. If isRequired is null, IsRequired keeps its value.
    /// </summary>
    public virtual void Initialise(string? text = null, bool? isRequired = false) {
      isInitialising = true;
      var newText = text??"";
      if (MaxLength>0 && newText.Length>MaxLength) {
        throw new Exception($"Error CheckedTextBox.Initialise(): Text '{text}' must be shorter than MaxLength {MaxLength}.");
      }
      initText = newText;
      Text = newText;
      IsRequired = isRequired??IsRequired;
      isInitialising = false;

      var newHasChanged = initText!=Text;
      if (HasChanged!=newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      var newIsAvailable = !IsRequired||Text.Length>0;
      if (IsAvailable!=newIsAvailable) {
        IsAvailable = newIsAvailable;
        showAvailability();
        IsAvailableEvent?.Invoke();
      }
    }


    /// <summary>
    /// Called from CheckedWindow after a save, sets Text as initial value
    /// </summary>
    public void ResetHasChanged() {
      initText = Text;
      HasChanged = false;
    }
    #endregion


    #region Event Handlers
    //      --------------

    private void checkedTextBox_TextChanged(object sender, TextChangedEventArgs e) {
     var newHasChanged = initText!=Text;
      if (HasChanged != newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = Text.Length>0;
        if (IsAvailable!=newIsAvailable) {
          IsAvailable = newIsAvailable;
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }
    #endregion


    #region Methods
    //      -------

    private void showAvailability() {
      if (IsAvailable) {
        ClearValue(TextBox.BackgroundProperty);
      } else {
        Background = Styling.RequiredBrush;
      }
    }


    /// <summary>
    /// Change the background color of this control if the user has changed its value
    /// </summary>
    public void ShowChanged(bool isChanged) {
      if (HasChanged) {
        if (isChanged) {
          Background = Styling.HasChangedBackgroundBrush;
        } else {
          ClearValue(TextBox.BackgroundProperty);
        }
      }
    }


    /// <summary>
    /// Returns Text without the characters selected by user. Useful in OnPreviewTextInput()
    /// </summary>
    /// <returns></returns>
    public string GetTextWithoutSelection() {
      if (SelectedText.Length==0) return Text;

      return Text[..CaretIndex] + Text[(CaretIndex+SelectedText.Length)..];
    }
    #endregion
  }
}