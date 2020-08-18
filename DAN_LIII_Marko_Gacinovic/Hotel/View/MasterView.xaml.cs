using Hotel.ViewModel;

using System.Windows;


namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for MasterView.xaml
    /// </summary>
    public partial class MasterView : Window
    {
        public MasterView()
        {
            InitializeComponent();
            this.DataContext = new MasterViewModel(this);
        }
    }
}
