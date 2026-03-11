using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace kpk_avalonia;

public partial class HomePage : Window
{
    public HomePage()
    {
        InitializeComponent();
        MainControl.Content = new StudentControl();
    }

    private void BtnStudents_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new StudentControl();
    }

    private void BtnGroups_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new GroupControl();
    }

    private void BtnSpeciality_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new SpecialtyControl();
    }
}
