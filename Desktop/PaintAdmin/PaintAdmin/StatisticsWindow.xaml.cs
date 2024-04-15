using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PaintAdmin
{
    public partial class StatisticsWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }

        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        public StatisticsWindow()
        {
            InitializeComponent();
            cbCategory.SelectedIndex = 0;
            DataContext = this;
            Closing += StatisticsWindow_Closing;
        }

        private void StatisticsWindow_Closing(object sender, CancelEventArgs e)
        {
            DeliveryWindow deliveryWindow = new DeliveryWindow();
            deliveryWindow.Show();
        }

        private async void LoadData(string What)
        {
            var salesData = await GenerateSalesData();

            var seriesCollection = new SeriesCollection();

            if (What.Equals("Продажи за все время"))
                seriesCollection.Add(CreateSeries("Продажи за все время", salesData));
            else if (What.Equals("Продажи за неделю"))
                seriesCollection.Add(CreateSeries("Продажи за неделю", salesData.Where(s => s.Date >= DateTime.Today.AddDays(-7))));
            else if (What.Equals("Продажи за месяц"))
                seriesCollection.Add(CreateSeries("Продажи за месяц", salesData.Where(s => s.Date >= DateTime.Today.AddMonths(-1))));
            else if (What.Equals("Продажи за год"))
                seriesCollection.Add(CreateSeries("Продажи за год", salesData.Where(s => s.Date >= DateTime.Today.AddYears(-1))));

            SeriesCollection = seriesCollection;
        }

        private async Task<IEnumerable<Table.SoldColor>> GenerateSalesData()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "SoldColors", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.SoldColor>>(content);
                for (int i = 0; i < contentData.Count(); i++)
                {
                    return contentData;
                }
            }
            else
            {
                MessageBox.Show("Ошибка", "Уведомление");
                return null;
            }
            return null;
        }

        private LineSeries CreateSeries(string title, IEnumerable<Table.SoldColor> salesData)
        {
            var values = new ChartValues<double>();
            var labels = salesData.Select(s => s.Date.ToString("MMM")).ToArray();

            foreach (var sale in salesData)
            {
                values.Add(sale.Price_Delivery);
            }

            return new LineSeries
            {
                Title = title,
                Values = values,
            };
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void cbCategory_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            string selectedCategory = (string)selectedItem.Content;
            LoadData(selectedCategory);
        }
    }
}
