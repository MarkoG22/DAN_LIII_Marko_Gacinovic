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
    class CreateManagerViewModel : ViewModelBase
    {
        CreateManagerView createManager;

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

        private tblDegree degree;
        public tblDegree Degree
        {
            get { return degree; }
            set { degree = value; OnPropertyChanged("Degree"); }
        }

        private List<tblDegree> degreeList;
        public List<tblDegree> DegreeList
        {
            get { return degreeList; }
            set { degreeList = value; OnPropertyChanged("DegreeList"); }
        }

        public CreateManagerViewModel(CreateManagerView createManagerOpen)
        {
            createManager = createManagerOpen;
            DegreeList = GetAllDegree();
            user = new tblAll();
            manager = new tblManager();
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
                || String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Pasword)                
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
                    tblManager newManager = new tblManager();

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

                    newManager.ManagerFlor = manager.ManagerFlor;
                    newManager.Experience = manager.Experience;
                    newManager.SSS = degree.DegreeName;
                    newManager.ManagerID = manager.ManagerID;
                    newManager.AllIDman = user.All_ID;

                    context.tblAlls.Add(newUser);
                    context.tblManagers.Add(newManager);
                    context.SaveChanges();
                }
                createManager.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong inputs, please check your inputs or try again.");
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
                createManager.Close();
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

        private List<tblDegree> GetAllDegree()
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    List<tblDegree> list = new List<tblDegree>();
                    list = (from x in context.tblDegrees select x).ToList();
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
