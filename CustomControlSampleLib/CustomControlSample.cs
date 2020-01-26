/**************************************************************************************

CustomControlSampleLib.CustomControlSample
==========================================

Example showing how to write custom control using CustomControlBase.
Just copy the text into your own control and follow the instructions for the parts needed changes.
Make your own library for your controls, keep them separate from the Windows

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
using System.Windows.Data;
using System.Windows.Media;
using WpfWindowsLib;

namespace CustomControlSampleLib {


  /// <summary>
  /// CustomControlSample displays a TextBox on the right side and an ellipse on the left. The TextBox takes the 
  /// size as needed by its content, while the ellipse has the same dimensions like the TextBox.
  /// </summary>
  public class CustomControlSample: CustomControlBase {
    // CustomControlSample provides a code sample showing what an inheritor of CustomControlBase usually needs to do:
    // 1) add some FrameworkElements as children of CustomControlSample (=children)
    // 2) override MeasureContentOverride (instead of MeasurementOverrid). The provided Size constraint is reduced by Border and 
    //    Padding
    // 3) override ArrangeContentOverride (instead of ArrangeOverride). The parameter arrangeRect is a rectangle, not a
    //    size, to provide an offset for Border and Padding. Use ArrangeBorderPadding() instead of Arrange() to arrange the children
    // 4) override OnRenderContent instead of OnRender to draw content directly to drawingContext
    //
    //There are 2 types of content a control inheriting from CustomControlBase can use:
    //+ FramworkElement: AddChild() in constructor and overwrite MeasureContentOverride and ArrangeContentOverride
    //+ draw to drawingContext: overwrite MeasureContentOverride, ArrangeContentOverride and OnRenderContent
    //
    //CustomControlSample has one TextBox child and draws a ellipse to demonstrate both cases


    /// <summary>
    /// Gives access to textBox for testing purpose
    /// </summary>
    public TextBox ChildTextBox; //child of CustomControlBase 


    #region 1) create and add Framework controls as children to constructor
    //      ---------------------------------------------------------------

    /// <summary>
    /// Default constructor
    /// </summary>
    public CustomControlSample() {
      //add FrameworkElement children, if any
      ChildTextBox = new TextBox { 
        Text="Change text for testing" + Environment.NewLine + "Change Window Size",
        AcceptsReturn= true
      };
      ChildTextBox.TextChanged += new TextChangedEventHandler(childTextBox_TextChanged);

      Binding newBinding = new Binding("Foreground") {
        Source = this,
        Mode = BindingMode.OneWay
      };
      ChildTextBox.SetBinding(TextBox.ForegroundProperty, newBinding);
      AddChild(ChildTextBox);

      //change some CustomControlBase properties
      Background = Brushes.Goldenrod;
      BorderBrush = Brushes.DarkGoldenrod;
    }


    void childTextBox_TextChanged(object sender, TextChangedEventArgs e) {
      if (HorizontalAlignment==HorizontalAlignment.Stretch && HorizontalContentAlignment!=HorizontalAlignment.Stretch) {
        //the width of the ChildTextBox and therefore of the ellipse is defined by the content of the ChildTextBox

        //FrameworkElement.ArrangeCore() only calls CustomControlSample.OnRender() if the size of CustomControlSample has
        //changed. Just changing the ChildTextBox content might not change the size of CustomControlSample, but the ellipse
        //has to be redrawn. Unfortunately, Microsoft does not allow in ArrangeOverride() to force a render. InvalidateVisual()
        //does not just force a render, but forces also measure and arrange.

        InvalidateVisual();
      }
    }
    #endregion


    #region 2) overwrite MeasureContentOverride instead of MeasurementOverrid
    //      -----------------------------------------------------------------

    protected override Size MeasureContentOverride(Size constraint) {
      //constraint is already reduced by Border and Padding.

      ChildTextBox.HorizontalAlignment = HorizontalContentAlignment;
      ChildTextBox.VerticalAlignment = VerticalContentAlignment;

      //constraint can be between 0 and infinite, but textBox.Measure() is able to handle that.
      //The return value from ChildTextBox.Measure() is in DesiredSize, which tells how much space the ChildTextBox requests. If 
      //width is set, ChildTextBox requests the same width. Otherwise it requests the size needed to display its content.

      //In this sample, ChildTextBox.Width is always NAN. Give ChildTextBox only half the available width
      ChildTextBox.Measure(new Size(constraint.Width/2, constraint.Height));

      //minimum size needed for TextBox and equally sized ellipse
      Size returnSize = new Size(2*ChildTextBox.DesiredSize.Width, ChildTextBox.DesiredSize.Height);
      if (HorizontalAlignment==HorizontalAlignment.Stretch) {
        //if ChildTextBox does not need all the space but content(!) should be stretched, request full width
        returnSize.Width = Math.Max(returnSize.Width, constraint.Width);
      }
      if (VerticalAlignment==VerticalAlignment.Stretch) {
        //if ChildTextBox does not need all the space but content(!) should be stretched, request full height
        returnSize.Height = Math.Max(returnSize.Height, constraint.Height);
      }
      return returnSize;
    }
    #endregion


    #region 3) override ArrangeContentOverride instead of ArrangeOverride
    //      -------------------------------------------------------------
    //
    // 3.1) Don't use CustomControl.DesiredSize in Arrange(), because DesiredSize is with Margin. Use arrangeRect instead, which is 
    //      without Margin, Border and Padding. DesiredSize should only be used when dealing with the children of the CustomControl.
    // 3.2) use ArrangeBorderPadding() instead of Arrange() to arrange children frameworkElements
    // 3.3) if IsSizingToContent() then use only the space needed, otherwise use all space provided

    double ellipsRenderWidth = double.NegativeInfinity;


    protected override Size ArrangeContentOverride(Rect arrangeRect) {
      if (ChildTextBox.HorizontalAlignment!=HorizontalContentAlignment || ChildTextBox.VerticalAlignment!=VerticalContentAlignment) {
        //ensure that arrange and measure use the same ContentAlignment, i.e. that it has not changed between the 2 calls
        throw new Exception("ContentAlignment has changed between Measure() and Arrange().");
      }

      //arrange visual children here. This will also call their Render method.

      ////calculate total desired space. loop over all children. Include also the space needed for OnRender
      //double totalDesiredWidth = 2*textBox.DesiredSize.Width;
      //double totalDesiredHeight = textBox.DesiredSize.Height;

      //arrange: 
      //loop over all FrameworkElement children. This sample has only 1 FrameworkElement child. The
      //children rendered by CustomControlSample directly will be arranged in OnRender
      Size availableTextboxSize;
      if (IsSizingWidthToExpandableContent()) {
        //ChildTextBox should use only space really needed. To let Framework.ArrangeCore() to get the alignment right, all
        //the space needs to be given to textbox.Arrange(), except the space which is used by the ellipse.
        double ellipseWidth = ChildTextBox.DesiredSize.Width;
        //Warning: Actually, it would be better to use the RenderSize after arranging the ChildTextBox instead of DesiredSize. But the 
        //code here is executed before the arrange, meaning RenderSize is not updated yet. Luckily, in CustomControlSample the 
        //measured size and the arranged size will be the same when the ChildTextBox does not need to grow (i.e. not width defined nor 
        //content stretched)
        availableTextboxSize = new Size(arrangeRect.Size.Width-ellipseWidth, arrangeRect.Size.Height);
      } else {
        //ChildTextBox has to use half of the available space. No content alignment
        availableTextboxSize = new Size(arrangeRect.Size.Width/2, arrangeRect.Size.Height);
      }
      //bool isContentToUseAllSpace = HorizontalAlignment==HorizontalAlignment.Stretch && HorizontalContentAlignment!=HorizontalAlignment.Stretch;
      //if (isContentToUseAllSpace) {
      //  //ChildTextBox should use only space really needed. To let Framework.ArrangeCore() to get the alignment right, all
      //  //the space needs to be given to ChildTextBox.Arrange(), except the space which is used by the ellipse.
      //  availableTextboxSize = new Size(arrangeRect.Size.Width-ellipseWidth, arrangeRect.Size.Height);
      //} else {
      //  //ChildTextBox has to use half of the available space. No content alignment
      //  availableTextboxSize = new Size(arrangeRect.Size.Width/2, arrangeRect.Size.Height);
      //}

      ChildTextBox.ArrangeBorderPadding(arrangeRect, 0, 0, availableTextboxSize.Width, availableTextboxSize.Height);

      if (ellipsRenderWidth!=ChildTextBox.RenderSize.Width) {
        ellipsRenderWidth = ChildTextBox.RenderSize.Width;
      }
      return arrangeRect.Size;
    }
    #endregion


    #region 4) override OnRenderContent instead of OnRender
    //      -----------------------------------------------
    // 4.1) drawingContext is moved already to the left and down to cater for Margin, Border and Padding.
    // 4.2) The CustomsControl's RenderSize cannot be used, since it includes Border and Padding. renderContentSize must be used instead, which
    // is exactly the same as what ArrangeContentOverride() returned. RenderSize would be used for children FrameworkElements.

    protected override void OnRenderContent(System.Windows.Media.DrawingContext drawingContext, Size renderContentSize) {
      //draws an ellipse next to the ChildTextBox using the same size
      double radiusX = ChildTextBox.RenderSize.Width/2;
      double radiusY = ChildTextBox.RenderSize.Height/2;
      double offsetX;
      double offsetY;
      if (!double.IsNaN(Width) || HorizontalAlignment==HorizontalAlignment.Stretch) {
        //HorizontalContentAlignment matters only if space available is different from the needed space, which is only possible if 
        //CustomControlSample is stretched or a width is defined
        switch (HorizontalContentAlignment) {
        case HorizontalAlignment.Left:
        case HorizontalAlignment.Stretch:
          offsetX = ChildTextBox.RenderSize.Width + radiusX;
          break;
        case HorizontalAlignment.Center:
          offsetX = renderContentSize.Width/2 + radiusX;
          break;
        case HorizontalAlignment.Right:
          offsetX = renderContentSize.Width - radiusX;
          break;
        default:
          throw new NotSupportedException();
        }
      } else {
        //if CustomControlSample is not stretched, renderContentSize.Width=2*textBox.RenderSize.Width
        offsetX = ChildTextBox.RenderSize.Width + radiusX;
      }
      if (!double.IsNaN(Height) || VerticalAlignment==VerticalAlignment.Stretch) {
        //VerticalContentAlignment matters only if space available is different from the needed space, which is only possible if 
        //CustomControlSample is stretched or height is defined
        switch (VerticalContentAlignment) {
        case VerticalAlignment.Top:
        case VerticalAlignment.Stretch:
          offsetY = radiusY;
          break;
        case VerticalAlignment.Center:
          offsetY = renderContentSize.Height/2;
          break;
        case VerticalAlignment.Bottom:
          offsetY = renderContentSize.Height - radiusY;
          break;
        default:
          throw new NotSupportedException();
        }
      } else {
        //if CustomControlSample is not stretched, renderContentSize.Height=textBox.RenderSize.Height
        offsetY = ChildTextBox.RenderSize.Height/2;
      }
      drawingContext.DrawEllipse(Brushes.LightGoldenrodYellow, null, new Point(offsetX, offsetY), radiusX, radiusY);
    }
    #endregion
  }
}
