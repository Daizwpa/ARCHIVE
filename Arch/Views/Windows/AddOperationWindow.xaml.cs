using Arch.ViewModels.Windows;
using Arch.Views.Pages;
using System;
using System.CodeDom;
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
        private AddOperationWindowViewModel ViewModel { get; }
        public Page CurrentPage { get; set; }
        
        public AddOperationWindow(decimal id, Page page, AddOperationWindowViewModel viewModel )
        {
            this.ViewModel = viewModel;

            if(page.GetType() == typeof(OperationAddPage))
            {
                OperationAddPage x = page as OperationAddPage;
                x.ViewModel.Clean();

                x.ViewModel.Exist = ()=>this.Close();
            }
            else
            {
                AddRecordPage x = page as AddRecordPage;
                x.ViewModel.Clean();
                x.ViewModel.Exist = () => this.Close();
            }

            CurrentPage = page;
            this.DataContext = this;
            InitializeComponent();

        }


    }
}
