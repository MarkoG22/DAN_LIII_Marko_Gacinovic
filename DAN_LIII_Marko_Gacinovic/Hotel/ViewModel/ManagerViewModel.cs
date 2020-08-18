using Hotel.Commands;
using Hotel.Models;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class ManagerViewModel : ViewModelBase
    {
        ManagerView managerView;

        #region Properties
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
        #endregion

        // constructor
        public ManagerViewModel(ManagerView managerViewOpen, tblManager ManagerToPass)
        {
            manager = ManagerToPass;
            managerView = managerViewOpen;
            EmployeeList = GetAllEmployee();
        }

        #region Commands
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

        /// <summary>
        /// method for calculating the salary
        /// </summary>
        private void CalculateSalaryExecute()
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    tblEmploye employee = (from y in context.tblEmployes where y.EmployeFlor == manager.ManagerFlor select y).FirstOrDefault();

                    string degree = manager.SSS;
                    double num = 0;

                    // switch loop for degrees
                    switch (degree)
                    {
                        case "I": num = 1; break;
                        case "II": num = 2; break;
                        case "III": num = 3; break;
                        case "IV": num = 4; break;
                        case "V": num = 5; break;
                        case "VI": num = 6; break;
                        case "VII": num = 7; break;
                        default:
                            break;
                    }

                    double i = 0.75 * (double)manager.Experience;
                    double s = 0.15 * num;
                    double p;

                    if (employee.Gender == "M")
                    {
                        p = 0.12;
                    }
                    else
                    {
                        p = 0.15;
                    }

                    Random rnd = new Random();
                    int x = rnd.Next(2, 1000);

                    // calculating the salary
                    double salary = 1000 * i * s * p + x;

                    employee.Salary = (int)salary;

                    context.SaveChanges();

                    // refreshing the list
                    EmployeeList = GetAllEmployee();

                    MessageBox.Show("The salary calculated successfully.");
                }  
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry, the salary can not be calculated.");
            }
        }
        #endregion

        /// <summary>
        /// method for getting all employees to the list
        /// </summary>
        /// <returns></returns>
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
