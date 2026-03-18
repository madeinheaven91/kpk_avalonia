using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using kpk_avalonia.Classes;
using System;
using System.Linq;

namespace kpk_avalonia;

public partial class GroupControl : UserControl
{
    public GroupControl()
    {
        //ОТВЕЧАЕТ ЗА ВЫВОД СПИСКА
        InitializeComponent();
        DgGroups.ItemsSource = ConnectionClass.connect.Groups.Include(g => g.Specialty).ToList();
        Refresh();
    }

    private void Refresh()
    {
        DgGroups.ItemsSource = null;
        DgGroups.ItemsSource = ConnectionClass.connect.Groups.Include(g => g.Specialty).ToList();
    }

    private void DgGroups_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
		BtnEdit_Click(sender, e);
    }

    // ADD
    private async void BtnAdd_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var dialog = new GroupDialog();
        var result = await dialog.ShowDialog<bool>(HomePage.Instance);

        if (result)
        {
            ConnectionClass.connect.Groups.Add(dialog.Group);
            ConnectionClass.connect.SaveChanges();
            Refresh();
        }
    }

    // EDIT
    private async void BtnEdit_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DgGroups.SelectedItem is not Data.Group selected) return;

        var dialog = new GroupDialog();
        var result = await dialog.ShowDialog<bool>(HomePage.Instance);

        if (result)
        {
            ConnectionClass.connect.Groups.Update(dialog.Group);
            ConnectionClass.connect.SaveChanges();
            Refresh();
        }
    }

    // DELETE
    private async void BtnDelete_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DgGroups.SelectedItem is not Data.Group selected) return;

        ConnectionClass.connect.Groups.Remove(selected);
        ConnectionClass.connect.SaveChanges();
        Refresh();
    }
}
