using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using kpk_avalonia.Classes;
using System.Linq;

namespace kpk_avalonia;

public partial class RegPage : Window
{
    public RegPage()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var login = TxtLogin.Text;
        var password = TxtPassword.Text;

        var user = ConnectionClass.connect.Logins.Where(d => d.Login1 == login && d.Password == password).FirstOrDefault();
        if (user != null)
        {
            var home = new HomePage();
            home.Show();
            Close();
        }
        else
        {
			TxtError.Text = "Неправильный логин или пароль";
        }
    }

    private void TextBlock_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
		var auth = new AuthPage();
		auth.Show();
		Close();
    }
}
