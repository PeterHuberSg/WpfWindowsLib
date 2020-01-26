using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControlSample {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: Window {
    public MainWindow() {
      InitializeComponent();

      TestCustomControlSample.FontSize = FontSizeTextBox.IntValue??20;
      FontSizeTextBox.LostFocus += fontSizeTextBox_LostFocus;
      FontsizeScrollBar.Value = FontSizeTextBox.IntValue??20;
      FontsizeScrollBar.Scroll += fontsizeScrollBar_Scroll;

      HorizontalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Left", Tag=HorizontalAlignment.Left, IsSelected=true });
      HorizontalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Center", Tag=HorizontalAlignment.Center });
      HorizontalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Right", Tag=HorizontalAlignment.Right });
      HorizontalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Stretch", Tag=HorizontalAlignment.Stretch });
      TestCustomControlSample.HorizontalAlignment = HorizontalAlignment.Left;
      HorizontalAlignmentComboBox.SelectionChanged += horizontalAlignmentComboBox_SelectionChanged;

      VerticalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Top", Tag=VerticalAlignment.Top, IsSelected=true });
      VerticalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Center", Tag=VerticalAlignment.Center });
      VerticalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Bottom", Tag=VerticalAlignment.Bottom });
      VerticalAlignmentComboBox.Items.Add(new ComboBoxItem { Content= "Stretch", Tag=VerticalAlignment.Stretch });
      TestCustomControlSample.VerticalAlignment = VerticalAlignment.Top;
      VerticalAlignmentComboBox.SelectionChanged += verticalAlignmentComboBox_SelectionChanged;
    }


    private void fontSizeTextBox_LostFocus(object sender, RoutedEventArgs e) {
      var newValue = FontSizeTextBox.IntValue??20;
      //if (newValue) {

      //}
      TestCustomControlSample.FontSize = newValue;
      FontsizeScrollBar.Value = newValue;
    }


    private void fontsizeScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e) {
      FontSizeTextBox.IntValue = (int)FontsizeScrollBar.Value;
      TestCustomControlSample.FontSize = FontSizeTextBox.IntValue.Value;
    }


    private void horizontalAlignmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
      TestCustomControlSample.HorizontalAlignment = (HorizontalAlignment)((ComboBoxItem)HorizontalAlignmentComboBox.SelectedItem).Tag;
    }

    private void verticalAlignmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
      TestCustomControlSample.VerticalAlignment = (VerticalAlignment)((ComboBoxItem)VerticalAlignmentComboBox.SelectedItem).Tag;
    }
  }
}
