using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using WarehouseDesktopApp.Classes;
using WarehouseDesktopApp.Context;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class UserProfileMenu : Window
{
    private TextBox _loginTextBox,
        _firstnameTextBox,
        _lastnameTextBox,
        _patronymicTextBox,
        _emailTextBox,
        _phoneTextBox,
        _passwordTextBox;

    private TextBlock _idTextBlock;
    private MaskedTextBox _passportMaskedTextBox;
    private Image _userPhoto;
    private ListBox _noAccessWarehousesListBox, _accessWarehousesListBox;
    private UserDTO _currentUser;
    private ComboBox _userTypeComboBox;
    private List<UsersType> _userTypesList = new List<UsersType>();
    private string _imagePath = "image1.png", _fullImagePath;

    private List<WarehouseDTO> _noAccessWarehousesList = new List<WarehouseDTO>(), _accessWarehousesList = new List<WarehouseDTO>();
    
    
    private WarehousesdbContext context = new WarehousesdbContext();
    public UserProfileMenu()
    {
        InitializeComponent();
    }

    public UserProfileMenu(UserDTO user)
    {
        _currentUser = user;
        InitializeComponent();
        _passwordTextBox.IsVisible = false;
        LoadWarehouses(user);
        LoadUserData(user);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        _loginTextBox = this.FindControl<TextBox>("LoginTextBox");
        _firstnameTextBox = this.FindControl<TextBox>("FirstnameTextBox");
        _lastnameTextBox = this.FindControl<TextBox>("LastnameTextBox");
        _patronymicTextBox = this.FindControl<TextBox>("PatronymicTextBox");
        _emailTextBox = this.FindControl<TextBox>("EmailTextBox");
        _phoneTextBox = this.FindControl<TextBox>("PhoneTextBox");
        _passwordTextBox = this.FindControl<TextBox>("PasswordTextBox");

        _userTypeComboBox = this.FindControl<ComboBox>("UserTypeComboBox");

        _idTextBlock = this.FindControl<TextBlock>("IdTextBlock");

        _passportMaskedTextBox = this.FindControl<MaskedTextBox>("PassportMaskedTextBox");

        _userPhoto = this.FindControl<Image>("UserPhoto");

        _noAccessWarehousesListBox = this.FindControl<ListBox>("NoAccessWarehousesListBox");
        _accessWarehousesListBox = this.FindControl<ListBox>("AccessWarehousesListBox");

        _userTypesList = context.UsersTypes.ToList();
        _userTypeComboBox.ItemsSource = _userTypesList.Select(t => t.Name).ToList();
    }

    private void LoadUserData(UserDTO user)
    {
        _loginTextBox.Text = user.Login;
        _firstnameTextBox.Text = user.Firstname;
        _lastnameTextBox.Text = user.Lastname;
        _patronymicTextBox.Text = user.Patronymic;
        _emailTextBox.Text = user.Email;
        _phoneTextBox.Text = user.Phone;
        _idTextBlock.Text = user.Id.ToString();
        _userPhoto.Source = user.PhotopathView;
        _passportMaskedTextBox.Text = FormatPassportNumber(user.Passport);
        _userTypeComboBox.ItemsSource = _userTypesList.Select(t => t.Name).ToList();
    }
    
    private string FormatPassportNumber(string passportNumber)
    {
        if (string.IsNullOrEmpty(passportNumber) || passportNumber.Length != 10)
            return passportNumber;

        return $"{passportNumber.Substring(0, 4)} {passportNumber.Substring(4)}";
    }
    
    private async Task<List<Warehouse>> GetWarehousesAsync()
    {
        using (var client = new HttpClient())
        {
            var url = $"http://37.128.207.61:3001/api/AllWarehouses";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Warehouse>>();
            }

            return new List<Warehouse>();
        }
    }
    
    private async Task<List<Warehouse>> GetWarehousesAccessAsync()
    {
        using (var client = new HttpClient())
        {
            var url = $"http://37.128.207.61:3001/api/AllWarehousesToAccess/{_currentUser.Id}";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Warehouse>>();
            }

            return new List<Warehouse>();
        }
    }
    
    private async void LoadWarehouses(UserDTO user)
    {
        try
        {
            _accessWarehousesList = new List<WarehouseDTO>();
            _noAccessWarehousesList = new List<WarehouseDTO>();
            
            var access = await GetWarehousesAccessAsync();
            var warehouses = await GetWarehousesAsync();

            if (warehouses.Any())
            {
                var accessWarehousesList = access.Select(w => new WarehouseDTO()
                {
                    Id = w.Id,
                    Address = w.Address,
                    Type = w.Type,
                    Class = w.Class,
                    Cells = w.Cells,
                    ClassNavigation = w.ClassNavigation,
                    SupplyDepartWarehouses = w.SupplyDepartWarehouses,
                    SupplyDestinationWarehouses = w.SupplyDestinationWarehouses,
                    TypeNavigation = w.TypeNavigation,
                    WarehousesAccessToUsers = w.WarehousesAccessToUsers
                }).ToList();

                foreach (var w in accessWarehousesList)
                {
                    w.AddressView = "Адрес: " + w.Address;
                    w.TypeView = "Тип: " + w.TypeNavigation.Name;
                    w.ClassView = "Класс: " + w.ClassNavigation.Name;
                }

                _accessWarehousesList = accessWarehousesList;
                
                var warehousesList = warehouses.Select(w => new WarehouseDTO()
                {
                    Id = w.Id,
                    Address = w.Address,
                    Type = w.Type,
                    Class = w.Class,
                    Cells = w.Cells,
                    ClassNavigation = w.ClassNavigation,
                    SupplyDepartWarehouses = w.SupplyDepartWarehouses,
                    SupplyDestinationWarehouses = w.SupplyDestinationWarehouses,
                    TypeNavigation = w.TypeNavigation,
                    WarehousesAccessToUsers = w.WarehousesAccessToUsers
                }).ToList();

                foreach (var w in warehousesList)
                {
                    w.AddressView = "Адрес: " + w.Address;
                    w.TypeView = "Тип: " + w.TypeNavigation.Name;
                    w.ClassView = "Класс: " + w.ClassNavigation.Name;

                    if (!accessWarehousesList.Any(aw => aw.Id == w.Id))
                    {
                        _noAccessWarehousesList.Add(w);
                    }
                }

                /*_accessWarehousesListBox.Items.Clear();
                _noAccessWarehousesListBox.Items.Clear();
                _accessWarehousesListBox.Items.Add(_accessWarehousesList);
                _noAccessWarehousesListBox.Items.Add(_noAccessWarehousesList);*/
                
                _accessWarehousesListBox.ItemsSource = _accessWarehousesList;
               _noAccessWarehousesListBox.ItemsSource = _noAccessWarehousesList;
            }
        }
        catch (Exception e)
        {
            
        }
    }

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }

    private void NoAccessGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            WarehouseDTO warehouse = ((sender as Grid).Parent.DataContext) as WarehouseDTO;
            WarehousesAccessToUser access = context.WarehousesAccessToUsers
                .Where(a => a.UserId == _currentUser.Id && a.WarehouseId == warehouse.Id).FirstOrDefault();
            if (access == null)
            {
                WarehousesAccessToUser newAccess = new WarehousesAccessToUser
                {
                    WarehouseId = warehouse.Id,
                    UserId = _currentUser.Id
                };

                context.WarehousesAccessToUsers.Add(newAccess);
                context.SaveChanges();
                LoadWarehouses(_currentUser);
            }
        }
        catch (Exception exception)
        {
            
        }
    }

    private void AccessGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            WarehouseDTO warehouse = ((sender as Grid).Parent.DataContext) as WarehouseDTO;
            WarehousesAccessToUser access = context.WarehousesAccessToUsers.Where(a => a.UserId == _currentUser.Id &&
                a.WarehouseId == warehouse.Id).FirstOrDefault();
            if (access != null)
            {
                context.WarehousesAccessToUsers.Remove(access);
                context.SaveChanges();
                LoadWarehouses(_currentUser);
            }
        }
        catch (Exception exception)
        {
            
        }
    }

    private void SaveChanges_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получение данных из текстовых полей
        var passportCode = _passportMaskedTextBox.Text.Replace(" ", "").Substring(0, 4);
        var passportNum = _passportMaskedTextBox.Text.Replace(" ", "").Substring(4);

        // Проверка условий
        if (string.IsNullOrEmpty(_loginTextBox.Text) || string.IsNullOrEmpty(_firstnameTextBox.Text) || string.IsNullOrEmpty(_lastnameTextBox.Text) || 
            string.IsNullOrEmpty(_patronymicTextBox.Text) || string.IsNullOrEmpty(_emailTextBox.Text) || string.IsNullOrEmpty(_phoneTextBox.Text) ||
            _userTypeComboBox.SelectedIndex == -1)
        {
            MessageBox messageBox = new MessageBox("Все поля должны быть заполнены.");
            messageBox.ShowDialog(this);
            return;
        }

        if (!IsValidEmail(_emailTextBox.Text))
        {
            MessageBox messageBox = new MessageBox("Неверный формат email");
            messageBox.ShowDialog(this);
            return;
        }

        if (!IsValidPhone(_phoneTextBox.Text))
        {
            MessageBox messageBox = new MessageBox("Неверный формат телефона");
            messageBox.ShowDialog(this);
            return;
        }

        
        if (_currentUser != null)
        {
            User user =  context.Users.Where(u => u.Id == _currentUser.Id).FirstOrDefault();
            user.Login = _loginTextBox.Text;
            user.Firstname = _firstnameTextBox.Text;
            user.Lastname = _lastnameTextBox.Text;
            user.Patronymic = _patronymicTextBox.Text;
            user.Email = _emailTextBox.Text;
            user.Phone = _phoneTextBox.Text;
            user.Passport = $"{passportCode} {passportNum}".Replace(" ", "");
            user.Type = _userTypesList[_userTypeComboBox.SelectedIndex].Id;

            context.SaveChangesAsync();
            MessageBox messageBox = new MessageBox("Данные успешно сохранены");
            messageBox.Show();
            this.Close();
        }

        else
        {
            if (!string.IsNullOrEmpty(_passwordTextBox.Text))
            {
                User newUser = new User
                {
                    Login = _loginTextBox.Text,
                    Password = Sha256Generator.ComputeSHA256Hash(_passwordTextBox.Text),
                    Firstname = _firstnameTextBox.Text,
                    Lastname = _lastnameTextBox.Text,
                    Patronymic = _patronymicTextBox.Text,
                    Email = _emailTextBox.Text,
                    Phone = _phoneTextBox.Text,
                    Passport = $"{passportCode} {passportNum}".Replace(" ", ""),
                    Type = _userTypesList[_userTypeComboBox.SelectedIndex].Id,
                    Photopath = _imagePath
                };
                
                context.Users.Add(newUser);
                context.SaveChanges();
                MessageBox mb = new MessageBox("Новый пользователь успешно добавлен");
                mb.Show();
                this.Close();
            }
            else
            {
                MessageBox mb = new MessageBox("Поле с паролем должно быть заполнено");
                mb.Show();
            }
            
        }

    }
    
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool IsValidPhone(string phone)
    {
        // Проверка формата телефона
        return Regex.IsMatch(phone, @"^\+?\d{1,2}?[-\s]?\(?\d{3}\)?[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$");
    }

    private async void  AddImage_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter
            {
                Extensions = { "png", "jpg" }
            });
            var result = await dialog.ShowAsync(this);
            _imagePath = result[0].Split("\\").Last();
            _fullImagePath = result[0];
            
            FileInfo fileInfo = new FileInfo(_fullImagePath);
            if (fileInfo.Length < 2 * 1024 * 1024)
            {
                _userPhoto.Source = new Bitmap(_fullImagePath);
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
}