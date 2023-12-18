using Arch.ViewModels.Pages;
using Arch.ViewModels.Windows;
using Arch.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Arch.Services
{
    public class DialogService<T>  where T : Page
    {
        private T page;
        private AddOperationWindow window;
        private AddOperationWindowViewModel viewmodel;

        public DialogService(T page, AddOperationWindowViewModel viewmodel)
        {
            this.page = page;
            this.viewmodel = viewmodel;
        }

        public void Show(object? data = null) 
        {
            decimal? id = data as decimal?;


           

            window = new AddOperationWindow(id ?? -1, page, viewmodel);

            window.ShowDialog();
            window.Close();


        }



        private void Close()
        { 
            window.Close();
        } 


    }
}
