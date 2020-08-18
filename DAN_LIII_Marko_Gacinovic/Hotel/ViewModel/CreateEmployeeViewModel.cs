using Hotel.Commands;
using Hotel.Models;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class CreateEmployeeViewModel : ViewModelBase
    {
        CreateEmployeeView employeeView;

        #region Properties
        private tblAll user;
        public tblAll User
        {
            get { return user; }
            set { user = value; OnPropertyChanged("User"); }
        }

        private tblEmploye employee;
        public tblEmploye Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged("Employee"); }
        }

        private tblEngagment engagment;
        public tblEngagment Engagment
        {
            get { return engagment; }
            set { engagment = value; OnPropertyChanged("Engagment"); }
        }

        private List<tblEngagment> engagmentList;
        public List<tblEngagment> EngagmentList
        {
            get { return engagmentList; }
            set { engagmentList = value; OnPropertyChanged("EngagmentList"); }
        }

        private tblManager manager;
        public tblManager Manager
        {
            get { return manager; }
            set { manager = value; OnPropertyChanged("Manager"); }
        }

        private List<tblManager> managerList;
        public List<tblManager> ManagerList
        {
            get { return managerList; }
            set { managerList = value; OnPropertyChanged("ManagerList"); }
        }
        #endregion

        // constructor
        public CreateEmployeeViewModel(CreateEmployeeView employeeOpen)
        {
            employeeView = employeeOpen;
            EngagmentList = GetAllEngagment();
            ManagerList = GetAllManager();
            user = new tblAll();
            employee = new tblEmploye();
        }

        #region Commands
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        /// <summary>
        /// method for disabling Save button
        /// </summary>
        /// <returns></returns>
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.Surname)
                || String.IsNullOrEmpty(user.Email)  || String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Pasword)
                || String.IsNullOrEmpty(employee.Gender) || String.IsNullOrEmpty(employee.Citizenship) 
                || !user.FirstName.All(Char.IsLetter) || !user.Surname.All(Char.IsLetter))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// method for creating new employee
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    tblAll newUser = new tblAll();
                    tblEmploye newEmployee = new tblEmploye();

                    // inputs and validations
                    if (user.FirstName.All(Char.IsLetter))
                    {
                        newUser.FirstName = user.FirstName;
                    }
                    else
                    {
                        MessageBox.Show("Wrong First Name input, please try again.");
                    }

                    if (user.Surname.All(Char.IsLetter))
                    {
                        newUser.Surname = user.Surname;
                    }
                    else
                    {
                        MessageBox.Show("Wrong Last Name input, please try again.");
                    }

                    newUser.DateOfBirth = user.DateOfBirth;
                    newUser.Email = user.Email;

                    newUser.Username = user.Username;

                    if (PasswordValidation(user.Pasword))
                    {
                        newUser.Pasword = user.Pasword;
                    }
                    else
                    {
                        MessageBox.Show("Wrong password. Password must have at least 8 characters.\n(1 upper char, 1 lower char, 1 number and 1 special char)\nPlease try again.");
                    }

                    user.All_ID = newUser.All_ID;

                    newEmployee.EmployeFlor = manager.ManagerFlor;                    

                    string sex = employee.Gender.ToUpper();

                    // gender validation
                    if ((sex == "M" || sex == "Z"))
                    {
                        newEmployee.Gender = sex;
                    }
                    else
                    {
                        MessageBox.Show("Wrong Gender input, please enter M or Z.");
                    }

                    newEmployee.Citizenship = employee.Citizenship;
                    newEmployee.Engagment = engagment.engName;
                    newEmployee.EmployeID = employee.EmployeID;
                    newEmployee.AllIDemp = user.All_ID;

                    // saving data to the database
                    context.tblAlls.Add(newUser);
                    context.tblEmployes.Add(newEmployee);
                    context.SaveChanges();

                    MessageBox.Show("The employee created successfully.");
                }
                employeeView.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong inputs, please check your inputs or try again. \nIf there are no floors, please create manager first.");
            }
        }

        // command for closing the window
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        /// <summary>
        /// method for closing the window
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                employeeView.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// method for getting all engagments to the list
        /// </summary>
        /// <returns></returns>
        private List<tblEngagment> GetAllEngagment()
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    List<tblEngagment> list = new List<tblEngagment>();
                    list = (from x in context.tblEngagments select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// method for getting all managers to the list
        /// </summary>
        /// <returns></returns>
        private List<tblManager> GetAllManager()
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    List<tblManager> list = new List<tblManager>();
                    list = (from x in context.tblManagers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// method for the password validation
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool PasswordValidation(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

            bool isValidated = regex.IsMatch(password);

            if (isValidated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
