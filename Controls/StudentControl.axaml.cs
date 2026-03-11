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
