using Hotel.Models;
using Hotel.ViewModel;

using System.Windows;

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public ManagerView(tblManager manager)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this, manager);
        }
    }
}
