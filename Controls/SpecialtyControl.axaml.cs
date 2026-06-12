using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using kpk_avalonia.Classes;
using System;
using System.Linq;

namespace kpk_avalonia;

public partial class SpecialtyControl : UserControl
{
    public SpecialtyControl()
    {
        //ОТВЕЧАЕТ ЗА ВЫВОД СПИСКА
        InitializeComponent();
        DgSpecialties.ItemsSource = ConnectionClass.connect.Specialties.ToList();
        Refresh();
    }

    private void Refresh()
    {
        var all = ConnectionClass.connect.Specialties.ToList();
        var search = TxtSearch.Text?.Trim();
        var result = string.IsNullOrEmpty(search)
            ? all
            : all.Where(s => (s.Name != null && s.Name.ToLower().Contains(search.ToLower())) ||
                             (s.Description != null && s.Description.ToLower().Contains(search.ToLower()))).ToList();
        DgSpecialties.ItemsSource = result;
    }

    private void TxtSearch_TextChanged(object sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        Refresh();
    }

    private void DgSpecialties_DoubleTapped(object sender, Avalonia.Input.TappedEventArgs e)
    {
		BtnEdit_Click(sender, e);
    }

    // ADD
    private async void BtnAdd_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var dialog = new SpecialtyDialog();
        var result = await dialog.ShowDialog<bool>(HomePage.Instance);

        if (result)
        {
            ConnectionClass.connect.Specialties.Add(dialog.Specialty);
            ConnectionClass.connect.SaveChanges();
            Refresh();
        }
    }

    // EDIT
    private async void BtnEdit_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DgSpecialties.SelectedItem is not Data.Specialty selected) return;

        var dialog = new SpecialtyDialog();
        var result = await dialog.ShowDialog<bool>(HomePage.Instance);

        if (result)
        {
            ConnectionClass.connect.Specialties.Update(dialog.Specialty);
            ConnectionClass.connect.SaveChanges();
            Refresh();
        }
    }

    // DELETE
    private async void BtnDelete_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DgSpecialties.SelectedItem is not Data.Specialty selected) return;

        ConnectionClass.connect.Specialties.Remove(selected);
        ConnectionClass.connect.SaveChanges();
        Refresh();
    }
}
