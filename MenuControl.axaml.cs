using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace WarehouseDesktopApp;

public partial class MenuControl : UserControl
{
    public MenuControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void SuppliesMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }

    private void ProfileMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        // UserProfile.IsVisible = true;
    }
}