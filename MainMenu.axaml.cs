using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class MainMenu : Window
{
    private UserProfile _userProfile;
    private User currentUser;
    private TextBlock _helloTextBlock;
    public MainMenu()
    {
        InitializeComponent();
    }
    
    public MainMenu(User user)
    {
        InitializeComponent();

        currentUser = user;
        _helloTextBlock.Text = $"Добрый день, {user.Login}";
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        _helloTextBlock = this.FindControl<TextBlock>("HelloTextBlock");
        _userProfile = this.FindControl<UserProfile>("UserProfile");
    }

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }

    private void SuppliesMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }

    private void ProfileMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
       // UserProfile.IsVisible = true;
    }
}