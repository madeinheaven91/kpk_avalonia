using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using kpk_avalonia.Classes;
using kpk_avalonia.Data;
using System.Linq;

namespace kpk_avalonia;

public partial class RegPage : Window
{
    //указываем непосредственно таблицу из бд
    Data.User user;
    Data.Login login;

    //указываем на элементы в приложении
    public RegPage()
    {
        InitializeComponent();
        user = new User();
        login = new Login();
        this.DataContext = user;
        CmbxGroup.ItemsSource = ConnectionClass.connect.Groups.ToList();
    }

    private void BtnAdd_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //1. Сохранение в таблицу логин и пароль
        login.Login1 = TxtLogin.Text;
        login.Password = TxtPassword.Text;
        ConnectionClass.connect.Logins.Add(login);
        ConnectionClass.connect.SaveChanges();

        //2. Сохранение пользователя
        user.LastName = TxtSurname.Text;
        user.FirstName = TxtName.Text;
        user.Patronymic = TxtPatronumic.Text;
        user.Dob = DateOnly.FromDateTime(DpBirthday.SelectedDate.Value.DateTime);
        user.GroupId = ((Group)CmbxGroup.SelectedItem).Id;

        //внешний ключ берется из новой записи логинов
        user.LoginId = login.Id;
        ConnectionClass.connect.Users.Add(user);
        ConnectionClass.connect.SaveChanges();
        var window = new AuthPage();
        window.Show();
        Close();
    }

    private async void BtnAddImage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Выбрать фото",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("Изображения") { Patterns = new[] { "*.jpg", "*.jpeg", "*.png", "*.gif" } }
            }
        });

        if (files.Count == 0) return;

        await using var stream = await files[0].OpenReadAsync();
        using var memStream = new MemoryStream();
        await stream.CopyToAsync(memStream);

        user.Photo = memStream.ToArray();

        memStream.Position = 0;
        IPicture.Source = new Bitmap(memStream);
    }



    private void CmbxGroup_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var g = CmbxGroup.SelectedItem as Group;
        if (g != null) TxtSpec.Text = ConnectionClass.connect.Specialties.Where(z => z.Id == g.SpecialtyId).FirstOrDefault().Name;
    }

    private void BtnBack_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = new AuthPage();
        window.Show();
        Close();
    }
}
