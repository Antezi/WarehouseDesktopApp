using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class UserProfileMenu : Window
{
    public UserProfileMenu()
    {
        InitializeComponent();
    }

    public UserProfileMenu(UserDTO user)
    {
        
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }
}