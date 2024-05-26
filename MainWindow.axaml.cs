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
    private User currentUser;
    private string connectionString = "http://37.128.207.61:3001/api";
    private TextBox _loginBox, _passwordBox;
    public MainWindow()
    {
        MinHeight = 400;
        MaxHeight = 400;
        MinWidth = 600;
        MaxWidth = 600;
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
                var response = await client.GetAsync($"{connectionString}/AuthUser/Login/{login}/Password/{hash}");
                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(userJson);

                    currentUser = user;
                    MainMenu mainMenu = new MainMenu(user);
                    mainMenu.Show();
                    this.Close();
                    // Вход выполнен успешно, используйте данные пользователя
                }
                else
                {
                    MessageBox mb = new MessageBox("Произошла ошибка");
                    mb.Closing += (sender, e) =>
                    {
                        // Какое-то действие
                    };
                    mb.ShowDialog(this);
                }
            }
        }
        else
        {
            MessageBox mb = new MessageBox("Произошла ошибка");
            mb.Closing += (sender, e) =>
            {
                // Какое-то действие
            };
            mb.ShowDialog(this);
        }
    }
}