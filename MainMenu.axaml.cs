using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using WarehouseDesktopApp.Context;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp;

public partial class MainMenu : Window
{
    private UserDTO _currentUser;
    private TextBlock _helloTextBlock;
    private Image _profileImage;
    public MainMenu()
    {
        InitializeComponent();
    }
    
    public MainMenu(User user)
    {
        InitializeComponent();
        FillUserDTO(user);

        _profileImage.Source = _currentUser.PhotopathView;
        _helloTextBlock.Text = $"Добрый день, {user.Login}";
    }

    private void FillUserDTO(User user)
    {
        _currentUser = new UserDTO
        {
            Id = user.Id,
            Login = user.Login,
            Type = user.Type,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Patronymic = user.Patronymic,
            Email = user.Email,
            Phone = user.Phone,
            Passport = user.Passport,
            Photopath = user.Photopath,
            AccessToUsers = user.AccessToUsers,
            RightAccessToUsers = user.RightAccessToUsers,
            TypeNavigation = user.TypeNavigation,
            WarehousesAccessToUsers = user.WarehousesAccessToUsers,
            PhotopathView = new Bitmap("../../../Assets/Users/Images/" + user.Photopath)
        };
    }

    private void InitializeComponent()
    {
        MinHeight = 800;
        MaxHeight = 800;
        MinWidth = 1200;
        MaxWidth = 1200;
        AvaloniaXamlLoader.Load(this);

        _helloTextBlock = this.FindControl<TextBlock>("HelloTextBlock");
        _profileImage = this.FindControl<Image>("ProfileImage");
    }

    private void ProfileImage_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        UserProfileMenu userProfileMenu = new UserProfileMenu(_currentUser);
        userProfileMenu.ShowDialog(this);
    }

    private void SuppliesMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        SupplieManageWindow supplieManageWindow = new SupplieManageWindow(_currentUser);
        supplieManageWindow.ShowDialog(this);
    }

    private void ProfileMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        UsersManageWindow usersManageWindow = new UsersManageWindow(_currentUser);
        usersManageWindow.ShowDialog(this);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
       
    }

    /* private void GenerateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using (var context = new WarehousesdbContext())
            {
                context.Database.EnsureCreated();

                // Генерация связанных данных
                var productTypes = DataGenerator.GenerateProductTypes(5);
                var truckModels = DataGenerator.GenerateTruckModels(10);
                var truckStatuses = DataGenerator.GenerateTruckStatuses(3);
                var usersTypes = DataGenerator.GenerateUsersTypes(5);
                var warehouseTypes = DataGenerator.GenerateWarehouseTypes(5);
                var warehouseClasses = DataGenerator.GenerateWarehouseClasses(3);
                var sizeTypes = DataGenerator.GenerateSizeTypes(5);
                var statusTypes = DataGenerator.GenerateStatusTypes(5);

                context.ProductTypes.AddRange(productTypes);
                context.TruckModels.AddRange(truckModels);
                context.TruckStatuses.AddRange(truckStatuses);
                context.UsersTypes.AddRange(usersTypes);
                context.WarehouseTypes.AddRange(warehouseTypes);
                context.WarehousesClasses.AddRange(warehouseClasses);
                context.SizeTypes.AddRange(sizeTypes);
                context.StatusTypes.AddRange(statusTypes);

                context.SaveChanges();

                // Генерация основных данных
                var products = DataGenerator.GenerateProducts(100, productTypes);
                var trucks = DataGenerator.GenerateTrucks(50, truckModels, truckStatuses);
                var users = DataGenerator.GenerateUsers(200, usersTypes);
                var warehouses = DataGenerator.GenerateWarehouses(30, warehouseTypes, warehouseClasses);
                var supplies = DataGenerator.GenerateSupplies(200, products, trucks, warehouses, sizeTypes, statusTypes);

                context.Products.AddRange(products);
                context.Trucks.AddRange(trucks);
                context.Users.AddRange(users);
                context.Warehouses.AddRange(warehouses);
                context.Supplies.AddRange(supplies);

                context.SaveChanges();
            }
        }*/
    private void ProductMenu_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        
    }
}
