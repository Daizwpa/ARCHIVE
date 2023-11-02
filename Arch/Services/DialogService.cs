using Arch.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Arch.Services
{
    public class DialogService<T> where T : Page
    {
        private T page;
        private AddOperationWindow window;

        public DialogService(T page) {
            this.page = page;
        }
        
        public void Show(object? data = null) 
        {
            decimal? id = data as decimal?;
            window = new AddOperationWindow(id ?? -1, page);

            window.ShowDialog();
        }

        public void Exist()
        {
            window.Close();
        }
    }
}
