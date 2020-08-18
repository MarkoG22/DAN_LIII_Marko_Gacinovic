using Hotel.ViewModel;
using System;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Windows;

using System.Windows.Input;


namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for CreateEmployeeView.xaml
    /// </summary>
    public partial class CreateEmployeeView : Window
    {
        public CreateEmployeeView()
        {
            InitializeComponent();
            this.DataContext = new CreateEmployeeViewModel(this);
        }

        private void NumbersTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LettersValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z ]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
