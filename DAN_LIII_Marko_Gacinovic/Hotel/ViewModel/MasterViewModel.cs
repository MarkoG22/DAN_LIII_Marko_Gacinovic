using Hotel.Commands;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class MasterViewModel : ViewModelBase
    {
        MasterView masterView;

        public MasterViewModel(MasterView masterOpen)
        {
            masterView = masterOpen;
        }

        private ICommand createEmployee;
        public ICommand CreateEmployee
        {
            get
            {
                if (createEmployee == null)
                {
                    createEmployee = new RelayCommand(param => CreateEmployeeExecute(), param => CanCreateEmployeeExecute());
                }
                return createEmployee;
            }

        }

        private bool CanCreateEmployeeExecute()
        {
            return true;
        }

        /// <summary>
        /// method for opening the view for creating employee
        /// </summary>
        private void CreateEmployeeExecute()
        {
            CreateEmployeeView employee = new CreateEmployeeView();
            employee.ShowDialog();
        }

        private ICommand createManager;
        public ICommand CreateManager
        {
            get
            {
                if (createManager == null)
                {
                    createManager = new RelayCommand(param => CreateManagerExecute(), param => CanCreateManagerExecute());
                }
                return createManager;
            }

        }

        private bool CanCreateManagerExecute()
        {
            return true;
        }

        /// <summary>
        /// method for opening the view for creating manager
        /// </summary>
        private void CreateManagerExecute()
        {
            CreateManagerView manager = new CreateManagerView();
            manager.ShowDialog();
        }
    }
}
