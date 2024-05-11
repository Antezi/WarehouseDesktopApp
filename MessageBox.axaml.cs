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
        InitializeComponent();
        _messageTextBlock.Text = message;
    }

    private void InitializeComponent()
    {
        _messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
    }

    private void OkButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}