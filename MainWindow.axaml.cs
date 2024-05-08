using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using WarehouseDesktopApp.Classes;
using WarehouseDesktopApp.Context;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class MainWindow : Window
{
    private string connectionString = "http://5.16.21.9:3001/api";
    private TextBox _loginBox, _passwordBox;
    public MainWindow()
    {
        InitializeComponent();
    }

    public void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        _loginBox = this.FindControl<TextBox>("LoginBox");
        _passwordBox = this.FindControl<TextBox>("PasswordBox");
    }

    private async void AuthButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(_loginBox.Text) && !string.IsNullOrEmpty(_passwordBox.Text))
        {
            string login = _loginBox.Text;
            string password = _passwordBox.Text;
            string hash = Sha256Generator.ComputeSHA256Hash(password);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{connectionString}/users/AuthUser/Login/{login}/Password/{hash}");
                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(userJson);
                
                    // Вход выполнен успешно, используйте данные пользователя
                }
                else
                {
                    // Обработка ошибок, например, показ сообщения об ошибке
                }
            }
        }
        else
        {
            // Вывод ошибки о том, что логин и пароль должны быть заполнены
        }
    }
}