// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Arch.Extensions;
using Arch.Models;
using Arch.Services;
using Arch.Views.Pages;
using Core.Files;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media;
using Wpf.Ui.Controls;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Arch.ViewModels.Pages
{
    public partial class DataViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly ISnackbarService _snackbarService;
        private readonly Connection _connection;
        private readonly CachServices _cachServices;
        private readonly DialogService<AddRecordPage> _dialogService;

        [ObservableProperty]
        private IList<Record> _records;


        [ObservableProperty]
        private Operation _selectedoperation;

        [ObservableProperty]
        private Record? _selectedrecord;

        [ObservableProperty]
        private string query = string.Empty;


        public DataViewModel(INavigationService navigationService, ISnackbarService snackbarService, CachServices cachServices , Connection connection, DialogService<AddRecordPage> dialogService) {

            _navigationService = navigationService;
            _snackbarService = snackbarService;
            _connection = connection;
            this._cachServices = cachServices;
            this._dialogService = dialogService;
            
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
                _cachServices.last_record = null;

                LoadRecords(Selectedoperation.Id);
                _cachServices.last_record = Records.FirstOrDefault();
                Selectedrecord = _cachServices.last_record;

                OnPropertyChanged(nameof(Selectedoperation));
                OnPropertyChanged(nameof(Selectedrecord));

            }
            catch
            {

                _snackbarService.Show("Error", "You have chose an operation!");
            }

        }


        partial void OnQueryChanged(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    Refershe();
                }
                else
                {
                    Records = _connection.Context.records.Where(s => s.Name.Contains(value) && s.operation.Id == Selectedoperation.Id).ToList();

                }
            }
            catch(Exception ex)
            {
                _snackbarService.Show("Error",ex.Message);


            }




        }

        private  void LoadRecords(int operationID)
        {
            try
            {
               
                Records = _connection.Context.records.AsNoTracking().Where(s=> s.operation.Id == operationID)?.ToList()?? new List<Record>();

                OnPropertyChanged(nameof(Records));

            }catch 
            {
                _snackbarService.Show("Error", "The list is empty!");
            }
        }


        [RelayCommand]
        private void openWindow()
        {
            var saver = _cachServices.last_record;

            try
            {
                if (Selectedoperation.Id == -1)
                    throw new Exception("You have to choose the operation before!");
                _cachServices.last_record = null;
                _dialogService.Show();

                

            }
            catch (Exception ex)
            {
                _snackbarService.Show("Error", ex.Message);

            }
            finally
            {
                _cachServices.last_record = saver;
                Refershe();

            }


        }

        [RelayCommand]
        private void delete()
        {
            try
            {
                if(Selectedrecord == null)
                    throw new Exception("You have select record!");

                _connection.Context.records.Remove(Selectedrecord);
                _connection.Context.SaveChanges();
                Refershe();
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

                _cachServices.last_record = Selectedrecord ?? throw new Exception("You have select record!");
                _dialogService.Show();
                Refershe();

            }
            catch (Exception ex)
            {
                _snackbarService.Show("Error", ex.Message);

            }
        }

        [RelayCommand]
        private void open_pdf()
        {
            try
            {
                string path = Selectedrecord?.FilePath ?? throw new  Exception("The path is Empty");
                // _snackbarService.Show("Info", path);
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                _snackbarService.Show("Error", ex.Message);
            }
        }

        partial void OnSelectedrecordChanged(Record? value)
        {
            _cachServices.last_record = Selectedrecord;
        }
   


    }
}
