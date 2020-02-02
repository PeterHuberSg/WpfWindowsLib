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
      if (!textBox.IsInitialized) return;

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


    /// <summary>
    /// Background of control after initialisation. Useful for inheriting class which needs to change
    /// the background to highlight the control and then wants to change back
    /// </summary>
    protected Brush? DefaultBackground { get; private set;}
    #endregion


    #region Constructor
    //      -----------

    /// <summary>
    /// Default constructor
    /// </summary>
    public CheckedTextBox() {
      Loaded += checkedTextBox_Loaded;
    }

    private void checkedTextBox_Loaded(object sender, RoutedEventArgs e) {
      //Background is only defined once default style is applied
      DefaultBackground = Background;
    }
    #endregion


    #region Initialisation
    //      --------------

    string initText = "";  //TextBox converts automatically null to an empty string. Don't allow null here neither


    protected override void OnInitialized(EventArgs e) {
      //verify the values set in XAML
      if (MaxLength>0 && Text.Length>MaxLength) throw new Exception($"Error CheckedTextBox: Text '{Text}' must be shorter than MaxLength {MaxLength}.");

      OnTextBoxInitialised();
      initText = Text;
      TextChanged += checkedTextBox_TextChanged;
      if (IsRequired) {
        IsAvailable = Text.Length>0;
        if (!IsAvailable) {
          showAvailability();
        }
      } else {
        //if no user input is required, the control is always available
        IsAvailable = true;
      }

      CheckedWindow.Register(this); 

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
      var newText = text??"";
      if (MaxLength>0 && newText.Length>MaxLength) {
        throw new Exception($"Error CheckedTextBox.Initialise(): Text '{newText}' must be shorter than MaxLength {MaxLength}.");
      }

      initText = newText; //TextBox converts null to empty string. initText must have here the same value like Text
      Text = newText; //resets HasChanged and updates isAvailable using old IsRequired

      IsRequired = isRequired??IsRequired; //will update isAvailable if IsRequired!=isRequired
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
        Background = DefaultBackground;
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
          Background = DefaultBackground;
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