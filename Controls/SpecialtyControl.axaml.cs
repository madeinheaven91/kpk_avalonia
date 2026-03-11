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
        DgSpecialties.ItemsSource = null;
        DgSpecialties.ItemsSource = ConnectionClass.connect.Specialties.ToList();
    }

    private void DgSpecialties_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {

    }

    private void BtnAdd_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void BtnEdit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void BtnDelete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
}
