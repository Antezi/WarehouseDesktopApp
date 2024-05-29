using Bogus;
using System;
using System.Collections.Generic;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp
{
  public class DataGenerator
    {
        public static List<ProductType> GenerateProductTypes(int count)
        {
            var productTypeIds = 1;
            var faker = new Faker<ProductType>()
                .RuleFor(pt => pt.Name, f => f.Commerce.Categories(1)[0]);

            return faker.Generate(count);
        }

        public static List<TruckModel> GenerateTruckModels(int count)
        {
            var truckModelIds = 1;
            var faker = new Faker<TruckModel>()
                .RuleFor(tm => tm.Name, f => f.Vehicle.Model());

            return faker.Generate(count);
        }

        public static List<TruckStatus> GenerateTruckStatuses(int count)
        {
            var truckStatusIds = 1;
            var faker = new Faker<TruckStatus>()
                .RuleFor(ts => ts.Name, f => f.Random.Word());

            return faker.Generate(count);
        }

        public static List<UsersType> GenerateUsersTypes(int count)
        {
            var userTypeIds = 1;
            var faker = new Faker<UsersType>()
                .RuleFor(ut => ut.Name, f => f.Name.JobType());

            return faker.Generate(count);
        }

        public static List<WarehouseType> GenerateWarehouseTypes(int count)
        {
            var warehouseTypeIds = 1;
            var faker = new Faker<WarehouseType>()
                .RuleFor(wt => wt.Name, f => f.Random.Word());

            return faker.Generate(count);
        }

        public static List<WarehousesClass> GenerateWarehouseClasses(int count)
        {
            var warehouseClassIds = 1;
            var faker = new Faker<WarehousesClass>()
                .RuleFor(wc => wc.Name, f => f.Random.Word());

            return faker.Generate(count);
        }

        public static List<SizeType> GenerateSizeTypes(int count)
        {
            var sizeTypeIds = 1;
            var faker = new Faker<SizeType>()
                .RuleFor(st => st.Name, f => f.Random.Word());

            return faker.Generate(count);
        }

        public static List<StatusType> GenerateStatusTypes(int count)
        {
            var statusTypeIds = 1;
            var faker = new Faker<StatusType>()
                .RuleFor(st => st.Name, f => f.Random.Word());

            return faker.Generate(count);
        }

        public static List<Product> GenerateProducts(int count, List<ProductType> productTypes)
        {
            var productIds = 1;
            var faker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Type, f => f.PickRandom(productTypes).Id)
                .RuleFor(p => p.Photopath, f => f.Internet.Avatar());

            return faker.Generate(count);
        }

        public static List<Truck> GenerateTrucks(int count, List<TruckModel> truckModels, List<TruckStatus> truckStatuses)
        {
            var truckIds = 1;
            var faker = new Faker<Truck>()
                .RuleFor(t => t.Number, f => f.Vehicle.Vin())
                .RuleFor(t => t.Model, f => f.PickRandom(truckModels).Id)
                .RuleFor(t => t.Status, f => f.PickRandom(truckStatuses).Id);

            return faker.Generate(count);
        }

        public static List<User> GenerateUsers(int count, List<UsersType> usersTypes)
        {
            var userIds = 1;
            var faker = new Faker<User>()
                .RuleFor(u => u.Login, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Type, f => f.PickRandom(usersTypes).Id)
                .RuleFor(u => u.Firstname, f => f.Name.FirstName())
                .RuleFor(u => u.Lastname, f => f.Name.LastName())
                .RuleFor(u => u.Patronymic, f => f.Name.Prefix())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Passport, f => f.Random.Replace("#### ######"))
                .RuleFor(u => u.Photopath, f => f.Internet.Avatar());

            return faker.Generate(count);
        }

        public static List<Warehouse> GenerateWarehouses(int count, List<WarehouseType> warehouseTypes, List<WarehousesClass> warehouseClasses)
        {
            var warehouseIds = 1;
            var faker = new Faker<Warehouse>()
                .RuleFor(w => w.Address, f => f.Address.FullAddress())
                .RuleFor(w => w.Type, f => f.PickRandom(warehouseTypes).Id)
                .RuleFor(w => w.Class, f => f.PickRandom(warehouseClasses).Id);

            return faker.Generate(count);
        }

        public static List<Supply> GenerateSupplies(int count, List<Product> products, List<Truck> trucks, List<Warehouse> warehouses, List<SizeType> sizeTypes, List<StatusType> statusTypes)
        {
            var supplyIds = 1;
            var faker = new Faker<Supply>()
                .RuleFor(s => s.Product, f => f.PickRandom(products).Id)
                .RuleFor(s => s.Size, f => f.PickRandom(sizeTypes).Id)
                .RuleFor(s => s.Status, f => f.PickRandom(statusTypes).Id)
                .RuleFor(s => s.DepartWarehouseId, f => f.PickRandom(warehouses).Id)
                .RuleFor(s => s.DestinationWarehouseId, f => f.PickRandom(warehouses).Id)
                .RuleFor(s => s.DeliveryStart, f => f.Date.Past())
                .RuleFor(s => s.DeliveryEnd, f => f.Date.Future())
                .RuleFor(s => s.TruckId, f => f.PickRandom(trucks).Id)
                .RuleFor(s => s.Count, f => f.Random.Int(1, 100));

            return faker.Generate(count);
        }
    }
}