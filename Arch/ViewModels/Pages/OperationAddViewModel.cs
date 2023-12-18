using Arch.Models;
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
        private readonly CachServices _cachServices;

        [ObservableProperty]
        private Operation _newOperation = new Operation();

        [ObservableProperty]
        private bool isDomand = true;
        [ObservableProperty]
        private bool isConsultation = false;


        public Action Exist { get; set; }


        public OperationAddViewModel(Connection connection, IContentDialogService contentDialogService, CachServices cachServices) {
            this.connction = connection;
            this._contentDialogService = contentDialogService;
            this._cachServices = cachServices;
            


        }


        public void Clean()
        {

            try
            {
                if(_cachServices.last_operation == null) {
                   this.NewOperation = new Operation();

                     
                    this.NewOperation.Id = (connction.Context.operations.Count() !=0)? connction.Context.operations.Max(o => o.Id) + 1: 1;
                    
                    

                    this.OnPropertyChanged(nameof(NewOperation));   

                }
                else
                {
                    this.NewOperation = connction.Context.operations.Where(s=> s.Id == _cachServices.last_operation.Id).FirstOrDefault()?? throw  new Exception ("There is no such this operation");
                }



            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }



        [RelayCommand]
        private void save()
        {
            try
            {
                if(_cachServices.last_operation == null)
                {


                    ReadTypeOperation();
                    connction.Context.operations.Add(NewOperation);
                    connction.Context.SaveChanges();

                }
                else
                {
                    ReadTypeOperation();
                    connction.Context.operations.Update(NewOperation);
                    connction.Context.SaveChanges();
                }

                System.Windows.MessageBox.Show("Done!");
                Clean();

                Exist?.Invoke();

                


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
