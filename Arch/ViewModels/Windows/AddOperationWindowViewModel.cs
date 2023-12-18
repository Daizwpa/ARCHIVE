using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.ViewModels.Windows
{
    public partial class AddOperationWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _applicationTitle = "Add";


        public AddOperationWindowViewModel()
        {


        }

    }
}
