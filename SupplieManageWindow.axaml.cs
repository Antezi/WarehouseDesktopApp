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
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class SupplieManageWindow : Window
{
    private ListBox _suppliesListBox;
    private ComboBox _typeComboBox, _countComboBox;

    private TextBlock _helloTextBlock,
        _idTextBlock,
        _productTextBlock,
        _statusTextBlock,
        _departTextBlock,
        _destinationTextBlock;
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

        _typeComboBox = this.FindControl<ComboBox>("TypeComboBox");
        _countComboBox = this.FindControl<ComboBox>("CountComboBox");

        _helloTextBlock = this.FindControl<TextBlock>("HelloTextBlock");
        _idTextBlock = this.FindControl<TextBlock>("IdTextBlock");
        _productTextBlock = this.FindControl<TextBlock>("ProductTextBlock");
        _statusTextBlock = this.FindControl<TextBlock>("StatusTextBlock");
        _departTextBlock = this.FindControl<TextBlock>("DepartTextBlock");
        _destinationTextBlock = this.FindControl<TextBlock>("DestinationTextBlock");
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
                var currentUsers = supplies.Select(s => new SupplyDTO()
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

                foreach (var u in currentUsers)
                {
                    try
                    {
                        Bitmap image = new Bitmap("../../../Assets/Users/Images/" + u.ProductNavigation.Photopath);
                        u.ImageView = image;
                    }
                    catch (Exception e)
                    {
                        
                    }
                }

                _suppliesListBox.ItemsSource = currentUsers; // Обновите источник данных ListBox
            }
            else
            {
                var mb = new MessageBox("Произошла ошибка: пользователи не найдены.");
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
}