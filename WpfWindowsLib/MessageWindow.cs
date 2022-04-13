/**************************************************************************************

WpfWindowsLib.MessageWindow
===========================

Replacement for MessageBox

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


  public class MessageWindow: Window {

    #region Constructor
    //      -----------

    public static MessageWindow Show(Window ownerWindow, string message, Action? refreshOwner = null) {
      var window = new MessageWindow(message, refreshOwner) { Owner = ownerWindow };
      window.Show();
      return window;
    }


    Action? refreshOwner;


    public MessageWindow(string message, Action? refreshOwner) {
      this.refreshOwner = refreshOwner;

      var stackPanel = new StackPanel();
      Content = stackPanel;

      var textBox = new TextBox {
        IsReadOnly = true,
        Text = message
      };
      stackPanel.Children.Add(textBox);

      var okButton = new Button {
        Content = "_Ok"
      };
      stackPanel.Children.Add(okButton);
      okButton.Click += okButton_Click;

      Closed += messageWindow_Closed;
    }
    #endregion


    #region Events
    //      ------


    private void okButton_Click(object sender, RoutedEventArgs e) {
      Close();
    }


    private void messageWindow_Closed(object? sender, EventArgs e) {
      refreshOwner?.Invoke();
    }
    #endregion
  }
}
