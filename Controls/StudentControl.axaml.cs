using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using kpk_avalonia.Classes;
using System.Linq;

namespace kpk_avalonia;

public partial class StudentControl : UserControl
{
    public StudentControl()
    {
        InitializeComponent();
        var groups = ConnectionClass.connect.Groups.ToList();
		// злой лайфхак
        var allGroups = new Data.Group { Id = -1, Number = "Все" };
        CmbGroup.ItemsSource = new[] { allGroups }.Concat(groups).ToList();
        CmbGroup.SelectedIndex = 0;
        Refresh();
    }

    private void Refresh()
    {
        var all = ConnectionClass.connect.Users.Include(g => g.Group).ToList();
        var search = TxtSearch.Text?.Trim();
        var selectedGroup = CmbGroup.SelectedItem as Data.Group;
        var groupFilter = selectedGroup?.Id != -1;

        var result = all
            .Where(u => string.IsNullOrEmpty(search) ||
                        $"{u.FirstName} {u.LastName} {u.Patronymic}".ToLower().Contains(search.ToLower()))
            .Where(u => !groupFilter || u.GroupId == selectedGroup!.Id)
            .ToList();

        DgStudents.ItemsSource = result;
    }

    private void TxtSearch_TextChanged(object sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        Refresh();
    }

    private void CmbGroup_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Refresh();
    }

    private void DgStudents_DoubleTapped(object sender, Avalonia.Input.TappedEventArgs e)
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
