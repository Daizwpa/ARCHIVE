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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using Arch.ViewModels.Pages;

namespace Arch.Views.Pages
{
    /// <summary>
    /// Interaction logic for OperationAddPage.xaml
    /// </summary>
    public partial class OperationAddPage : Page
    {
        public OperationAddViewModel ViewModel { get; }

        public OperationAddPage(OperationAddViewModel ViewModel )
            
        {
            this.ViewModel = ViewModel;
            this.DataContext = this;
            InitializeComponent();
        }

    }
}
