using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class MainMenu : Window
{
    private User currentUser;
    private TextBlock _helloTextBlock;
    public MainMenu()
    {
        MinHeight = 800;
        MaxHeight = 800;
        MinWidth = 1200;
        MaxWidth = 1200;
        
        InitializeComponent();
    }
    
    public MainMenu(User user)
    {
        MinHeight = 800;
        MaxHeight = 800;
        MinWidth = 1200;
        MaxWidth = 1200;
        
        InitializeComponent();

        currentUser = user;
        _helloTextBlock.Text = $"Добрый день, {user.Login}";
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        _helloTextBlock = this.FindControl<TextBlock>("HelloTextBlock");
    }

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }

    private void SuppliesMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        SupplieManageWindow supplieManageWindow = new SupplieManageWindow();
        supplieManageWindow.ShowDialog(this);
    }

    private void ProfileMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        UsersManageWindow usersManageWindow = new UsersManageWindow();
        usersManageWindow.ShowDialog(this);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
       
    }
}