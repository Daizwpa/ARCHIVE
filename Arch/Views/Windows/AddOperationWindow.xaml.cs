using Arch.ViewModels.Windows;
using Arch.Views.Pages;
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

namespace Arch.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddOperationWindow.xaml
    /// </summary>
    public partial class AddOperationWindow 
    {
        private AddOperationWindowViewModel ViewModel { get; } = new AddOperationWindowViewModel();
        public Page CurrentPage { get; set; }
        
        public AddOperationWindow(decimal id, Page page)
        {
            CurrentPage = page;
            this.DataContext = this;
            InitializeComponent();

        }


    }
}
