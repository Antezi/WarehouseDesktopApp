using Avalonia;
using Avalonia.Controls;
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
}