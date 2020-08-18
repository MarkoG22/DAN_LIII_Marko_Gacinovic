using Hotel.Models;
using Hotel.ViewModel;
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

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for CalculateSalaryView.xaml
    /// </summary>
    public partial class CalculateSalaryView : Window
    {
        public CalculateSalaryView(tblEmploye employee)
        {
            InitializeComponent();
            this.DataContext = new CalculateSalaryViewModel(this, employee);
        }
    }
}
