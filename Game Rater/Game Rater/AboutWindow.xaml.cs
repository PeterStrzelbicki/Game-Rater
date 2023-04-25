/*
  File Name:    AboutWindow.xaml.cs
  Project:      Game Rater
  By:           Peter Strzelbicki
  Date:         April 24, 2023
  Description:  This file displays an "About" window that shows relevant information about the Game Rater application.
*/

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
using System.Windows.Shapes;

namespace Game_Rater
{
    /*
    class        : AboutWindow
    description  : This class contains all the components for an "About" window.
    */
    public partial class AboutWindow : Window
    {

        /*
        function     : AboutWindow()
        description  : This is the constructor for the AboutWindow class.
        */
        public AboutWindow()
        {
            InitializeComponent();
        }


        /*
        function     : okBtn_Click()
        description  : This function closes the window when the Close button is clicked.
        */
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}