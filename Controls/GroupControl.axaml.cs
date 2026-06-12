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
        InitializeComponent();
        var specialties = ConnectionClass.connect.Specialties.ToList();
        var allSpecialties = new Data.Specialty { Id = -1, Name = "Все" };
        CmbSpecialty.ItemsSource = new[] { allSpecialties }.Concat(specialties).ToList();
        CmbSpecialty.SelectedIndex = 0;
        Refresh();
    }

    private void Refresh()
    {
        var all = ConnectionClass.connect.Groups.Include(g => g.Specialty).ToList();
        var search = TxtSearch.Text?.Trim();
        var selectedSpecialty = CmbSpecialty.SelectedItem as Data.Specialty;
        var specialtyFilter = selectedSpecialty?.Id != -1;

        var result = all
            .Where(g => string.IsNullOrEmpty(search) ||
                        (g.Number != null && g.Number.ToLower().Contains(search.ToLower())) ||
                        (g.Description != null && g.Description.ToLower().Contains(search.ToLower())))
            .Where(g => !specialtyFilter || g.SpecialtyId == selectedSpecialty!.Id)
            .ToList();

        DgGroups.ItemsSource = result;
    }

    private void TxtSearch_TextChanged(object sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        Refresh();
    }

    private void CmbSpecialty_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Refresh();
    }

    private void DgGroups_DoubleTapped(object sender, Avalonia.Input.TappedEventArgs e)
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
