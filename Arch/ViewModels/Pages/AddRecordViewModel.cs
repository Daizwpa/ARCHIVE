
using Arch.Models;
using Arch.Services;
using Core.Models;
using Microsoft.Win32;
using System.IO;

namespace Arch.ViewModels.Pages
{
    public partial class AddRecordViewModel : ObservableObject
    {

        private readonly Connection connction;
        private readonly IContentDialogService _contentDialogService;
        private readonly CachServices _cachServices;

        [ObservableProperty]
        private Operation selectedoperation = new Operation();


        [ObservableProperty]
        private Record newrecord = new Record();




        public Action Exist { get; set; }


        public AddRecordViewModel(Connection connection, IContentDialogService contentDialogService, CachServices cacheservice)
        {
            this.connction = connection;
            this._contentDialogService = contentDialogService;

            this._cachServices = cacheservice;

        }


        public void Clean()
        {

            try
            {
                if (_cachServices.last_operation == null)
                    throw new Exception("You have not selected operation yet!");
                this.Selectedoperation = connction.Context.operations.Where(s=>s.Id== _cachServices.last_operation.Id).FirstOrDefault() ?? throw new Exception("You have selected operation");

                
                if (_cachServices.last_record == null)
                {
                    Newrecord = new Record();

                    
                    this.Newrecord.Id = (connction.Context.records.Count() != 0)? connction.Context.records.Max(o => o.Id) + 1 : 1;

                    
                    this.Newrecord.operation = this.Selectedoperation;

                    this.Selectedoperation.Records.Add(this.Newrecord);
                }
                else
                {
                    this.Newrecord = connction.Context.records.Where(s => s.Id == _cachServices.last_record.Id).FirstOrDefault() ?? throw new Exception("there is no record like this");

                }
                this.OnPropertyChanged(nameof(Newrecord));
                this.OnPropertyChanged(nameof(Selectedoperation));


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        [RelayCommand]
        private void uploadFile()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    //InitialDirectory = @"D:\",
                    Title = "Browse Text Files",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "Pdf",
                    Filter = "Pdf Files|*.pdf",
                    FilterIndex = 2,
                    RestoreDirectory = true,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };

                if (openFileDialog1.ShowDialog() == true)
                {
                    Newrecord.FilePath = openFileDialog1.FileName;
                    
                    OnPropertyChanged(nameof(Newrecord));
                }

            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);

            }
        }



        [RelayCommand]
        private void save()
        {
            try
            {
                Movefile(Newrecord.FilePath);
                
                if (_cachServices.last_record == null)
                {

                    connction.Context.Add<Record>(Newrecord);
                    connction.Context.Update<Operation>(Selectedoperation);
                    connction.Context.SaveChanges();

                }
                else
                {
                    connction.Context.Update<Record>(Newrecord);
                    connction.Context.SaveChanges();
                }
                System.Windows.MessageBox.Show("Done!");
                Exist?.Invoke();




            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }


        private void Movefile(string currentPath)
        {
            try
            {
                string patharchive = AppConfig.GetInstanse().DatabaseFilePath;
                string fullNewPath = Path.Combine(patharchive, Newrecord.Name + Newrecord.operation.Id + Newrecord.Id + GetFileName(currentPath));

                if (currentPath != fullNewPath)
                {
                    File.Copy(currentPath, fullNewPath);
                    Newrecord.FilePath = fullNewPath;    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private string GetFileName(string fullPath)
        {
            
            string[] componentOfPath = fullPath.Split('\\');

            return componentOfPath.Last();
        }
    }
}
