using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WpfWindowsLib {


  /// <summary>
  /// Draws glyphs to a DrawingContext. From the font information in the constructor, GlyphDrawer creates and stores the GlyphTypeface, which
  /// is used everytime for the drawing of the string.
  /// </summary>
  public class GlyphDrawer {

    readonly Typeface typeface;


    public GlyphTypeface GlyphTypeface {
      get { return glyphTypeface; }
    }
    readonly GlyphTypeface glyphTypeface;


    public float PixelsPerDip { get; }


    public GlyphDrawer(FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double pixelsPerDip) {
      typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
      if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
        throw new InvalidOperationException("No glyphtypeface found");

      PixelsPerDip = (float)pixelsPerDip;
    }


    /// <summary>
    /// Writes a string to a DrawingContext, using the GlyphTypeface stored in the GlyphDrawer.
    /// </summary>
    /// <param name="drawingContext"></param>
    /// <param name="origin"></param>
    /// <param name="text"></param>
    /// <param name="size">same unit like FontSize: (em)</param>
    /// <param name="brush"></param>
    public void Write(DrawingContext drawingContext, Point origin, string text, double size, Brush brush) {
      if (string.IsNullOrEmpty(text)) return;

      ushort[] glyphIndexes = new ushort[text.Length];
      double[] advanceWidths = new double[text.Length];

      double totalWidth = 0;

      for (int charIndex = 0; charIndex<text.Length; charIndex++) {
        ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[charIndex]];
        glyphIndexes[charIndex] = glyphIndex;

        double width = glyphTypeface.AdvanceWidths[glyphIndex] * size;
        advanceWidths[charIndex] = width;

        totalWidth += width;
      }

      GlyphRun glyphRun = new GlyphRun(glyphTypeface, 0, false, size, PixelsPerDip, glyphIndexes, origin, advanceWidths, null, null, null, null, null, null);

      drawingContext.DrawGlyphRun(brush, glyphRun);
    }


    /// <summary>
    /// Writes a string to a DrawingContext, using the GlyphTypeface stored in the GlyphDrawer. The text will be right alligned. The
    /// last character will be at Origin, all other characters in front.
    /// </summary>
    /// <param name="drawingContext"></param>
    /// <param name="origin"></param>
    /// <param name="text"></param>
    /// <param name="size">same unit like FontSize: (em)</param>
    /// <param name="brush"></param>
    public void WriteRightAligned(DrawingContext drawingContext, Point origin, string text, double size, Brush brush) {
      if (string.IsNullOrEmpty(text)) return;

      ushort[] glyphIndexes = new ushort[text.Length];
      double[] advanceWidths = new double[text.Length];

      double totalWidth = 0;

      for (int charIndex = 0; charIndex<text.Length; charIndex++) {
        ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[charIndex]];
        glyphIndexes[charIndex] = glyphIndex;

        double width = glyphTypeface.AdvanceWidths[glyphIndex] * size;
        advanceWidths[charIndex] = width;

        totalWidth += width;
      }

      Point newOrigin = new Point(origin.X - totalWidth, origin.Y);
      GlyphRun glyphRun = new GlyphRun(glyphTypeface, 0, false, size, PixelsPerDip, glyphIndexes, newOrigin, advanceWidths, null, null, null, null, null, null);

      drawingContext.DrawGlyphRun(brush, glyphRun);
    }


    /// <summary>
    /// Returns the length of the text using the GlyphTypeface stored in the GlyphDrawer. 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="size">same unit like FontSize: (em)</param>
    /// <returns></returns>
    public double GetLength(string text, double size) {
      double length = 0;

      for (int charIndex = 0; charIndex<text.Length; charIndex++) {
        ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[charIndex]];
        double width = glyphTypeface.AdvanceWidths[glyphIndex] * size;
        length += width;
      }
      return length;
    }


    public double GetMaxLength(IEnumerable<string> texte, double size) {
      var maxLength = 0.0;
      foreach (var text in texte) {
        double length = 0;
        for (int charIndex = 0; charIndex<text.Length; charIndex++) {
          ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[charIndex]];
          double width = glyphTypeface.AdvanceWidths[glyphIndex] * size;
          length += width;
        }
        maxLength = Math.Max(maxLength, length);
      }
      return maxLength;
    }


    public Size MeasureString(string text, double size) {
      var formattedText = new FormattedText(text, CultureInfo.CurrentUICulture,
                                              FlowDirection.LeftToRight,
                                              typeface,
                                              size,
                                              Brushes.Black,
                                              PixelsPerDip);

      return new Size(formattedText.Width, formattedText.Height);
    }
  }
}