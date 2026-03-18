using Avalonia.Controls;
using kpk_avalonia.Classes;
using kpk_avalonia.Data;

namespace kpk_avalonia;

public partial class SpecialtyDialog : Window
{
    public Specialty Specialty { get; private set; }

    public SpecialtyDialog()
    {
        InitializeComponent();
        Specialty = new Specialty();
    }

    public SpecialtyDialog(Specialty specialty)
    {
        InitializeComponent();
        Specialty = specialty;
        TxtName.Text = specialty.Name;
        TxtDescription.Text = specialty.Description;
    }

    private void BtnSave_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Specialty.Name = TxtName.Text;
        Specialty.Description = TxtDescription.Text;
        Close(true);
    }

    private void BtnCancel_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close(false);
    }
}
