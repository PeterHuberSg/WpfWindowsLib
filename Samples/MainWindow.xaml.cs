/**************************************************************************************

Samples.MainWindow
==================

Start Window giving access to a control tests

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using System.Windows;


namespace Samples {


  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: Window {
    public MainWindow() {
      InitializeComponent();

      CheckedTextBoxButton.Click += checkedTextBoxButton_Click;
      DecimalTextBoxButton.Click += decimalTextBoxButton_Click;
      IntTextBoxButton.Click += intTextBoxButton_Click;
      EmailTextBoxButton.Click += emailTextBoxButton_Click;
      PhoneTextBoxButton.Click += phoneTextBoxButton_Click;
    }


    private void checkedTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new CheckedTextBoxWindow { Owner=this }.Show();
    }


    private void decimalTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new DecimalTextBoxWindow { Owner=this }.Show();
    }


    private void intTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new IntTextBoxWindow { Owner=this }.Show();
    }


    private void emailTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new EmailTextBoxWindow { Owner=this }.Show();
    }


    private void phoneTextBoxButton_Click(object sender, RoutedEventArgs e) {
      new EmailTextBoxWindow { Owner=this }.Show();
    }
  }
}
