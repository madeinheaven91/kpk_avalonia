using System;
using System.Globalization;
using System.IO;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace kpk_avalonia.Classes;

public class BytesToBitmapConverter : IValueConverter
{
    public static readonly BytesToBitmapConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
		// если передаются байты из бд, то преобразуем их в картинку
        if (value is byte[] { Length: > 0 } bytes)
        {
            try { return new Bitmap(new MemoryStream(bytes)); }
            catch { }
        }

		// если байты не передаются, то возвращаем картинку по умолчанию
        var uri = new Uri("avares://kpk_avalonia/Assets/fallback.jpg");
        using var stream = AssetLoader.Open(uri);
        return new Bitmap(stream);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
