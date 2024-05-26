using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace WarehouseDesktopApp;

public partial class MessageBox : Window
{
    private TextBlock _messageTextBlock;
    public MessageBox()
    {
        InitializeComponent();
    }
    
    public MessageBox(string message)
    {
        MinHeight = 250;
        MaxHeight = 250;
        MinWidth = 400;
        MaxWidth = 400;
        InitializeComponent();
        _messageTextBlock.Text = message;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
    }

    private void OkButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}