/**************************************************************************************

WpfWindowsLib.CheckedComboBox
=============================

ComboBox implementing ICheck

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace WpfWindowsLib {


  /// <summary>
  /// If this ComboBox is placed in a Window inherited from CheckedWindow, it reports automatically 
  /// any value change to that parent Window.<para/>
  /// Read SelectedIndexNull if int? is needed instead of int (SelectedIndex). NotSelectedIndex defines which
  /// SelectedIndex value represents null, usually 0.
  /// </summary>
  public class CheckedComboBox: ComboBox {

    /***************************************************************************************************
    Comments WPF ComboBox:
    The design of the ComboBox is unfortunate, because it knows the state where the user has chosen 
    nothing (SelectedIndex=-1), but there is no way for the user to undo a once made choice. Once he 
    has selected something, there is no way he can return to SelectedIndex==-1. For ICheck.HasChanged to 
    work properly, the user must be able to undo anything he has done. Meaning the functionality of 
    SelectedIndex==-1 cannot be used.
    
    ComboBox offers the user to select from a limited range of integers. For other ICheck controls
    we try to provide also a nullable value, in this case int?. Since ComboBox supports only a small
    range of integers, it is always possible to add one more value which stands in for null. Normally,
    that would be SelectedIndex==0. 

    To prevent any problem with SelectedIndex==-1, SelectedIndex gets set to 0, it if is -1 during
    OnInitialized(EventArgs e).

    For these reasons, the following approach is taken. When IsRequired==true, IsAvailable is true when
    SelectedIndex!=0. SelectedIndexNullable returns null when SelectedIndex==0.

    Another shortcoming of the ComboBox design is that BackGround does not change how ComboBox looks
    like. Instead, the Background of the Border inside the ComboBox needs to be changed.
    ***************************************************************************************************/

    #region Properties
    //      ----------

    /// <summary>
    /// Same as SelectedIndex, but treats 0 as null
    /// </summary>
    public int? SelectedIndexNullable { 
      get { return (SelectedIndex==0 ? (int?)null : SelectedIndex); } 
      set { SelectedIndex = value??0; }
    }


    #region IsRequired property
    public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
      "IsRequired",
      typeof(bool),
      typeof(CheckedComboBox),
      new FrameworkPropertyMetadata(false,
          FrameworkPropertyMetadataOptions.AffectsRender,
          new PropertyChangedCallback(onIsRequiredChanged)
      )
    );


    private static void onIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var checkedComboBox = (CheckedComboBox)d;
      if (checkedComboBox.isInitialising) return;

      //when IsRequired is set in XAML, it will not be handled here but in OnInitialized(), which
      //guarantees that the control's values and IsRequired are assigned, if both are used in XAML
      checkedComboBox.IChecker.IsRequiredChanged(checkedComboBox.IsRequired, isAvailable: checkedComboBox.SelectedIndex!=0);
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
    /// Has the value of this control changed since it was initialised ?
    /// </summary>
    public bool HasChanged { get { return IChecker.HasChanged; } }


    /// <summary>
    /// Provides the ICheck functionality to CheckedComboBox
    /// </summary>
    public IChecker<int> IChecker { get; }
    #endregion


    #region Constructor
    //      -----------

    /// <summary>
    /// Default constructor
    /// </summary>
    public CheckedComboBox() {
      IChecker = new IChecker<int>(this) {
        OnChangeBackground = onChangeBackground,
        OnClearBackground = onClearBackground,
        OnSetBackground = onSetBackground,
        OnResetBackground = onResetBackground
      };

      LayoutUpdated += checkedComboBox_LayoutUpdated;
    }

    #endregion


    #region Initialisation
    //      --------------


    bool isInitialising = true;


    protected override void OnInitialized(EventArgs e) {
      if (SelectedIndex==-1) {
        SelectedIndex = 0;
      }

      IChecker.OnInitialized(initValue: SelectedIndex, IsRequired, isAvailable: SelectedIndex!=0);
      isInitialising = false;
      //add event handlers only once XAML values are processed, i.e in OnInitialized. 
      SelectionChanged += checkedComboBox_SelectionChanged;
      base.OnInitialized(e);
    }


    /// <summary>
    /// Sets SelectedIndex and IsRequired from code behind.<para/>
    /// If selectedIndex is null, SelectedIndex is set to 0.<para/>
    /// If isRequired is null, IsRequired keeps its value.
    /// </summary>
    public virtual void Initialise(int? selectedIndex = 0, bool? isRequired = null) {
      isInitialising = true;
      if (selectedIndex.HasValue) {
        SelectedIndex = selectedIndex.Value;
      } else {
        SelectedIndex = 0;
      }
      if (isRequired.HasValue) {
        IsRequired = isRequired.Value;
      }
      IChecker.Initialise(initValue: SelectedIndex, IsRequired, isAvailable: SelectedIndex!=0);
      isInitialising = false;
    }
    #endregion


    #region Event Handlers
    //      --------------

    ToggleButton? toggleButton;


    public override void OnApplyTemplate() {
      //unfortunately, Loaded event also fires when template is not applied, for example when control's
      //Visibility = Collapsed. Since there is no TemplateApllied event, OnApplyTemplate has to be used 
      //to find toggleButton in the template
      base.OnApplyTemplate();

      //if (DesignerProperties.GetIsInDesignMode(this)) return;
      toggleButton = (ToggleButton)Template.FindName("toggleButton", this);
      //comboBoxBorder can not be searched here yet, because the templates visual tree is not created yet :-)
    }


    Border? comboBoxBorder;
    
    
    private void checkedComboBox_LayoutUpdated(object? sender, EventArgs e) {
      //this is the only event I could find firing after OnApplyTemplate().
      if (toggleButton==null || comboBoxBorder!=null) return;

      comboBoxBorder = (Border)VisualTreeHelper.GetChild(toggleButton, 0);
      if (delayedBackground==null) return;

      comboBoxBorder.Background = delayedBackground;
    }


    private void checkedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
      if (isInitialising) return;

      IChecker.ValueChanged(SelectedIndex, isAvailable: SelectedIndex!=0);
    }
    #endregion


    #region Methods
    //      -------

    Brush? delayedBackground = null;


    private void onChangeBackground(Brush backgroundBrush) {
      if (comboBoxBorder is null) {
        delayedBackground = backgroundBrush;
      } else {
        comboBoxBorder.Background = backgroundBrush!;
      }
    }


    private void onClearBackground() {
      comboBoxBorder!.ClearValue(Control.BackgroundProperty);
    }


    Brush? oldBackground;


    private void onSetBackground(Brush backgroundBrush) {
      oldBackground = comboBoxBorder!.Background;
      comboBoxBorder!.Background = backgroundBrush!;
    }


    /// <summary>
    /// Default version for OnResetBackground.
    /// </summary>
    private void onResetBackground() {
      comboBoxBorder!.Background = oldBackground!;
    }
    #endregion
  }
}