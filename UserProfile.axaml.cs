using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace WarehouseDesktopApp;

public partial class UserProfile : UserControl
{
    public UserProfile()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void AvaibleWarehousesButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
}