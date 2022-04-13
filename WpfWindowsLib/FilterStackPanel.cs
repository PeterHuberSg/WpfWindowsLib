/**************************************************************************************

WpfWindowsLib.FilterStackPanel
==============================

Can hold some controls for filtering, usually used at the top of the window before 
displaying a DataGrid

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
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfWindowsLib {


  /// <summary>
  /// Used in the upper part of a window hosting the filter buttons and stuff. It sets
  /// styles for TextBoxes, buttons, labels, etc.
  /// </summary>
  public class FilterStackPanel: StackPanel {

    public FilterStackPanel() {
      /*
      <StackPanel Background="Gainsboro" Orientation="Horizontal">
        <StackPanel.Resources>
          <Style TargetType="Button">
            <Setter Property="VerticalAlignment" Value="Center" />
          </Style>
        </StackPanel.Resources>
      </StackPanel>
      */
      Background = Styling.PanelBackgroundBrush;
      Orientation = Orientation.Horizontal;

      var buttonMargin = new Thickness(5,3,5,3); //no labels, need left margin
      var checkBoxMargin = new Thickness(0,5,5,3); //have labels, no left margin.
      var comboBoxMargin = new Thickness(0,5,5,3); //have labels, no left margin.
      var datePickerMargin = new Thickness(0,5,5,3); //have labels, no left margin. Increase top margin a bit to align text with TextBlocks
      var radioButtonMargin = new Thickness(5, 3, 5, 3); //no labels, need left margin
      var textBoxMargin = new Thickness(0,5,5,3); //have labels, no left margin. Increase top margin a bit to align text with TextBlocks

      var buttonStyle = new Style(typeof(Button));
      buttonStyle.Setters.Add(new Setter(Button.VerticalAlignmentProperty, VerticalAlignment.Center));
      buttonStyle.Setters.Add(new Setter(Button.MarginProperty, buttonMargin));
      Resources.Add(typeof(Button), buttonStyle);

      var checkBoxStyle = new Style(typeof(CheckBox));
      checkBoxStyle.Setters.Add(new Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center));
      checkBoxStyle.Setters.Add(new Setter(CheckBox.MarginProperty, checkBoxMargin));
      Resources.Add(typeof(CheckBox), checkBoxStyle);

      var comboBoxStyle = new Style(typeof(ComboBox));
      comboBoxStyle.Setters.Add(new Setter(ComboBox.VerticalAlignmentProperty, VerticalAlignment.Center));
      comboBoxStyle.Setters.Add(new Setter(ComboBox.MarginProperty, comboBoxMargin));
      Resources.Add(typeof(ComboBox), comboBoxStyle);

      var datePickerStyle = new Style(typeof(DatePicker));
      datePickerStyle.Setters.Add(new Setter(DatePicker.VerticalAlignmentProperty, VerticalAlignment.Center));
      datePickerStyle.Setters.Add(new Setter(DatePicker.MarginProperty, datePickerMargin));
      Resources.Add(typeof(DatePicker), datePickerStyle);

      var labelStyle = new Style(typeof(Label));
      labelStyle.Setters.Add(new Setter(Label.VerticalAlignmentProperty, VerticalAlignment.Center));
      //labels don't need a margin, because they have a padding of 5
      Resources.Add(typeof(Label), labelStyle);

      var radioButtonStyle = new Style(typeof(RadioButton));
      radioButtonStyle.Setters.Add(new Setter(Label.VerticalAlignmentProperty, VerticalAlignment.Center));
      radioButtonStyle.Setters.Add(new Setter(RadioButton.MarginProperty, radioButtonMargin));
      Resources.Add(typeof(RadioButton), radioButtonStyle);

      var textBlockStyle = new Style(typeof(TextBlock));
      textBlockStyle.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
      //don't add margins for TextBlock, because they are used also in buttons, labels, ...
      Resources.Add(typeof(TextBlock), textBlockStyle);

      var textBoxStyle = new Style(typeof(TextBox));
      textBoxStyle.Setters.Add(new Setter(TextBox.VerticalAlignmentProperty, VerticalAlignment.Center));
      textBoxStyle.Setters.Add(new Setter(TextBox.MarginProperty, textBoxMargin));
      Resources.Add(typeof(TextBox), textBoxStyle);

    }
  }
}