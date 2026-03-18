using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using kpk_avalonia.Classes;
using System;
using System.Linq;

namespace kpk_avalonia;

public partial class StudentControl : UserControl
{
    public StudentControl()
    {
        //ОТВЕЧАЕТ ЗА ВЫВОД СПИСКА
        InitializeComponent();
        DgStudents.ItemsSource = ConnectionClass.connect.Users.Include(g => g.Group).ToList();
        Refresh();
    }

    private void Refresh()
    {
        DgStudents.ItemsSource = null;
        DgStudents.ItemsSource = ConnectionClass.connect.Users.Include(g => g.Group).ToList();
    }

    private void DgStudents_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
		BtnEdit_Click(sender, e);
    }

    // ADD
    private async void BtnAdd_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var dialog = new StudentDialog();
        var result = await dialog.ShowDialog<bool>(HomePage.Instance);

        if (result)
        {
            ConnectionClass.connect.Users.Add(dialog.User);
            ConnectionClass.connect.SaveChanges();
            Refresh();
        }
    }

    // EDIT
    private async void BtnEdit_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DgStudents.SelectedItem is not Data.User selected) return;

        var dialog = new StudentDialog(selected);
        var result = await dialog.ShowDialog<bool>(HomePage.Instance);

        if (result)
        {
            ConnectionClass.connect.Users.Update(dialog.User);
            ConnectionClass.connect.SaveChanges();
            Refresh();
        }
    }

    // DELETE
    private async void BtnDelete_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DgStudents.SelectedItem is not Data.User selected) return;

        ConnectionClass.connect.Users.Remove(selected);
        ConnectionClass.connect.SaveChanges();
        Refresh();
    }
}
