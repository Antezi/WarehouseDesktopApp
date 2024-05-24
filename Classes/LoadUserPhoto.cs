using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace WarehouseDesktopApp;

public class LoadUserPhoto
{
    private async Task LoadUserPhotoAsync(string photoPath)
    {
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://your-server-ip/{photoPath}");
            response.EnsureSuccessStatusCode();
            var imageData = await response.Content.ReadAsByteArrayAsync();
            Bitmap UserPhoto = new Bitmap(new MemoryStream(imageData));
        }
        catch (Exception ex)
        {
            // Обработка ошибки
        }
    }
}