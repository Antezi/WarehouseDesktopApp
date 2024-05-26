using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using WarehouseDesktopApp.Context;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class SupplieManageWindow : Window
{
    private List<SupplyDTO> currentSupplies = new List<SupplyDTO>();
    private WarehousesdbContext context = new WarehousesdbContext();
    private int supplyCount = 10, statusCode, productCode;
    private ListBox _suppliesListBox;
    private ComboBox _statusComboBox, _countComboBox, _productComboBox;

    private TextBox _searchTextBox;
    private TextBlock _helloTextBlock,
        _idTextBlock,
        _productTextBlock,
        _statusTextBlock,
        _departTextBlock,
        _destinationTextBlock,
        _suppliesCountTextBlock;
    public SupplieManageWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        MinHeight = 800;
        MaxHeight = 800;
        MinWidth = 1200;
        MaxWidth = 1200;
        AvaloniaXamlLoader.Load(this);

        _suppliesListBox = this.FindControl<ListBox>("SuppliesListBox");

        _statusComboBox = this.FindControl<ComboBox>("StatusComboBox");
        _countComboBox = this.FindControl<ComboBox>("CountComboBox");
        _productComboBox = this.FindControl<ComboBox>("ProductComboBox");

        _searchTextBox = this.FindControl<TextBox>("SearchTextBox");

        _helloTextBlock = this.FindControl<TextBlock>("HelloTextBlock");
        _idTextBlock = this.FindControl<TextBlock>("IdTextBlock");
        _productTextBlock = this.FindControl<TextBlock>("ProductTextBlock");
        _statusTextBlock = this.FindControl<TextBlock>("StatusTextBlock");
        _departTextBlock = this.FindControl<TextBlock>("DepartTextBlock");
        _destinationTextBlock = this.FindControl<TextBlock>("DestinationTextBlock");
        _suppliesCountTextBlock = this.FindControl<TextBlock>("SuppliesCountTextBlock");

        _countComboBox.Items.Add("10");
        _countComboBox.Items.Add("20");
        _countComboBox.Items.Add("50");
        _countComboBox.Items.Add("Все");

        _statusComboBox.ItemsSource = context.StatusTypes.Select(s => s.Name).ToList();
        _productComboBox.ItemsSource = context.Products.Select(p => p.Name).ToList();
        
        LoadData();
    }
    
    private async Task<List<Supply>> GetSuppliesAsync()
    {
        using (var client = new HttpClient())
        {
            var url = $"http://37.128.207.61:3001/api/GetAllSupplies";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Supply>>();
            }

            return new List<Supply>();
        }
    }
    
    private async void LoadData()
    {
        try
        {
            var supplies = await GetSuppliesAsync();

            if (supplies.Any())
            {
                var currentSupplies = supplies.Select(s => new SupplyDTO()
                {
                    Id =  s.Id,
                    Product = s.Product,
                    Size = s.Size,
                    Status = s.Status,
                    DepartWarehouseId = s.DepartWarehouseId,
                    DestinationWarehouseId = s.DestinationWarehouseId,
                    DeliveryStart = s.DeliveryStart,
                    DeliveryEnd = s.DeliveryEnd,
                    Cells = s.Cells, 
                    DepartWarehouse = s.DepartWarehouse,
                    DestinationWarehouse = s.DestinationWarehouse,
                    ProductNavigation = s.ProductNavigation,
                    SizeNavigation = s.SizeNavigation,
                    StatusNavigation = s.StatusNavigation
                }).ToList();

                foreach (var s in currentSupplies)
                {
                    try
                    {
                        Bitmap image = new Bitmap("../../../Assets/Products/Images/" + s.ProductNavigation.Photopath);
                        s.ImageView = image;
                    }
                    catch (Exception e)
                    {
                        
                    }
                }

                SortData(currentSupplies);
                _suppliesListBox.ItemsSource = currentSupplies;
            }
            else
            {
                var mb = new MessageBox("Произошла ошибка: груз не найден.");
                mb.Closing += (sender, e) =>
                {
                    // Какое-то действие при закрытии MessageBox
                };
                mb.ShowDialog(this);
            }
        }
        catch (Exception e)
        {
            var mb = new MessageBox($"Произошла ошибка: {e.Message}");
            mb.Closing += (sender, e) =>
            {
                // Какое-то действие при закрытии MessageBox
            };
            mb.ShowDialog(this);
        }
    }
    
    
    private void SortData(List<SupplyDTO> supplies)
    {
        if (statusCode != null)
        {
            supplies = supplies.Where(s => s.Status == statusCode).ToList();
        }

        if (productCode != null)
        {
            supplies = supplies.Where(s => s.Product == productCode).ToList();
        }

            /*if (_productComboBox.SelectedIndex != -1)
            {
                if (_countComboBox.SelectedIndex != 0)
                {
                    client = client.OrderBy(c => c.Lastname).ToList();
                }
                else if (dataSearch == 2)
                {
                    client = client.OrderByDescending(c => c.LastVisitView).ToList();
                }
                else if (dataSearch == 3)
                {
                    client = client.OrderByDescending(c => c.VisitCountView).ToList();
                }
            }

            if (_monthBirthdayCheckBox.IsChecked == true)
            {
                client = client.Where(c => c.Birthday.Value.Month == Convert.ToInt32(DateTime.Now.Month))
                    .ToList();
            }*/

            /*if (!string.IsNullOrEmpty(_searchTextBox.Text))
            {
                var searchTerms = _searchTextBox.Text.ToLower()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
                supplies = supplies.Where(x => searchTerms.All(term => x.ToLower().Contains(term) || x.Email.ToLower().Contains(term)
                    || x.Phone.ToLower().Contains(term))).ToList();
            }*/

            
            if (supplies.Count < supplyCount)
            {
                _suppliesListBox.ItemsSource = supplies;
                currentSupplies = supplies;
                _suppliesCountTextBlock.Text = supplies.Count + " из " + currentSupplies.Count;
            }
            else
            {
                List<SupplyDTO> _currentClientList = new List<SupplyDTO>();
                for (int i = 0; i < supplyCount; i++)
                {
                    _currentClientList.Add(supplies[i]);
                }

                _suppliesListBox.ItemsSource = _currentClientList;
                currentSupplies = supplies;
                _suppliesCountTextBlock.Text = _currentClientList.Count + " из " + supplies.Count;
            }
    }

    private void PrevButton_OnClick(object? sender, RoutedEventArgs e)
    {

    }

    private void NextButton_OnClick(object? sender, RoutedEventArgs e)
    {

    }

    private void EditSupplieButton_OnClick(object? sender, RoutedEventArgs e)
    {

    }

    private void NewSupplieButton_OnClick(object? sender, RoutedEventArgs e)
    {

    }

    private void StatusComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_statusComboBox.SelectedIndex != 0)
        {
            string statusString =  _statusComboBox.SelectedItem.ToString();
            statusCode = context.StatusTypes.Where(s => s.Name == statusString).FirstOrDefault().Id;
            if (statusCode != null) 
            {
                SortData(currentSupplies);    
            }
        }
    }
    
    private void ProductComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_productComboBox.SelectedIndex != -1)
        {
            string productString =  _productComboBox.SelectedItem.ToString();
            productCode = context.Products.Where(s => s.Name == productString).FirstOrDefault().Id;
            if (productCode != null) 
            {
                SortData(currentSupplies);    
            }
        }
    }

    private void CountComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_countComboBox.SelectedIndex != -1) {
            if (_countComboBox.SelectedIndex == 0)
            {
                supplyCount = 10;
            }
            else if (_countComboBox.SelectedIndex == 1)
            {
                supplyCount = 20;
            }
            else if (_countComboBox.SelectedIndex == 2)
            {
                supplyCount = 50;
            }
            else if (_countComboBox.SelectedIndex == 3)
            {
                supplyCount = currentSupplies.Count;
            }
        }

        SortData(currentSupplies);
    }

}