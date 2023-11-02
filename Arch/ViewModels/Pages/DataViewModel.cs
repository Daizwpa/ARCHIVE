// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Arch.Extensions;
using Arch.Models;
using Arch.Services;
using Core.Models;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace Arch.ViewModels.Pages
{
    public partial class DataViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly ISnackbarService _snackbarService;
        private readonly Connection _connection;
        private readonly CachServices _cachServices;


        [ObservableProperty]
        private IList<Record> _records;


        [ObservableProperty]
        private Operation _selectedoperation;



        public DataViewModel(INavigationService navigationService, ISnackbarService snackbarService, CachServices cachServices , Connection connection) {

            _navigationService = navigationService;
            _snackbarService = snackbarService;
            _connection = connection;
            this._cachServices = cachServices;
            
        }

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo()
        {

            Refershe();

        }

        private void Refershe()
        {
            try
            {
                if (_cachServices.last_operation == null)
                    throw new Exception();

                Selectedoperation = _cachServices.last_operation.CopyObject();

                LoadRecords(Selectedoperation.Id);

            }
            catch
            {

                _snackbarService.Show("Error", "You have chose an operation!");
            }

        } 


        private  void LoadRecords(decimal operationID)
        {
            try
            {

                Records = _connection.Context.records.Where(s=> s.operation.Id == operationID).ToList()?? new List<Record>();

            }catch 
            {
                throw;
            }
        }



    }
}
