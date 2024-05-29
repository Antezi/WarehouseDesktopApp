using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using WarehouseDesktopApp.Context;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class SupplieEditWindow : Window
{
    private ComboBox _sizeComboBox, _statusComboBox, _truckComboBox, _departWarehouseComboBox, _destinationComboBox, _productComboBox;
    private TextBlock _idTextBlock, _departAddressTextBlock, _destinationTextBlock, _saveChangesTextBlock;
    private NumericUpDown _countNumericUpDown;
    private Button _loadNewImageButton;
    private CalendarDatePicker _deliveryStartCalendarDatePicker, _deliveryEndCalendarDatePicker;
    private Image _supplyImage;
    private WarehousesdbContext context = new WarehousesdbContext();
    private string _firstImageName, _imagePath, _fullImagePath;
    private bool isNewSupply = true;
    private SupplyDTO _currentSupplyDTO;

    private List<SizeType> _sizesTypesList = new List<SizeType>();
    private List<StatusType> _statusTypesList = new List<StatusType>();
    private List<Truck> _trucksList = new List<Truck>();
    private List<Warehouse> _warehousesList = new List<Warehouse>();
    private List<Product> _productsList = new List<Product>();
    public SupplieEditWindow()
    {
        InitializeComponent();
    }
    
    public SupplieEditWindow(SupplyDTO supply)
    {
        isNewSupply = false;
        _currentSupplyDTO = supply;
        InitializeComponent();
        LoadSupplyData(supply);
        CheckAvailbleComboBoxes(supply);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        _sizeComboBox = this.FindControl<ComboBox>("SizeComboBox");
        _statusComboBox = this.FindControl<ComboBox>("StatusComboBox");
        _truckComboBox = this.FindControl<ComboBox>("TruckComboBox");
        _departWarehouseComboBox = this.FindControl<ComboBox>("DepartWarehouseComboBox");
        _destinationComboBox = this.FindControl<ComboBox>("DestinationComboBox");
        _productComboBox = this.FindControl<ComboBox>("ProductComboBox");

        _idTextBlock = this.FindControl<TextBlock>("IdTextBlock");
        _departAddressTextBlock = this.FindControl<TextBlock>("DepartAddressTextBlock");
        _destinationTextBlock = this.FindControl<TextBlock>("DestinationTextBlock");
        _saveChangesTextBlock = this.FindControl<TextBlock>("SaveChangesTextBlock");

        _countNumericUpDown = this.FindControl<NumericUpDown>("CountNumericUpDown");

        _loadNewImageButton = this.FindControl<Button>("LoadNewImageButton");

        _deliveryStartCalendarDatePicker = this.FindControl<CalendarDatePicker>("DeliveryStartCalendarDatePicker");
        _deliveryEndCalendarDatePicker = this.FindControl<CalendarDatePicker>("DeliveryEndCalendarDatePicker");

        _supplyImage = this.FindControl<Image>("SupplyImage");
        
        
        
        LoadComboBoxData();
    }

    private void LoadComboBoxData()
    {
        _sizesTypesList = context.SizeTypes.ToList();
        _statusTypesList = context.StatusTypes.ToList();
        _trucksList = context.Trucks.ToList();
        _warehousesList = context.Warehouses.ToList();
        _productsList = context.Products.ToList();
        
        _sizeComboBox.ItemsSource = _sizesTypesList.Select(s => s.Name).ToList();
        _statusComboBox.ItemsSource = _statusTypesList.Select(s => s.Name).ToList();
        _truckComboBox.ItemsSource = _trucksList.Select(s => s.Number).ToList();
        _departWarehouseComboBox.ItemsSource = _warehousesList.Select(w => w.Address).ToList();
        _destinationComboBox.ItemsSource = _warehousesList.Select(w => w.Address).ToList();
        _productComboBox.ItemsSource = _productsList.Select(p => p.Name).ToList();

        //_sizeComboBox.ItemsSource = context.SizeTypes.Select(s => s.Name).ToList();
        //_statusComboBox.ItemsSource = context.StatusTypes.Select(s => s.Name).ToList();
        //_truckComboBox.ItemsSource = context.Trucks.Select(t => t.Number).ToList();
        //_departWarehouseComboBox.ItemsSource = context.Warehouses.Select(w => w.Address).ToList();
        //_destinationComboBox.ItemsSource = context.Warehouses.Select(w => w.Address).ToList();
    }

    private void LoadSupplyData(SupplyDTO supply)
    {
        _idTextBlock.Text = "Id: " + supply.Id.ToString();
        _sizeComboBox.SelectedItem = _sizesTypesList.Where(s => s.Id == supply.Size).FirstOrDefault().Name;
        _statusComboBox.SelectedItem = _statusTypesList.Where(s => s.Id == supply.Status).FirstOrDefault().Name;
        _productComboBox.SelectedItem = _productsList.Where(p => p.Id == supply.Product).FirstOrDefault().Name;
        if (supply.Truck != null)
        {
            _truckComboBox.SelectedItem = _trucksList.Where(t => t.Number == supply.Truck.Number).FirstOrDefault().Number;
        }
        
        _departWarehouseComboBox.SelectedItem =
            _warehousesList.Where(w => w.Id == supply.DepartWarehouseId).FirstOrDefault().Address;
        _destinationComboBox.SelectedItem =
            _warehousesList.Where(w => w.Id == supply.DestinationWarehouseId).FirstOrDefault().Address;
        
        _departAddressTextBlock.Text = "Адрес отправления: " + supply.DepartWarehouse.Address;
        _destinationTextBlock.Text = "Адрес доставки: " + supply.DestinationWarehouse.Address;

        _countNumericUpDown.Value = Convert.ToDecimal(supply.Count);

        _deliveryStartCalendarDatePicker.SelectedDate = supply.DeliveryStart.Date;
        if (supply.DeliveryEnd != null)
        {
            _deliveryEndCalendarDatePicker.SelectedDate = supply.DeliveryEnd.Value;
        }
        
        _supplyImage.Source = supply.ImageView;
        _firstImageName = supply.ProductNavigation.Photopath;
        
        _saveChangesTextBlock.Text = "Сохранить изменения";
    }

    private void CheckAvailbleComboBoxes(SupplyDTO supply)
    {
        if (supply.Status == 2)
        {
            _sizeComboBox.IsEnabled = false;
            _truckComboBox.IsEnabled = false;
            _countNumericUpDown.IsEnabled = false;
            _departWarehouseComboBox.IsEnabled = false;
            _productComboBox.IsEnabled = false;
            _deliveryStartCalendarDatePicker.IsEnabled = false;
        }
        
        else if (supply.Status == 3)
        {
            _sizeComboBox.IsEnabled = false;
            _truckComboBox.IsEnabled = false;
            _countNumericUpDown.IsEnabled = false;
            _departWarehouseComboBox.IsEnabled = false;
            _destinationComboBox.IsEnabled = false;
            _deliveryStartCalendarDatePicker.IsEnabled = false;
            _deliveryEndCalendarDatePicker.IsEnabled = false;
            _productComboBox.IsEnabled = false;
        }
    }

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }

    private void SizeComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }

    private void StatusComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }

    private void TruckComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }

    private void DepartWarehouseComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }

    private void DestinationComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }

    private async void LoadNewImageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter
            {
                Extensions = { "png", "jpg" }
            });
            var result = await dialog.ShowAsync(this);
            
            FileInfo fileInfo = new FileInfo(_fullImagePath);
            if (fileInfo.Length < 2 * 1024 * 1024)
            {
                _imagePath = result[0].Split("\\").Last();
                _fullImagePath = result[0];
                _supplyImage.Source = new Bitmap(_fullImagePath);
            }
            else
            {
                MessageBox mb = new MessageBox("Размер файла должен быть меьше 2 МБ");
                mb.ShowDialog(this);
            }
        }
        catch (Exception exception)
        {

        }
    }

    private void SaveChangesButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_sizeComboBox.SelectedIndex != -1 && _statusComboBox.SelectedIndex != -1 &&
            _truckComboBox.SelectedIndex != -1 &&
            _departWarehouseComboBox.SelectedIndex != -1 && _destinationComboBox.SelectedIndex != -1 &&
            _deliveryStartCalendarDatePicker.SelectedDate != null)
        {
            try
            {
                if (isNewSupply == true)
                {
                    Supply supply = new Supply
                    {
                        Size = _sizesTypesList[_sizeComboBox.SelectedIndex].Id,
                        Status = _statusTypesList[_statusComboBox.SelectedIndex].Id,
                        TruckId = _trucksList[_truckComboBox.SelectedIndex].Id,
                        Count = Convert.ToInt32(_countNumericUpDown.Value),
                        DepartWarehouseId = _warehousesList[_departWarehouseComboBox.SelectedIndex].Id,
                        DestinationWarehouseId = _warehousesList[_destinationComboBox.SelectedIndex].Id,
                        DeliveryStart = _deliveryStartCalendarDatePicker.SelectedDate.Value,
                        Product = _productsList[_productComboBox.SelectedIndex].Id
                    };

                    context.Supplies.Add(supply);
                    context.SaveChanges();
                }
                else
                {
                    Supply supply = context.Supplies.Where(s => s.Id == _currentSupplyDTO.Id).FirstOrDefault();
                    if (supply != null)
                    {
                        supply.Size = _sizesTypesList[_sizeComboBox.SelectedIndex].Id;
                        supply.Status = _statusTypesList[_statusComboBox.SelectedIndex].Id;
                        supply.TruckId = _trucksList[_truckComboBox.SelectedIndex].Id;
                        supply.Count = Convert.ToInt32(_countNumericUpDown.Value);
                        supply.DeliveryStart = _deliveryStartCalendarDatePicker.SelectedDate.Value;
                        try
                        {
                            supply.DeliveryEnd = _deliveryEndCalendarDatePicker.SelectedDate.Value;
                        }
                        catch (Exception exception)
                        {
                        }
                        
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                
            }
        }
    }
}