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
    private ListBox _usersListBox;
    private ComboBox _typeComboBox, _countComboBox;
    private TextBlock _helloTextBlock,
        _idTextBlock,
        _loginTextBlock,
        _typeTextBlock,
        _firstnameTextBlock,
        _lastnameTextBlock,
        _patronymicTextBlock,
        _usersCountTextBlock;
    
    private List<UserDTO> users = new List<UserDTO>();
    private List<User> currentUsers = new List<User>();
    private int type = 1, count = 10, page = 1;
    
    public UsersManageWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        _usersListBox = this.FindControl<ListBox>("UsersListBox");

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

        LoadData();
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
            var users = await GetUsersAsync(type, page, count);

            if (users.Any())
            {
                var currentUsers = users.Select(user => new UserDTO
                {
                    Id =  user.Id,
                    Login = "Логин: " + user.Login,
                    Type = user.Type,
                    Firstname = "Имя: " + user.Firstname,
                    Lastname = "Фамилия: " + user.Lastname,
                    Patronymic = "Отчество:  " + user.Patronymic,
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
                }

                _usersListBox.ItemsSource = currentUsers; // Обновите источник данных ListBox
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

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }

    private void InputElement_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }
}