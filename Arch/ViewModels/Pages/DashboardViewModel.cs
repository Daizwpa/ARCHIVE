// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Arch.Extensions;
using Arch.Models;
using Arch.Services;
using Arch.Views.Pages;
using Arch.Views.Windows;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.CodeDom;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Arch.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {


        private readonly DialogService<OperationAddPage> _dialogService;
        private readonly Connection _connection;
        private readonly CachServices _cachServices;
        private readonly ISnackbarService _snackbarService;

        [ObservableProperty]
        private Operation _selected_operation;

        [ObservableProperty]
        private string _query = string.Empty;

        [ObservableProperty]
        private IList<Operation> _operations = new ObservableCollection<Operation>();


        public DashboardViewModel(
            DialogService<OperationAddPage> dialogService,
            CachServices cacheservice,
            Connection connection, 
            ISnackbarService snackbarService)
        {
            this._dialogService = dialogService;
            this._connection = connection;
            this._cachServices = cacheservice;
            this._snackbarService = snackbarService; 
            loadData();


        }


        [RelayCommand]
        private void openWindow()
        {
            var saver = _cachServices.last_operation;

            try
            {

                _cachServices.last_operation = null;
                _dialogService.Show();


            }
            catch (Exception ex)
            {
                _snackbarService.Show("Error", ex.Message);

            }
            finally
            {
                _cachServices.last_operation = saver;
                loadData();
            }


        }

        [RelayCommand]
        private void delete()
        {
            try
            {
                if (Selected_operation == null)
                    throw new Exception("You have choose the operation before!");
                _connection.Context.operations.Remove(Selected_operation);
                _connection.Context.SaveChanges();
                loadData();
                _snackbarService.Show("Done", "the operation have removed successfully");


            }
            catch (Exception ex)
            {
                _snackbarService.Show("Error", ex.Message);
            }

        }

        [RelayCommand]
        private void edit()
        {
            try
            {
                if (Selected_operation == null)
                    throw new Exception("You have choose the operation before!");

                _cachServices.last_operation  = Selected_operation;
                _dialogService.Show();
                loadData();

            }
            catch(Exception ex)
            {
                _snackbarService.Show("Error", ex.Message);

            }
        }


        partial void OnSelected_operationChanged(Operation value)
        {
            _cachServices.last_operation = Selected_operation.CopyObject();
        }

        partial void OnQueryChanged(string value)
        {
             if (string.IsNullOrEmpty(value))
             {
               loadData();
             }
            else
            {
                Operations = _connection.Context.operations.AsNoTracking().Where(s=> s.Name.Contains(value)).ToList();
            }
        }



        private void loadData()
        {
            Operations = _connection.Context.operations.AsNoTracking().ToList<Operation>() ?? new List<Operation>();
        }




    }
}
