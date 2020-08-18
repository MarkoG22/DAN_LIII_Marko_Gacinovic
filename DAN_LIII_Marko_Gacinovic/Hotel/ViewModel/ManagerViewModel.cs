using Hotel.Commands;
using Hotel.Models;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class ManagerViewModel : ViewModelBase
    {
        ManagerView managerView;

        private tblAll user;
        public tblAll User
        {
            get { return user; }
            set { user = value; OnPropertyChanged("User"); }
        }

        private tblManager manager;
        public tblManager Manager
        {
            get { return manager; }
            set { manager = value; OnPropertyChanged("Manager"); }
        }

        private tblEmploye employee;
        public tblEmploye Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged("Employee"); }
        }

        private List<tblEmploye> employeeList;
        public List<tblEmploye> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; OnPropertyChanged("EmployeeList"); }
        }

        public ManagerViewModel(ManagerView managerViewOpen, tblManager ManagerToPass)
        {
            manager = ManagerToPass;
            managerView = managerViewOpen;
            EmployeeList = GetAllEmployee();
        }

        // commands
        private ICommand calculateSalary;
        public ICommand CalculateSalary
        {
            get
            {
                if (calculateSalary == null)
                {
                    calculateSalary = new RelayCommand(param => CalculateSalaryExecute(), param => CanCalculateSalaryExecute());
                }
                return calculateSalary;
            }
        }

        private bool CanCalculateSalaryExecute()
        {
            return true;
        }

        private void CalculateSalaryExecute()
        {
            try
            {
                CalculateSalaryView salary = new CalculateSalaryView(employee);
                salary.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        private List<tblEmploye> GetAllEmployee()
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    List<tblEmploye> list = new List<tblEmploye>();
                    list = (from x in context.tblEmployes where x.EmployeFlor == manager.ManagerFlor select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
