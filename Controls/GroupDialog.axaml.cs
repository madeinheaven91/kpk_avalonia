using Avalonia.Controls;
using kpk_avalonia.Classes;
using kpk_avalonia.Data;
using System.Linq;

namespace kpk_avalonia;

public partial class GroupDialog : Window
{
    public Group Group { get; private set; }

    public GroupDialog()
    {
        InitializeComponent();
        Group = new Group();
        CmbxSpecialty.ItemsSource = ConnectionClass.connect.Specialties.ToList();
    }

    public GroupDialog(Group group)
    {
        InitializeComponent();
        Group = group;
        TxtNumber.Text = group.Number;
        TxtDescription.Text = group.Description;
        CmbxSpecialty.ItemsSource = ConnectionClass.connect.Specialties.ToList();
        CmbxSpecialty.SelectedItem = ConnectionClass.connect.Specialties
            .Where(s => s.Id == group.SpecialtyId)
            .FirstOrDefault();
    }

    private void BtnSave_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Group.Number = TxtNumber.Text;
        Group.Description = TxtDescription.Text;
        if (CmbxSpecialty.SelectedItem is Specialty selected)
            Group.SpecialtyId = selected.Id;
        Close(true);
    }

    private void BtnCancel_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close(false);
    }
}
