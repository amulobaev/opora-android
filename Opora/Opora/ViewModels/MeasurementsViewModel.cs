using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Syncfusion.XlsIO;
using Xamarin.Forms;

using Opora.Models;
using Opora.Views;
using Opora.Domain;

namespace Opora.ViewModels
{
    public class MeasurementsViewModel : PageViewModel
    {
        private ICommand _addItemCommand;
        private Measurement _selectedItem;
        private readonly IRepository<Measurement, Guid> _measurementRepository;
        private ICommand _exportCommand;
        private readonly ObservableCollection<Measurement> _items = new ObservableCollection<Measurement>();

        /// <summary>
        /// Конструктор
        /// </summary>
        public MeasurementsViewModel(IRepository<Measurement, Guid> measurementRepository)
        {
            _measurementRepository = measurementRepository;

            Title = "Замеры";

            Update();

            MessagingCenter.Subscribe<EditMeasurementViewModel>(this, "UpdateMeasurements", obj =>
            {
                Update();
            });
        }

        public ObservableCollection<Measurement> Items
        {
            get { return _items; }
        }

        public Measurement SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;
                RaisePropertyChanged();

                if (SelectedItem == null)
                    return;
                // Здесь передать данные о замере
                Page.Navigation.PushAsync(new EditMeasurementPage());
                MessagingCenter.Send(this, "EditMeasurement", SelectedItem);

                // Manually deselect item
                SelectedItem = null;
            }
        }

        public Command LoadItemsCommand { get; set; }

        public ICommand AddItemCommand
        {
            get { return _addItemCommand ?? (_addItemCommand = new RelayCommand(AddItem)); }
        }

        public ICommand ExportCommand
        {
            get { return _exportCommand ?? (_exportCommand = new RelayCommand(Export)); }
        }

        private void Update()
        {
            IsBusy = true;

            Items.Clear();

            var items = _measurementRepository.GetItems().ToList();
            if (items.Any())
            {
                foreach (Measurement item in items)
                {
                    Items.Add(item);
                }
            }

            IsBusy = false;
        }

        private async void AddItem()
        {
            await Page.Navigation.PushAsync(new EditMeasurementPage());

            DateTime now = DateTime.Now;
            var measurement = new Measurement
            {
                Id = Guid.NewGuid(),
                CreatedAt = now,
                UpdatedAt = now
            };

            MessagingCenter.Send(this, "EditMeasurement", measurement);
        }

        private async void Export()
        {
            string filename = string.Format("export-{0:yyyy-MM-dd_hh-mm-ss}.xlsx", DateTime.Now);

            var data = await GetData();

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await App.Current.MainPage.DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    DependencyService.Get<ISave>().Save(filename, data);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert(ex.Message);
            }
        }

        private async Task<byte[]> GetData()
        {

            //Create an instance of ExcelEngine.
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2013.
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance.
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet["A1"].Text = "N опоры";
                worksheet["B1"].Text = "Марка опоры";
                worksheet["C1"].Text = "Координаты опоры";
                worksheet["D1"].Text = "Угол наклона";
                worksheet["E1"].Text = "Max наклон опоры";

                //Set the column width in points.
                //worksheet["A1:E1"].ColumnWidth = 10;

                //Set the style for header range.
                IRange headingRange = worksheet["A1:E1"];
                headingRange.CellStyle.Font.Bold = true;
                headingRange.CellStyle.ColorIndex = ExcelKnownColors.Light_green;

                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    // Марка опоры
                    worksheet["B" + (2 + i)].Text = item.Pillar.Name;
                    // Координаты опоры
                    worksheet["С" + (2 + i)].Text = item.Position;
                    // Угол наклона
                    worksheet["D" + (2 + i)].Number = item.Angle;
                }

                //worksheet["A2"].Text = "Product A";
                //worksheet["A3"].Text = "Product B";
                //worksheet["A4"].Text = "Product C";

                //worksheet["B2"].Number = 2;
                //worksheet["B3"].Number = 1;
                //worksheet["B4"].Number = 1;

                //worksheet["C2"].Number = 99.00;
                //worksheet["C3"].Number = 199.00;
                //worksheet["C4"].Number = 149.00;

                //Save the workbook to stream in xlsx format. 
                try
                {
                    using (MemoryStream str = new MemoryStream())
                    {
                        workbook.SaveAs(str);
                        return str.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Opora", ex.Message, "OK");
                    return null;
                }

                //workbook.Close();
            }
        }
        
    }
}