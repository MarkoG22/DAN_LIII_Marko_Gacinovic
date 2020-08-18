using Hotel.Models;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ViewModel
{
    class CalculateSalaryViewModel : ViewModelBase
    {
        CalculateSalaryView calculateSalary;

        private tblEmploye employee;
        public tblEmploye Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged("Employee"); }
        }

        public CalculateSalaryViewModel(CalculateSalaryView calculateSalaryOpen, tblEmploye employeeToPass)
        {
            calculateSalary = calculateSalaryOpen;
            employee = employeeToPass;
        }
    }
}
