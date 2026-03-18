using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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

    private void BtnAddImage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
    }



    private void CmbxGroup_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var g = CmbxGroup.SelectedItem as Group;
        if (g != null)
        {
            TxtSpec.Text = ConnectionClass.connect.Specialties.Where(z => z.Id == g.SpecialtyId).FirstOrDefault().Name;
        }
    }

    private void BtnBack_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = new AuthPage();
        window.Show();
        Close();
    }
}
