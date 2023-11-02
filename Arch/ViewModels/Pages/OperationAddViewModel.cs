using Arch.Services;
using Arch.Views.Pages;
using Core.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace Arch.ViewModels.Pages
{
    public partial class OperationAddViewModel : ObservableObject
    {

        private readonly Connection connction;
        private readonly IContentDialogService _contentDialogService;


        [ObservableProperty]
        private Operation _newOperation;

        [ObservableProperty]
        private bool isDomand = true;
        [ObservableProperty]
        private bool isConsultation = false;

        public OperationAddViewModel(Connection connection, IContentDialogService contentDialogService) {
            this.connction = connection;
            this._contentDialogService = contentDialogService;
            Initial();


        }



        private void Initial()
        {

            try
            {
   
                this.NewOperation = new Operation();
                this.NewOperation.Id = connction.Context.operations.Max(o => o.Id) + 1;

            }
            catch(Exception ex)
            {
                _contentDialogService.ShowAlertAsync("Error", ex.Message, "OK");
            }

        }



        [RelayCommand]
        private void save()
        {
            try
            {

                ReadTypeOperation();
                connction.Context.operations.Add(NewOperation);
                connction.Context.SaveChanges();
                Initial();
                

            }
            catch(Exception ex)
            {
                _contentDialogService.ShowAlertAsync("Error", ex.Message, "OK");
            }
            


        }


        private void ReadTypeOperation()
        {
            if (IsDomand)
            {
                NewOperation.Type = "Demand";
            }
            else
            {
                NewOperation.Type = "Consultation";

            }
        }

    }
}
