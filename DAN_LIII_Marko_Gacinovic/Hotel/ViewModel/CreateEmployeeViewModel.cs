using Hotel.Commands;
using Hotel.Models;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class CreateEmployeeViewModel : ViewModelBase
    {
        CreateEmployeeView employeeView;

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

        public CreateEmployeeViewModel(CreateEmployeeView employeeOpen)
        {
            employeeView = employeeOpen;
            EngagmentList = GetAllEngagment();
            user = new tblAll();
            employee = new tblEmploye();
        }

        // commands
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

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.Surname)
                || String.IsNullOrEmpty(user.Email)  || String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Pasword)
                || String.IsNullOrEmpty(employee.Gender) || String.IsNullOrEmpty(employee.Citizenship) || String.IsNullOrEmpty(employee.Engagment)
                || !user.FirstName.All(Char.IsLetter) || !user.Surname.All(Char.IsLetter))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

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

                    newEmployee.EmployeFlor = employee.EmployeFlor;                    

                    string sex = employee.Gender.ToUpper();

                    // gender validation
                    if ((sex == "M" || sex == "Z" || sex == "X" || sex == "N"))
                    {
                        newEmployee.Gender = sex;
                    }
                    else
                    {
                        MessageBox.Show("Wrong Gender input, please enter M, Z, X or N.");
                    }

                    newEmployee.Citizenship = employee.Citizenship;
                    newEmployee.Engagment = employee.Engagment;
                    newEmployee.EmployeID = employee.EmployeID;
                    newEmployee.AllIDemp = user.All_ID;

                    context.tblAlls.Add(newUser);
                    context.tblEmployes.Add(newEmployee);
                    context.SaveChanges();                    
                }
                employeeView.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong inputs, please check your inputs or try again.");
            }
        }

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

    }
}
