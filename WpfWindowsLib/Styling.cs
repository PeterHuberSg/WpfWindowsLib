using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WpfWindowsLib {


  public static class Styling {

    //public static int DPI {
    //  get { return dpi; }
    //}
    //static int dpi = int.MinValue; //This line must be placed before any Pen creation so that it gets executed first


    #region Schedule
    //      --------

    //public static readonly Brush PraesenzBrush3 = createFrozenBrush(Color.FromArgb(0xA0, 0x80, 0xF0, 0xF0));
    //public static readonly Brush PraesenzBrush3Solid = createFrozenBrush(Color.FromRgb(0xC0, 0xF0, 0xF0));
    //public static readonly Brush PraesenzBrush2 = createFrozenBrush(Color.FromArgb(0xA0, 0x90, 0xF0, 0xF0));
    //public static readonly Brush PraesenzBrush1 = createFrozenBrush(Color.FromArgb(0xA0, 0x70, 0xD0, 0xD0));
    //public static readonly Brush PraesenzBrush0 = createFrozenBrush(Color.FromArgb(0xA0, 0x50, 0xB0, 0xB0));
    //public static readonly Brush PraesenzBrush = PraesenzBrush3;
    ////public static readonly Brush PraesenzBrush = createFrozenBrush(Color.FromArgb(0xA0, 0x30, 0x90, 0x90));
    //public static readonly Brush KundenTerminBrush = createFrozenBrush(Color.FromRgb(0xFF, 0xF4, 0x96));
    //public static readonly Brush KundenTerminMemberBrush = createFrozenBrush(Color.FromRgb(0xF0, 0xE0, 0x70));

    //public const int PaddingX = 2;
    //public const int PaddingY = 2;
    //public const int LineWidth = 1;
    //public const double LineWidthHalf = LineWidth / 2.0;
    //public const int PaddingLineX = 2*PaddingX + LineWidth;

    //public const int BorderWidth = 2;
    //public const double BorderWidthHalf = BorderWidth / 2.0;
    //public readonly static Pen PenBorder = createFrozenPen(Brushes.DimGray, BorderWidth);
    //public readonly static Pen PenGridDark = createFrozenPen(Brushes.Black, 0.8);
    //public readonly static Pen PenGridLight = createFrozenPen(Brushes.DimGray, 0.8);
    //public readonly static Brush WeekendBrush = createFrozenBrush(Color.FromRgb(0xf0, 0xf0, 0xf0));
    //public readonly static Brush LightGrayBgBrush = createFrozenBrush(Color.FromRgb(0xf0, 0xf0, 0xf0));
    //public readonly static Brush PauseLightBrush = createFrozenBrush(Color.FromRgb(0xd8, 0xd8, 0xd8));
    //public readonly static Brush PauseDarkBrush = createFrozenBrush(Color.FromRgb(0xc0, 0xc0, 0xc0));


    //private static Brush createFrozenBrush(Color color) {
    //  var brush = new SolidColorBrush(color);
    //  brush.Freeze();
    //  return brush;
    //}


    //private static Pen createFrozenPen(SolidColorBrush color, double borderWidth) {
    //  if (dpi==int.MinValue) {
    //    var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
    //    var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

    //    var dpiX = (int)dpiXProperty.GetValue(null, null);
    //    var dpiY = (int)dpiYProperty.GetValue(null, null);

    //    if (dpiX!=dpiY) {
    //      throw new Exception($"Der Monitor hat verschiedene DotPerInch für x {dpiX} und y {dpiY}.");
    //    }
    //    dpi = dpiX;
    //  }

    //  var adjustedWidth = borderWidth * 96 / dpi;
    //  var pen = new Pen(color, adjustedWidth);
    //  pen.Freeze();
    //  return pen;
    //}

    #endregion


    #region Forms
    //      -----

    public static readonly Brush RequiredBrush = Brushes.LightGoldenrodYellow;
    public static readonly Brush ErrorBrush = Brushes.PapayaWhip;

    public static readonly Brush HasChangedBackgroundBrush = Brushes.LightGreen;
    #endregion
  }
}
