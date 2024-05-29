using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using WarehouseDesktopApp.Context;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class UsersManageWindow : Window
{
    private WarehousesdbContext context = new WarehousesdbContext();
    private ListBox _usersListBox;
    private ComboBox _typeComboBox, _countComboBox;
    private Image _profileImage;
    private UserDTO _currentUser;
    private List<UsersType> _userTypesList = new List<UsersType>();
    private TextBlock _helloTextBlock,
        _idTextBlock,
        _loginTextBlock,
        _typeTextBlock,
        _firstnameTextBlock,
        _lastnameTextBlock,
        _patronymicTextBlock,
        _usersCountTextBlock;
    
    private List<UserDTO> users = new List<UserDTO>();
    private List<UserDTO> _currentUsers = new List<UserDTO>();
    private int type = 1, count = 10, page = 1;
    
    public UsersManageWindow()
    {
        InitializeComponent();
    }

    public UsersManageWindow(UserDTO user)
    {
        _currentUser = user;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        MinHeight = 800;
        MaxHeight = 800;
        MinWidth = 1200;
        MaxWidth = 1200;
        AvaloniaXamlLoader.Load(this);

        _usersListBox = this.FindControl<ListBox>("UsersListBox");

        _profileImage = this.FindControl<Image>("ProfileImage");

        _helloTextBlock = this.FindControl<TextBlock>("HelloTextBlock");
        _idTextBlock = this.FindControl<TextBlock>("IdTextBlock");
        _loginTextBlock = this.FindControl<TextBlock>("LoginTextBlock");
        _typeTextBlock = this.FindControl<TextBlock>("TypeTextBlock");
        _firstnameTextBlock = this.FindControl<TextBlock>("FirstnameTextBlock");
        _lastnameTextBlock = this.FindControl<TextBlock>("LastnameTextBlock");
        _patronymicTextBlock = this.FindControl<TextBlock>("PatronymicTextBlock");
        _usersCountTextBlock = this.FindControl<TextBlock>("UsersCountTextBlock");

        _typeComboBox = this.FindControl<ComboBox>("TypeComboBox");
        _countComboBox = this.FindControl<ComboBox>("CountComboBox");
        
        _countComboBox.Items.Add("10");
        _countComboBox.Items.Add("20");
        _countComboBox.Items.Add("50");
        _countComboBox.Items.Add("Все");


        _userTypesList = context.UsersTypes.ToList();
        _typeComboBox.ItemsSource = _userTypesList.Select(t => t.Name).ToList();
        _profileImage.Source = _currentUser.PhotopathView;
        LoadData();
    }

    private async Task<List<User>> GetAllUsersAsync()
    {
        using (var client = new HttpClient())
        {
            var url = $"http://37.128.207.61:3001/api/AllUsers";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<User>>();
            }

            return new List<User>();
        }
    }


    private async Task<List<User>> GetUsersAsync(int type, int page, int count)
    {
        using (var client = new HttpClient())
        {
            var url = $"http://37.128.207.61:3001/api/users/{type}/Page/{page}/Count/{count}";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<User>>();
            }

            return new List<User>();
        }
    }

    private async void LoadData()
    {
        try
        {
            //var users = await GetUsersAsync(type, page, count);
            var users = await GetAllUsersAsync();

            if (users.Any())
            {
                var currentUsers = users.Select(user => new UserDTO
                {
                    Id =  user.Id,
                    Login = user.Login,
                    Type = user.Type,
                    Firstname =  user.Firstname,
                    Lastname = user.Lastname,
                    Patronymic = user.Patronymic,
                    Email = user.Email,
                    Phone = user.Phone,
                    Passport = user.Passport,
                    Photopath = user.Photopath,
                    TypeNavigation = user.TypeNavigation,
                    AccessToUsers = user.AccessToUsers,
                    RightAccessToUsers = user.RightAccessToUsers,
                    WarehousesAccessToUsers = user.WarehousesAccessToUsers,
                }).ToList();

                foreach (var u in currentUsers)
                {
                    try
                    {
                        Bitmap image = new Bitmap("../../../Assets/Users/Images/" + u.Photopath);
                        u.PhotopathView = image;
                    }
                    catch (Exception e)
                    {
                        
                    }

                    u.LoginView = "Логин: " + u.Login;
                    u.FirstnameView = "Имя: " + u.Firstname;
                    u.LastnameView = "Фамилия: " + u.Lastname;
                    u.PatronymicView = "Отчество: " + u.Patronymic;
                }

                _currentUsers = currentUsers;
                UpdateData(currentUsers);
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

    private void UpdateData(List<UserDTO> users)
    {
        if (_typeComboBox.SelectedIndex != -1)
        {
            users = users.Where(u => u.Type == _userTypesList[_typeComboBox.SelectedIndex].Id).ToList();
        }
        
        int totalPages = (int)Math.Ceiling(users.Count / (double)count);
        if (page > totalPages)
        {
            page = totalPages;
        }
        
        users = users.Skip((page - 1) * count).Take(count).ToList();
        
        _usersListBox.ItemsSource = users;
    }

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        //UserDTO user = ((sender as Image).Parent.DataContext) as UserDTO;
        UserProfileMenu userProfileMenu = new UserProfileMenu(_currentUser);
        userProfileMenu.Closing += (sender, e) =>
        {
            LoadData();
        };
        userProfileMenu.ShowDialog(this);
    }


    private void NewUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        UserProfileMenu userProfileMenu = new UserProfileMenu();
        userProfileMenu.Closing += (sender, e) =>
        {
            LoadData();
        };
        userProfileMenu.ShowDialog(this);
    }

    private void PrevButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (page > 1)
        {
            if (page > 1)
            {
                page--;
                UpdateData(_currentUsers);
            }
        }
    }

    private void NextButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (count * page < _currentUsers.Count)
        {
            page++;
            UpdateData(_currentUsers);
        }
    }

    private void UserGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        UserDTO user = ((sender as Grid).Parent.DataContext) as UserDTO;
        UserProfileMenu userProfileMenu = new UserProfileMenu(user);
        userProfileMenu.Closing += (sender, e) =>
        {
            LoadData();
        };
        userProfileMenu.ShowDialog(this);
    }

    private void EditUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        UserDTO user = ((sender as Button).Parent.DataContext) as UserDTO;
        UserProfileMenu userProfileMenu = new UserProfileMenu(user);
        userProfileMenu.Closing += (sender, e) =>
        {
            LoadData();
        };
        userProfileMenu.ShowDialog(this);
    }

    private void CountComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_countComboBox.SelectedIndex != -1) 
        {
            if (_countComboBox.SelectedIndex == 0)
            {
                count = 10;
                UpdateData(_currentUsers);
            }
            else if (_countComboBox.SelectedIndex == 1)
            {
                count = 20;
                UpdateData(_currentUsers);
            }
            else if (_countComboBox.SelectedIndex == 2)
            {
                count = 50;
                UpdateData(_currentUsers);
            }
            else if (_countComboBox.SelectedIndex == 3)
            {
                count = _currentUsers.Count;
                UpdateData(_currentUsers);
            }
        }
    }

    private void TypeComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateData(_currentUsers);
    }
}